
using Havit.NewProjectTemplate.Model.Common;

namespace Havit.NewProjectTemplate.DataLayer.Repositories.Common;

public interface ICountryByIsoCodeLookupService
{
	Country GetCountryByIsoCode(string isoCode);
}
