using CargoPay.Domain.Entities;

namespace CargoPay.Application.Interfaces
{
    public interface ICardService
    {
        Task<Card> CreateCardAsync();
        Task<decimal> GetBalanceAsync(string cardNumber);
        Task<Payment> PayAsync(string cardNumber, decimal amount);
    }

}
