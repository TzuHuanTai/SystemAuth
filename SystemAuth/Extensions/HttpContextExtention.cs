using Microsoft.AspNetCore.Http;
using SystemAuth.ViewModels;

namespace SystemAuth.Extensions
{
	public static class HttpContextExtention
	{
		public static string CurrentUserId(this HttpContext httpContext)
		{
			/* using System.IdentityModel.Tokens.Jwt
			 * JwtRegisteredClaimNames.NameId
			 * 用這個會變成下列網址		 
			 * http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
			 */
			return httpContext?.User?.FindFirst(JwtClaimTypes.Id)?.Value;
		}

		public static string GetToken(this HttpContext httpContext)
		{
			return httpContext?.Request.Headers["Authorization"];
		}
	}
}
