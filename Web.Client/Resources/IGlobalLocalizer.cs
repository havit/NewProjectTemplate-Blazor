using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources
{
	public interface IGlobalLocalizer : IStringLocalizer<GlobalLocalizer>
	{
		LocalizedString DeleteSuccess { get; }
		LocalizedString NewSuccess { get; }
		LocalizedString UpdateSuccess { get; }
	}
}