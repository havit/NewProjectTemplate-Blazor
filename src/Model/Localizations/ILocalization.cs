namespace Havit.NewProjectTemplate.Model.Localizations;

public interface ILocalization<TLocalizedEntity> : Havit.Model.Localizations.ILocalization<TLocalizedEntity, Language>
{
	new TLocalizedEntity Parent { get; set; }
	int ParentId { get; set; }

	new Language Language { get; set; }
	int LanguageId { get; set; }

}
