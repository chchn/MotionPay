namespace MotionPay.Models
{
    public class ResponseModel<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T content { get; set; }
    }
}
