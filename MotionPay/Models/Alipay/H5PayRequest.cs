namespace MotionPay.Models.Alipay
{
    public class H5PayRequest
    {
        public string mid { get; set; }
        public string sign { get; set; }
        public string goods_info { get; set; }
        public string out_trade_no { get; set; }
        public string pay_channel { get; set; }
        public string return_url { get; set; }
        public string spbill_create_ip { get; set; }
        public string terminal_no { get; set; }
        public int total_fee { get; set; }
        public string wap_url { get; set; }
    }
}
