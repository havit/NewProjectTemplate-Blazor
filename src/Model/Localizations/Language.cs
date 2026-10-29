using System.ComponentModel.DataAnnotations;
using Havit.Model.Localizations;

namespace Havit.NewProjectTemplate.Model.Localizations;

public class Language : ILanguage
{
	public int Id { get; set; }

	[MaxLength(200)]
	public string Name { get; set; }

	[MaxLength(10)]
	public string Culture { get; set; }

	[MaxLength(10)]
	public string UiCulture { get; set; }

	public enum Entry
	{
		Czech = -1,
		English = -2
	}
}
