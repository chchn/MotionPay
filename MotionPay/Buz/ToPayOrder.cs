using System;
using TuYu.Common;

namespace MotionPay.Buz
{
    public class ToPayOrder
    {
        /// <summary>
        /// 支付订单--业务
        /// </summary>
        /// <param name="out_trade_no"></param>
        public static void PayOrder(string out_trade_no)
        {
            long orderId = 0;
            long.TryParse(out_trade_no, out orderId);
            try
            {
            }
            catch (Exception ex)
            {
                Log.WriteLog("订单回调错误", ex.Message);
            }
        }
    }
}