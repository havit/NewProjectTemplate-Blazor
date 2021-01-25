using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Model.Finance;

namespace Havit.GoranG3.Services.Finance
{
	public interface IExchangeRateMapper
	{
		void MapFromExchangeRateDto(ExchangeRateDto exchangeRateDto, ExchangeRate exchangeRate);
		ExchangeRateDto MapToExchangeRateDto(ExchangeRate exchangeRate);
	}
}