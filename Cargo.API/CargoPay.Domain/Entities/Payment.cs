namespace CargoPay.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CardId { get; set; }
        public decimal Amount { get; set; }
        public decimal FeeApplied { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        public Card Card { get; set; } = default!;
    }
}
