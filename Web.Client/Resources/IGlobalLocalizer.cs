using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources
{
	public interface IGlobalLocalizer : IStringLocalizer<GlobalLocalizer>
	{
		LocalizedString DeleteSuccess { get; }
		LocalizedString NewSuccess { get; }
		LocalizedString UpdateSuccess { get; }
		LocalizedString DeleteConfirmation { get; }
		LocalizedString DeleteRecord { get; }
		LocalizedString NewRecord { get; }
		LocalizedString SelectNull { get; }
		LocalizedString SelectNullItems { get; }
	}
}