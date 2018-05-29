using FarmerAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FarmerAPI.Extensions
{
    public static class IHttpContextAccessorExtension
    {
        public static int CurrentUserRole(this IHttpContextAccessor httpContextAccessor)
        {
            var StringRoleId = httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimTypes.RoleId)?.Value;
            int.TryParse(StringRoleId ?? "0", out int RoleId);

            return RoleId;
        }

        public static string CurrentUserId(this IHttpContextAccessor httpContextAccessor)
        {
            var StringId = httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimTypes.Id)?.Value;
            
            return StringId;
        }

    }
}
