using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Facades.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Havit.GoranG3.WebAPI.Controllers
{
    /// <summary>
    /// Controller pro systémové akce.
    /// </summary>
    public class SystemController : ControllerBase
    {
        private readonly IDataSeedFacade dataSeedFacade;

        public SystemController(IDataSeedFacade dataSeedFacade)
        {
            this.dataSeedFacade = dataSeedFacade;
        }

        /// <summary>
        /// Provede seedování dat zadaného profilu.
        /// Název profilu musí být celý název typu bez ohledu na velikost písmen.
        /// </summary>
        /// <param name="profile">Název profilu k seedování.</param>
        [HttpPost("api/system/seed/{profile}")]
        public void SeedData(string profile)
        {
            dataSeedFacade.SeedDataProfile(profile);
        }
	}
}
