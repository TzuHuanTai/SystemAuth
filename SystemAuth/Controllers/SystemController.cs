using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using SystemAuth.ViewModels;
using SystemAuth.Extensions;
using SystemAuth.Models.SQLite;

namespace SystemAuth.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private IConfiguration _config;
        private readonly SystemAuthContext _context;
        private readonly IHttpContextAccessor _accessor;       

        public SystemController(IConfiguration config, SystemAuthContext context, IHttpContextAccessor accessor)
        {
            _config = config;
            _context = context;
            _accessor = accessor;          
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetReflection()
        {
            var DbExistCtrls = _context.Ctrl;
            var DbExistActions = _context.Actions;
 
            IEnumerable<Type> controllers = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t)).Select(t => t);            

            foreach (Type controller in controllers)
            {
                int ControllerID;
                string ControllerName = controller.Name.Replace("Controller","");

                //檢查是否已有Controller登入
                if (IsControllerExists(ControllerName))
                {                    
                    //有則抓出id
                    ControllerID = DbExistCtrls.Where(x => x.Name == ControllerName).Select(x => x.CtrlId).SingleOrDefault();
                }
                else
                {
                    ControllerID = DbExistCtrls.Max(x => x.CtrlId) + 1;
                    Ctrl ctrl = new Ctrl()
                    {
						CtrlId = ControllerID,
                        Name = ControllerName
                    };

                    _context.Ctrl.Add(ctrl);
                    //先存擋
                    await _context.SaveChangesAsync();
                }                

                List<MethodInfo> actions = controller.GetMethods().Where(t => !t.IsSpecialName && t.DeclaringType.IsSubclassOf(typeof(ControllerBase)) && t.DeclaringType.FullName == controller.FullName && t.IsPublic && !t.IsStatic).ToList();

                foreach (MethodInfo action in actions)
                {
                    Attribute attribute = action.GetCustomAttributes().Where(attr => attr is IActionHttpMethodProvider).FirstOrDefault();
                    
                    string ActionName = action.Name;
                    string HttpMethod = attribute.GetType().Name.Replace("Http", "").Replace("Attribute", "");

                    //int ActionID;

                    //檢查是否已有Action登入在此Controller下
                    if (IsActionsExists(ActionName, ControllerID, HttpMethod))
                    {
                       // do nothing
                    }
                    else
                    {
                        int ActID = DbExistActions.Max(x => x.ActionId) + 1;
                        Actions act = new Actions()
                        {
							ActionId = ActID,
                            Name = ActionName,
                            Method = HttpMethod,
                            ControllerId = ControllerID
                        };
                        _context.Actions.Add(act);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult GetAllowedMenu()
        {
            try
            {
                //從Token抓user帳號，無則null
                string Account = _accessor.CurrentUserId();

                //該帳號所有角色可進入的menu都篩選出來
                List<int> AllowedMenuId = _context.IMenuRole
                    //角色至少有Guest: 0 
                    .Where(x => x.Role.IMemberRole.Any(y => y.Account == Account) || x.RoleId == 0)
                    .Select(x => x.MenuId)
                    .Distinct()
                    .ToList();

                //撈出允許的menu資料，並依造SortNo排序
                List<Menu> AuthMenu = _context.Menu.Where(x =>
                    x.IMenuRole.Any(y =>
                        AllowedMenuId.Contains(y.MenuId)
                    )
                ).OrderBy(x => x.SortNo).ToList();

                List<VmMenu> ReturnMenu = new List<VmMenu>();

                //Menu有很多棵tree，每個tree一個root
                foreach (Menu Root in AuthMenu.Where(x => x.RootMenuId == null))
                {
                    //找出所有root底下的leafs
                    List<VmMenu> Tree = TreeMenu(Root, AuthMenu);

                    //加入回傳的tree
                    ReturnMenu.Add(Tree[0]);
                };

                return Ok(ReturnMenu);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //children會有很多menu因此屬性為List<vmMenu>，所以必須回傳List<vmMenu>才可跑遞迴
        private List<VmMenu> TreeMenu(Menu Root, List<Menu> AllMenu)
        {
            List<VmMenu> ReturnTreeMenu = new List<VmMenu>();

            VmMenu TreeRoot = new VmMenu()
            {
                Path = Root.Path,
                MenuText = Root.MenuText,
                Selector = Root.Selector,
                Component = Root.Component
            };

            if (AllMenu.Any(x => x.RootMenuId == Root.MenuId))
            {
                TreeRoot.Children = new List<VmMenu>();
                //找root及其底下leafs
                foreach (Menu item in AllMenu.Where(x => x.RootMenuId == Root.MenuId)) // && x.MenuId == Root.MenuId
                {
                    List<VmMenu> Node = TreeMenu(item, AllMenu);
                    TreeRoot.Children.Add(Node[0]);
                };                
            } 

            ReturnTreeMenu.Add(TreeRoot);

            return ReturnTreeMenu;
        }

        private bool IsActionsExists(string Name, int CtrlID, string HttpMethod)
        {
            return _context.Actions.Any(e => e.Name == Name && e.ControllerId == CtrlID && e.Method.ToLower() == HttpMethod.ToLower());
        }

        private bool IsControllerExists(string Name)
        {
            return _context.Ctrl.Any(e => e.Name == Name);
        }
    }
}