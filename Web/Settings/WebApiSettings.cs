using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLite;

namespace Havit.GoranG3.Web.Settings
{
    /// <summary>
    /// Konfigurace napojení na WebAPI.
    /// </summary>
    public class WebApiSettings
    {
        /// <summary>
        /// Url k serveru poskytující WebAPI.
        /// </summary>
        public string WebApiUrl { get; set; }

    }
}
