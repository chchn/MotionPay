/* Developer Qin  */
/* Email:lijie.qin@chchuan.com  */
using System;
using System.Web;
using TuYu.Common;
using TuYu.WeiXin;
using MotionPay.Config;
namespace MotionPay.Utility
{
    public class ServicePage : System.Web.UI.Page
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            #region
            //如果在微信浏览器中才走下面的微信步骤
            if (Request.UserAgent.ToLower().Contains("micromessenger"))
            {
                string openId = WeChatVerifyHelper.GetUserOpenIdCookie();
                string wechat = HYRequest.GetStringByParams("wechat");
                //Oauth验证链接
                if (string.IsNullOrEmpty(openId) && string.IsNullOrEmpty(wechat))
                {
                    string redirect_url = HttpContext.Current.Request.Url.AbsoluteUri;
                    if (redirect_url.Contains("?"))
                    {
                        redirect_url = redirect_url + "&wechat=1";
                    }
                    else
                    {
                        redirect_url = redirect_url + "?wechat=1";
                    }
                    string url = string.Format(@"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + PayConfig.AppId + "&redirect_uri=" + HttpContext.Current.Server.UrlEncode(redirect_url) + "&response_type=code&scope=snsapi_base&state=123#wechat_redirect");
                    HttpContext.Current.Response.Redirect(url);
                }
                //用户Oauth验证
                if (string.IsNullOrEmpty(openId) && !string.IsNullOrEmpty(wechat))
                {
                    string oauth_code = HYRequest.GetStringByParams("code");
                    OAuthEntity entity = SeniorService.UserOAuthInfo(PayConfig.AppId, PayConfig.AppSecret, oauth_code);
                    if (entity.result)
                    {
                        openId = entity.openid;
                        WeChatVerifyHelper.AddUserOpenIdCookie(openId);
                    }
                }
                Log.WriteLog("OpenId", openId);
            }
            #endregion
        }
    }
}