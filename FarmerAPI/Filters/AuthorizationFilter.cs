using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FarmerAPI.Filters
{
    public class AuthorizationFilter: Attribute,IAuthorizationFilter
    {        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                //Extract credentials
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);



                if (username == "test" && password == "test")
                {

                }
                else
                {
                    FailAuthorize(context);
                }
            }
            else
            {
                FailAuthorize(context);
            }
        }

        public void FailAuthorize(AuthorizationFilterContext context)
        {
            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            //顯示Json格式"Unauthorized"
            //context.Result = new JsonResult("Unauthorized");

            //顯示400 not found
            //context.Result = new NotFoundResult();

            //顯示 401 Unauthorized
            context.Result = new UnauthorizedResult();

            //預設禁止畫面：描述寫403
            //context.Result = new ForbidResult("403");

            //重新導向至特定View
            //context.Result = new ViewResult() { ViewName = "UnauthorizedAccess" };

            //報錯
            //throw new Exception("The authorization header is either empty or isn't Basic."); 
        }
    }


}
