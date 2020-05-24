using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Services.TimeServices;

namespace Havit.GoranG3.Services.TimeServices
{
	/// <summary>
	/// Poskytuje aktuální čas v časové zóně "Central Europe Standard Time".
	/// </summary>
	public class ApplicationTimeService : TimeZoneTimeServiceBase, ITimeService
	{
		/// <summary>
		/// Aktuální čas v časové zóně aplikace
		/// </summary>
		public static DateTime LocalNow => new ApplicationTimeService().GetCurrentTime();

		/// <summary>
		/// Vrací časovou zónu, pro kterou je poskytován aktuální čas. Vždy "Central Europe Standard Time".
		/// </summary>
		protected override TimeZoneInfo CurrentTimeZone => TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
	}
}
