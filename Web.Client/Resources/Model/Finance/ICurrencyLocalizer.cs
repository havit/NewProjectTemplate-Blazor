using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	public interface ICurrencyLocalizer : IStringLocalizer<CurrencyLocalizer>
	{
		public LocalizedString Code { get; }
		public LocalizedString DefaultBankAccount { get; }
		public LocalizedString Delete { get; }
		public LocalizedString DeleteConfirmation { get; }
		public LocalizedString New { get; }
		public LocalizedString Plural { get; }
		public LocalizedString Singular { get; }
	}
}