using CargoPay.Application.Interfaces;

namespace CargoPay.Application.Services
{
    public class FeeService : IFeeService
    {
        private readonly object _lock = new();
        private decimal _lastFee = 1.0m;
        private DateTime _lastUpdate = DateTime.MinValue;

        public decimal GetCurrentFee()
        {
            lock (_lock)
            {
                var now = DateTime.UtcNow;

                if ((now - _lastUpdate).TotalSeconds >= 30)
                {
                    var random = new Random();
                    var randomMultiplier = (decimal)random.NextDouble() * 2;

                    _lastFee = Math.Round(_lastFee * randomMultiplier, 4);
                    _lastUpdate = now;
                }

                return _lastFee;
            }
        }
    }


}
