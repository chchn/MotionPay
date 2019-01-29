using MotionPay.Config;
using MotionPay.Utility;
using System;

namespace MotionPayApi.Pay
{
    public partial class Index : ServicePage
    {
        protected static string WECHAT_APPID = PayConfig.AppId;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}