using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmerAPI.Models;
using FarmerAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace FarmerAPI.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly WeatherContext _context;
        public MenuController(WeatherContext context)
        {
            _context = context;
        }

        [HttpGet("[action]/{RoleId}")]
        public IEnumerable<vmMenu>  Menu(int RoleId)
        {
            List<int> AllowedMenuId = _context.ImenuRole.Where(x => x.RoleId == RoleId).Select(x => x.MenuId).ToList();
            IEnumerable<Menu> FilteredMenu = _context.Menu.Where(x => AllowedMenuId.Contains(x.MenuId));

            List<vmMenu> ReturnMenu = new List<vmMenu>();

            foreach (Menu data in FilteredMenu)
            {
                ReturnMenu.Add(new vmMenu
                {
                    MenuText = data.MenuText,
                    Component = data.Component
                });
            };

            return ReturnMenu;
        }
    }
}