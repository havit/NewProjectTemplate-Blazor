using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
		/// Vrací časovou zónu, pro kterou je poskytován aktuální čas. Vždy "Central Europe Standard Time".
		/// </summary>
		protected override TimeZoneInfo CurrentTimeZone
		{
			get
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					return TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
				}
				return TimeZoneInfo.FindSystemTimeZoneById("Europe/Prague"); // MacOS
			}
		}
	}
}
