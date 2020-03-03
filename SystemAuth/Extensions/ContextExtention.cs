using System.Linq;
using System.Collections.Generic;
using SystemAuth.Models.SQLite;

namespace SystemAuth.Extensions
{
	public static class ContextExtention
	{
        //檢查使用者是否有該角色
        public static bool HasUserRole(this SystemAuthContext context, List<int> userRole, string username)
        {
            return context.IMemberRole.Any(x => x.Account == username && userRole.Contains(x.RoleId));
        }

        //檢查使用者是否有權限執行該Action
        public static bool HasAllowedAction(this SystemAuthContext context, List<int> userRole, string action)
        {
            return context.Actions.Any(x =>
                x.Name == action &&
                x.IActionRole.Any(
                    y => userRole.Contains(y.RoleId) && y.ActionId == x.ActionId
                )
            );
        }

        public static List<int> GetUserRoles(this SystemAuthContext context, string userAccount)
        {
            //預設userRole = 0 (Guest) 
            List<int> userRole = new List<int>() { 0 };

            List<int> RoleID = context.IMemberRole
                   .Where(y => y.Account == userAccount)
                   .Select(x => x.RoleId)
                   .ToList();

            //任一角色有權限即可執行Action
            userRole.AddRange(RoleID);

            return userRole;
        }
    }
}
