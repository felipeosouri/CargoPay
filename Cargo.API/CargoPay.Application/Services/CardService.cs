using CargoPay.Application.Exceptions;
using CargoPay.Application.Interfaces;
using CargoPay.Domain.Entities;
using CargoPay.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CargoPay.Application.Services
{
    public class CardService : ICardService
    {
        private readonly CargoPayDbContext _context;
        private readonly IFeeService _feeService;
        private readonly ILogger<CardService> _logger;
        private static readonly Random _random = new();


        public CardService(CargoPayDbContext context, IFeeService feeService, ILogger<CardService> logger)
        {
            _context = context;
            _feeService = feeService;
            _logger = logger;
        }

        public async Task<Card> CreateCardAsync()
        {
            var cardNumber = string.Concat(Enumerable.Range(0, 15).Select(_ => _random.Next(0, 10)));

            var balance = Math.Round((decimal)(_random.NextDouble() * 90 + 10), 2);

            var card = new Card { CardNumber = cardNumber, Balance = balance };
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<decimal> GetBalanceAsync(string cardNumber)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber)
                ?? throw new KeyNotFoundException("Card not found.");

            return card.Balance;
        }

        public async Task<Payment> PayAsync(string cardNumber, decimal amount)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            var card = await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
            if (card is null)
                throw new KeyNotFoundException("The card does not exist.");

            var fee = _feeService.GetCurrentFee();
            var total = amount + fee;

            if (card.Balance < total)
                throw new BusinessException("The card balance is insufficient to make the payment.");

            card.Balance -= total;

            var payment = new Payment
            {
                CardId = card.Id,
                Amount = amount,
                FeeApplied = fee
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Successful payment. Card: {CardNumber}, Amount: {Amount}, Fee: {Fee}",
                card.CardNumber, amount, fee);

            return payment;
        }
    }

}
