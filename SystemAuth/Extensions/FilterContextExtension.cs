using Microsoft.AspNetCore.Mvc.Filters;

namespace SystemAuth.Extensions
{
    public static class FilterContextExtension
    {
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
