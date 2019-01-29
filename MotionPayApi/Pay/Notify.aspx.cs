using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using MotionPay.Models;
using MotionPay.Buz;
using TuYu.Common;
using System.Collections.Generic;
using MotionPay.Utility;

namespace MotionPayApi.Pay
{
    public partial class MotionPayApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string postStr = "";
            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);
                if (!string.IsNullOrEmpty(postStr))
                {
                    try
                    {
                        Log.WriteLog("wx_notice", postStr);
                        NoticeMsg(postStr);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLog("微信支付通知回调:", ex.Message.ToString());
                    }
                }
            }
        }
        public void NoticeMsg(string jsonData)
        {
            try
            {
                var notifyData = JsonConvert.DeserializeObject<PayNotifyModel>(jsonData);
                #region 验签名
                Dictionary<string, string> requestJson = new Dictionary<string, string>();
                requestJson.Add("currency_type", notifyData.currency_type);
                requestJson.Add("exchange_rate", notifyData.exchange_rate.ToString());
                requestJson.Add("mid", notifyData.mid);
                requestJson.Add("out_trade_no", notifyData.out_trade_no);
                requestJson.Add("pay_channel", notifyData.pay_channel);
                requestJson.Add("pay_result", notifyData.pay_result);
                requestJson.Add("settlement_amount", notifyData.settlement_amount.ToString());
                requestJson.Add("third_order_no", notifyData.third_order_no);
                requestJson.Add("total_fee", notifyData.total_fee.ToString());
                requestJson.Add("transaction_id", notifyData.transaction_id);
                requestJson.Add("user_identify", notifyData.user_identify);
                string signLocal = SignSHA1.getSign(requestJson);
                Log.WriteLog("signLocal",signLocal);
                #endregion
                if (notifyData.sign.Equals(signLocal)&&notifyData.pay_result.ToLower().Equals("success"))
                {
                    string out_trade_no = notifyData.out_trade_no;
                    ToPayOrder.PayOrder(out_trade_no);

                    //回调成功通知
                    var res = new {code="0",message="success" };
                    Log.WriteLog(this.GetType().ToString(), "order query success : " + JsonConvert.SerializeObject(res));
                    Response.Write(JsonConvert.SerializeObject(res));

                }
            }catch(Exception ex)
            {
                Log.WriteLog("wx_notice_json", ex.Message);
            }
        }
    }
}