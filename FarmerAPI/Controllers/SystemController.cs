using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmerAPI.Models;
using FarmerAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using FarmerAPI.Extensions;

namespace FarmerAPI.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly WeatherContext _context;
        private readonly IHttpContextAccessor _accessor;

        public SystemController(WeatherContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        [HttpGet("[action]")]
        public IEnumerable<vmMenu>  GetAllowedMenu()
        {
            int RoleId = _accessor.CurrentUserRole();//從Token抓RoleId
            List<int> AllowedMenuId = _context.ImenuRole.Where(x => x.RoleId == RoleId).Select(x => x.MenuId).ToList();
            IEnumerable<Menu> AuthMenu = _context.Menu.Where(x => AllowedMenuId.Contains(x.MenuId));

            List<vmMenu> ReturnMenu = new List<vmMenu>();
            
            //Menu有很多棵tree，每個tree一個root
            foreach (Menu Root in AuthMenu.Where(x => x.RootMenuId == null))
            {
                //找出所有root底下的leafs
                List<vmMenu> Tree = TreeMenu(Root, AuthMenu);

                //加入回傳的tree
                ReturnMenu.Add(Tree[0]);
            };

            return ReturnMenu;
        }

        //children會有很多menu因此屬性為List<vmMenu>，所以必須回傳List<vmMenu>才可跑遞迴
        private List<vmMenu> TreeMenu(Menu Root, IEnumerable<Menu> AllMenu)
        {
            List<vmMenu> ReturnTreeMenu = new List<vmMenu>();

            vmMenu TreeRoot = new vmMenu()
            {
                Path = Root.Path,
                MenuText = Root.MenuText,
                Component = Root.Component
            };

            if (AllMenu.Any(x => x.RootMenuId == Root.MenuId))
            {
                TreeRoot.Children = new List<vmMenu>();
                //找root及其底下leafs
                foreach (Menu item in AllMenu.Where(x => x.RootMenuId == Root.MenuId)) // && x.MenuId == Root.MenuId
                {
                    List<vmMenu> Node = TreeMenu(item, AllMenu);
                    TreeRoot.Children.Add(Node[0]);
                };                
            }           

            ReturnTreeMenu.Add(TreeRoot);

            return ReturnTreeMenu;
        }
    }
}