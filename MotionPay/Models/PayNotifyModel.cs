namespace MotionPay.Models
{
    public class PayNotifyModel
    {
        /// <summary>
        /// 币种
        /// </summary>
        public string currency_type { get; set; }
        /// <summary>
        /// 交易汇率
        /// </summary>
        public decimal exchange_rate { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mid { get; set; }
        /// <summary>
        /// 第三方订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public string pay_channel { get; set; }
        /// <summary>
        /// 支付结果
        /// </summary>
        public string pay_result { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal settlement_amount { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 第三方订单号
        /// </summary>
        public string third_order_no { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal total_fee { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string user_identify { get; set; }
    }
}