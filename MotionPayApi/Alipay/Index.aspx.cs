using System;
using TuYu.Common;
using MotionPay;
namespace MotionPayApi.Alipay
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            decimal paymentAmount = Convert.ToDecimal(0.01);
            string paymentType = "H5_A";
            string productName = "Test Product";
            string wap_URL = "http://m.babynaiba.com/alipay/index.aspx?orderId="+ orderId;
            Log.WriteLog("AliPay", "wap_URL is:" + wap_URL);
            string pay_URL =AlipayClient.GenerateH5URL(paymentType, orderId, paymentAmount, productName, wap_URL);
            Log.WriteLog("AliPay", "pay_URL is:" + pay_URL);
            Response.Write(pay_URL);
        }
    }
}