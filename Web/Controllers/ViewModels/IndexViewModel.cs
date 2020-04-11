using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLite;

namespace Havit.GoranG3.Web.Controllers.ViewModels
{
    /// <summary>
    /// ViewModel pro klientskou aplikaci.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Předáno do taghelperu asp-prerender-data.
        /// </summary>
        public AspPrerenderData AspPrerenderDataObject { get; set; }

		[TsClass]
		public class AspPrerenderData
        {
            /// <summary>
            /// Url k serveru poskytující WebAPI.
            /// Za vyrenderováno jako atribut aspnet-prerender-webapiurl.
            /// </summary>
            public string WebApiUrl { get; set; }

        }
    }
}
