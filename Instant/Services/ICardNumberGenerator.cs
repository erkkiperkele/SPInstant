namespace Instant.Services
{
    public interface ICardNumberGenerator
    {
        long GenerateCardNumber();
        void FreeCardNumber(long toFree);
    }
}
