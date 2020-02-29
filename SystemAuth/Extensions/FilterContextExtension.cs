using SystemAuth.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SystemAuth.Extensions
{
    public static class FilterContextExtension
    {
        //public static int CurrentUserRole(this FilterContext context)
        //{
        //    var StringRoleId = context?.HttpContext?.User?.FindFirst(JwtClaimTypes.RoleId)?.Value;
        //    int.TryParse(StringRoleId ?? "0", out int RoleId);

        //    return RoleId;
        //}

        public static string CurrentUserId(this FilterContext context)
        {
			/* using System.IdentityModel.Tokens.Jwt
			 * JwtRegisteredClaimNames.NameId
			 * 用這個會變成下列網址		 
			 * http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
			 */

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
