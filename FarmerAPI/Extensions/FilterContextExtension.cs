using FarmerAPI.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmerAPI.Extensions
{
    public static class FilterContextExtension
    {
        public static int CurrentUserRole(this FilterContext context)
        {
            var StringRoleId = context?.HttpContext?.User?.FindFirst(JwtClaimTypes.RoleId)?.Value;
            int.TryParse(StringRoleId ?? "0", out int RoleId);

            return RoleId;
        }

        public static string CurrentUserId(this FilterContext context)
        {
            var StringId = context?.HttpContext?.User?.FindFirst(JwtClaimTypes.Id)?.Value;

            return StringId;
        }

        public static string CurrentAction(this FilterContext context)
        {
            var ActionName = context?.RouteData?.Values["Action"]?.ToString();

            return ActionName;
        }

        public static string CurrentController(this FilterContext context)
        {
            var ControllerName = context?.RouteData?.Values["Controller"]?.ToString();

            return ControllerName;
        }
    }
}
