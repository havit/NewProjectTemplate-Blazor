using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Model.Finance;

namespace Havit.GoranG3.Services.Finance
{
	public interface ICurrencyMapper
	{
		CurrencyDto MapToCurrencyDto(Currency currency);
		void MapFromCurrencyDto(CurrencyDto currencyDto, Currency currency);
	}
}