namespace Havit.NewProjectTemplate.Model.Localizations;

/// <summary>
/// Lokalizace.
/// </summary>
public interface ILocalization<TLocalizedEntity> : Havit.Model.Localizations.ILocalization<TLocalizedEntity, Language>
{
	new TLocalizedEntity Parent { get; set; } // new - Havit.Model.Localizations.ILocalization<,> jiĹľ mĂˇ vlastnost Parent
	int ParentId { get; set; }

	new Language Language { get; set; } // new - Havit.Model.Localizations.ILocalization<,> jiĹľ mĂˇ vlastnost Language
	int LanguageId { get; set; }

}
