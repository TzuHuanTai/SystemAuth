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
        private readonly SystemStructureContext _contextSys;
        private readonly SystemStructure2Context _contextSys2;

        public SystemController(WeatherContext context, IHttpContextAccessor accessor, SystemStructureContext contextSys, SystemStructure2Context contextSys2)
        {
            _context = context;
            _accessor = accessor;
            _contextSys = contextSys;
            _contextSys2 = contextSys2;
        }

        [HttpGet("[action]")]
        public IEnumerable<vmMenu>  GetAllowedMenu()
        {
            //從Token抓user帳號，無則null
            string Account = _accessor.CurrentUserId();

            //該帳號所有角色可進入的Menu都篩選出來
            List<int> AllowedMenuId = _context.ImenuRole
                .Where(x => x.Role.ImemRole.Any(y => y.Account == Account))
                .Select(x => x.MenuId)
                .ToList();

            //角色至少有Guest
            AllowedMenuId.Add(0);

            //撈出允許的menu資料
            IEnumerable<Menu> AuthMenu = _context.Menu.Where(x =>
                x.ImenuRole.Any(y =>
                    AllowedMenuId.Contains(y.RoleId)
                )
            );

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

        [HttpGet("[action]")]
        public List<Test2Db> Test(int index = 2)
        {

            List<Test> ReturnSomethings = new List<Test>();
            List<VwTest> ReturnJoin2DB = new List<VwTest>();
            if (index == 0) //inner
            {
                ReturnSomethings = _context.Menu.Join(
                _context.ImenuRole,
                a => a.MenuId,
                b => b.MenuId,
                (a, b) => new Test { Path = a.Path, MenuText = a.MenuText, Component = a.Component, RoleId = b.RoleId }).ToList();
            }
            else if (index == 1) //left or right (outter)
            {
                ReturnSomethings = _context.Menu.GroupJoin(
                _context.ImenuRole,
                a => a.MenuId,
                b => b.MenuId,
                (a, joinT) => new  { Path = a.Path, MenuText = a.MenuText, Component = a.Component, joinTable = joinT })
                .SelectMany(sub=>sub.joinTable.DefaultIfEmpty(), (sub,b)=>new Test { Path = sub.Path, MenuText = sub.MenuText, Component = sub.Component, RoleId = b.RoleId })
                .ToList();
            }
            else if(index == 2)
            {
                //ReturnJoin2DB = _contextSys.TestTable.Join(
                //_context.Actions.Where(x=>x.ActionId>=3),
                //a => a.TestPk,
                //b => b.ActionId,
                //(a, b) => new Test2DB
                //{
                //    TestPk = a.TestPk,
                //    Test1 = (a.Test1 ?? "0"),
                //    Test2 = (a.Test2 ?? "0"),
                //    ActionName = (b.ActionName ?? "0")
                //}).ToList();
                //var TestFromSql1 = _contextSys.Set<Test2DB>().
                //    FromSql(
                //    "select b.TestPK, b.Test1, b.Test2, a.ActionName " +
                //    "from  [Weather].[dbo].[Actions] a join " +
                //    "[SystemStructure].[dbo].[TestTable] b " +
                //    "on a.ActionId=b.TestPK"
                //    ).ToList();

                var TestFromSql2 = _contextSys.Set<VwTest>().
                    FromSql(
                    "select top(2)* from dbo.VwTest"
                    ).ToList();

                //return TestFromSql2;
            }



            //_context.Database.ExecuteSqlCommand("");

            string aaa = _contextSys.TestTable.Select(x => x.Test2).First();
            string bbb = _context.StationInfo.Select(x => x.Name).First();
            //_context.Set().FromSql()
            //return new string[] { aaa, bbb };
            //return ReturnJoin2DB;



            var database1 = _contextSys.TestTable.ToList();
            var database2 = _contextSys2.Test2Db.Where(x =>
                    database1.Any(y => y.TestPk == x.TestPk)
                ).ToList();

            return database2;

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