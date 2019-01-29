/* Developer Qin  */
/* Email:lijie.qin@chchuan.com  */
using MotionPay.Models.Alipay;
using System.Collections.Generic;
using TuYu.Common;
using Newtonsoft.Json;
using MotionPay.Utility;
using MotionPay.Config;
using System;
using MotionPay.Models;

namespace MotionPay
{
    public class AlipayClient
    {
        /// <summary>
        /// 生成H5支付链接地址
        /// </summary>
        /// <param name="paymentType"></param>
        /// <param name="orderId"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="productName"></param>
        /// <param name="returnURL"></param>
        /// <param name="wapURL"></param>
        /// <returns></returns>
        public static string GenerateH5URL(string paymentType, string orderId,
    decimal paymentAmount, string productName, string wapURL)
        {
            H5PayRequest payOnlineH5Req = new H5PayRequest();
            payOnlineH5Req.mid = PayConfig.WECHAT_MINI_MID;
            payOnlineH5Req.out_trade_no = orderId;
            payOnlineH5Req.total_fee = Convert.ToInt32(paymentAmount * 100);
            payOnlineH5Req.pay_channel = (paymentType.Replace("H5_", ""));
            payOnlineH5Req.goods_info = productName;
            payOnlineH5Req.return_url = PayConfig.Alipay_Notify_Url;
            payOnlineH5Req.terminal_no = "WebServer";
            payOnlineH5Req.spbill_create_ip = "192.168.1.132";
            payOnlineH5Req.wap_url = wapURL;
            payOnlineH5Req.sign = CreateSign(payOnlineH5Req);
            string jsonH5Req = JsonConvert.SerializeObject(payOnlineH5Req);
            Log.WriteLog("Alipay", "JSONstr to send:" + jsonH5Req);
            try
            {
                string resp = HttpUtil.HttpPostData(PayConfig.Alipay_H5_PAY, jsonH5Req);
                Log.WriteLog("Alipay", "Return result:" + resp);
                ResponseModel<AlipayResponseModel> json = JsonConvert.DeserializeObject<ResponseModel<AlipayResponseModel>>(resp);
                Dictionary<string, string> localObject = new Dictionary<string, string>();
                localObject.Add("mid", PayConfig.WECHAT_MINI_MID);
                localObject.Add("out_trade_no", orderId);
                string sign = SignSHA1.getSign(localObject);
                Log.WriteLog("Alipay", "Sign:" + sign);
                string payUrl = PayConfig.Alipay_PAY_URL + "?mid=" + PayConfig.WECHAT_MINI_MID + "&out_trade_no=" + orderId + "&sign=" + sign;
                Log.WriteLog("Alipay", "return URL is:" + payUrl + "#");
                return payUrl;
            }
            catch (Exception ex)
            {
                Log.WriteLog("Alipay", "Ex:" + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 请求签名
        /// </summary>
        /// <param name="h5Req"></param>
        /// <returns></returns>
        public static string CreateSign(H5PayRequest h5Req)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("goods_info", h5Req.goods_info);
            dic.Add("out_trade_no", h5Req.out_trade_no);
            dic.Add("pay_channel", h5Req.pay_channel);
            dic.Add("return_url", h5Req.return_url);
            dic.Add("spbill_create_ip", h5Req.spbill_create_ip);
            dic.Add("terminal_no", h5Req.terminal_no);
            dic.Add("total_fee", h5Req.total_fee.ToString());
            dic.Add("wap_url", h5Req.wap_url);
            dic.Add("mid", h5Req.mid);
            Log.WriteLog("Alipay","beginSign:"+ JsonConvert.SerializeObject(h5Req));
            string signStr = SignSHA1.getSignOnline(dic, PayConfig.WECHAT_MINI_APPID, PayConfig.WECHAT_MINI_APPSECURE);
            return signStr;
        }
    }
}
