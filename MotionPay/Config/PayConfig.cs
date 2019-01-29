/* Developer Qin  */
/* Email:lijie.qin@chchuan.com  */
namespace MotionPay.Config
{
    /// <summary>
    /// 支付相关参数配置
    /// </summary>
    public class PayConfig
    {
        public static string AppId = "";
        public static string AppSecret = "";
        public static string WECHAT_MINI_MID = "";
        public static string WECHAT_MINI_APPID = "";
        public static string WECHAT_MINI_APPSECURE = "";
        public static string Notify_Url = "";

        public static string BASE_URL = "https://online.motionpaytech.com/onlinePayment/v1_1/pay";
        public static string ORDER_MINI_URL = BASE_URL + "/mini";

        private static string apiHostURL = "https://online.motionpaytech.com/";
        /// <summary>
        /// 支付宝H5-请求地址
        /// </summary>
        public static string Alipay_H5_PAY = apiHostURL + "onlinePayment/v1_1/pay/wapPay";
        /// <summary>
        /// 支付宝H5-结果跳转地址
        /// </summary>
        public static string Alipay_PAY_URL = apiHostURL + "onlinePayment/v1_1/pay/getPayUrl";
        /// <summary>
        /// 支付宝H5--回调
        /// </summary>
        public static string Alipay_Notify_Url = "";
    }
}
