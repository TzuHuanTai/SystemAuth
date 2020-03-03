using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SystemAuth.Extensions;
using SystemAuth.Models.SQLite;

/* 20180529 @Richard 統一在Startup.cs AddMvc中全域注入此Filter功能是為了達到要"動態"判斷，可判斷Request是否有權限。
 * 說明：
 * 因有注入AddAuthentication服務判斷jwt，而又在app.UseAuthentication()要求Request進入MVC前都要要判斷jwt，
 * 所以加上標籤[AllowAnonymous]或[Authrize]就會自動判讀可否使用該Action。
 * Action判斷jwt時，沒加標籤等同[AllowAnonymous]。若jwt偽造，[Authrize]會自動阻擋。
 * 這樣就不用每個Action都加標籤了！
 * 因注入在services.AddMvc中，而app.UseMvc()又是最後才執行。
 * 所以來此Filter時，jwt已被app.UseAuthentication()判斷過了，可直接抓jwt判讀結果再依角色做Action權限控管。
 * 因此所有Action上面都不用加標籤，統一在這Filter就判斷好了！
 */

namespace SystemAuth.Filters
{
    public class AuthorizationFilter: Attribute,IAuthorizationFilter
    {        
        private readonly SystemAuthContext _context;
      
        public AuthorizationFilter(SystemAuthContext dbContext)
        {            
            _context = dbContext;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 取得Request呼叫的Action名稱
            string accessAction = context.CurrentAction();

            // 外部的驗證在"/api/auth/checkAuth"實做，內部controller驗證
            if (string.Compare(accessAction, "checkAuth", true) != 0)
            {
                //----取得參數判斷user是否有權限使用Action----//
                string authHeader = context.HttpContext.GetToken();
                string userAccount = context.HttpContext.CurrentUserId(); // 從jwt抓id，無jwt或偽造時，讀出userAccount = null
                var userRole = _context.GetUserRoles(userAccount);

                // ----正式驗證request權限---- //
                if (authHeader != null && authHeader.StartsWith("Bearer", true, CultureInfo.CurrentCulture))
                {

                    if (!(_context.HasAllowedAction(userRole, accessAction) && _context.HasUserRole(userRole, userAccount)))
                    {
                        FailAuthorize(context);
                    }
                }
                //Guest & Anonymous without authorized header, 沒有帶入header，userRole預設是0，對照database Guest角色代號0
                else
                {
                    if (!_context.HasAllowedAction(userRole, accessAction))
                    {
                        FailAuthorize(context);
                    }
                }
            }
        }

        public void FailAuthorize(AuthorizationFilterContext context)
        {
            //顯示 401 Unauthorized
            context.Result = new UnauthorizedResult();
        }
    }
}
