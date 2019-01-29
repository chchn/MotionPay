using System;
using System.Web;

namespace MotionPay.Utility
{
    /// <summary>
    /// Wechat OpenId Cookie
    /// </summary>
    public class WeChatVerifyHelper
    {
        private static string OpenIdCookieName = "BabynaibaTouchOpenId";
        /// <summary>
        /// 记录用户的OpenId
        /// </summary>
        /// <param name="openid"></param>
        public static void AddUserOpenIdCookie(string openid)
        {
            if (!string.IsNullOrEmpty(openid))
            {
                HttpCookie hc = new HttpCookie(OpenIdCookieName, openid);
                hc.Expires = DateTime.Now.AddMonths(6);
                hc.Domain = HttpContext.Current.Request.Url.Host.Substring(HttpContext.Current.Request.Url.Host.IndexOf('.'));
                HttpContext.Current.Response.Cookies.Add(hc);
            }
        }
        /// <summary>
        /// 获得当前用户的OpenId
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetUserOpenIdCookie()
        {
            string value = "";
            HttpCookie hc = HttpContext.Current.Request.Cookies[OpenIdCookieName];
            if (hc != null && !string.IsNullOrEmpty(hc.Value))
            {
                value = hc.Value;
            }
            return value;
        }
    }
}
