using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace LikeButton.Services.Helpers
{
    public class WebHelpers
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext
        {
            get { return _httpContextAccessor.HttpContext; }
        }

        public static string GetRemoteIP
        {
            get { return HttpContext.Connection.RemoteIpAddress.ToString(); }
        }

        public static string GetUserAgent
        {
            get { return HttpContext.Request.Headers["User-Agent"].ToString(); }
        }

        public static string GetScheme
        {
            get { return HttpContext.Request.Scheme; }
        }

        public static string GetUser
        {
            get
            {
                if (HttpContext != null)
                {
                    var UserClaim = HttpContext.User;

                    var Username = UserClaim.Claims?.FirstOrDefault(x => x.Type.Equals("ID", StringComparison.OrdinalIgnoreCase))?.Value;

                    return Username;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
