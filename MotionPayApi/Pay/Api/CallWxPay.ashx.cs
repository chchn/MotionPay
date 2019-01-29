using MotionPay.Config;
using MotionPay.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using TuYu.Common;

namespace MotionPayApi.Pay.Api
{
    /// <summary>
    /// CallWxPay 的摘要说明
    /// </summary>
    public class CallWxPay : IHttpHandler
    {
        private static string BASE_URL = PayConfig.BASE_URL;
        private static string ORDER_MINI_URL = BASE_URL + "/mini";
        private static string Notify_Url = PayConfig.Notify_Url;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            decimal amount = HYRequest.GetDecimalByParams("amount");
            int totalAmount = Convert.ToInt32(amount * 100);
            if (totalAmount <= 0)
            {
                var data = new { code = "1", message = "金额不能小于等于0" };
                context.Response.Write(JsonConvert.SerializeObject(data));
            }
            else
            {
                string returnRes = prePayCanada(totalAmount.ToString());
                context.Response.Write(returnRes);
            }
        }

        public string prePayCanada(string tranAmount)
        {
            string out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string total_fee = tranAmount;
            string openid = WeChatVerifyHelper.GetUserOpenIdCookie();

            Dictionary<string, string> requestJson = new Dictionary<string, string>();
            requestJson.Add("mid", PayConfig.WECHAT_MINI_MID);
            requestJson.Add("out_trade_no", out_trade_no);
            requestJson.Add("pay_channel", "W");
            requestJson.Add("total_fee", total_fee);
            requestJson.Add("goods_info", "ThirdPartyProduct");
            requestJson.Add("return_url", Notify_Url);
            requestJson.Add("spbill_create_ip", "192.168.1.132");
            // requestJson.put("terminal_no", "MiniApp"); // WeChat Mini App Account later.
            requestJson.Add("terminal_no", "WebServer"); // Use WebServer for now.
            requestJson.Add("openid", openid);
            //		SysWxChildMerchantToken token=sysWxChildMerchantTokenDao.getByMid(prePayReq.getSysParam().getMid());
            //		String sign = MakeSign.getSignValue(inputJson, token.getWxAppId(), token.getWxAppSecret());
            //		String sign = MakeSign.getSignValue(inputJson, "5005642018003", "a0e99280e3c9b4df6f907c7eab3e063e");
            string signLocal = SignSHA1.getSign(requestJson);
            requestJson.Add("sign", signLocal);
            Log.WriteLog("sign", signLocal);
            //send payment request
            string requestData = JsonConvert.SerializeObject(requestJson);
            Log.WriteLog("inputJson", requestData);
            string result = HttpUtil.Send(requestData, ORDER_MINI_URL);
            Log.WriteLog("TheResponseJson", result);
            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}