namespace CargoPay.Application.DTOS
{
    public class PayRequest
    {
        public required string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
