using Havit.NewProjectTemplate.Model.Crm;

namespace Havit.NewProjectTemplate.DataLayer.Repositories.Crm
{
	public interface ICountryByIsoCodeLookupService
	{
		Country GetCountryByIsoCode(string isoCode);
	}
}