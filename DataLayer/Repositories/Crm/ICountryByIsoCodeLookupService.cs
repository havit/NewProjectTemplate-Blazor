using Havit.GoranG3.Model.Crm;

namespace Havit.GoranG3.DataLayer.Repositories.Crm
{
	public interface ICountryByIsoCodeLookupService
	{
		Country GetCountryByIsoCode(string isoCode);
	}
}