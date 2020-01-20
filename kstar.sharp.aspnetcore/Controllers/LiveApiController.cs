using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kstar.sharp.domain.Entities;
using kstar.sharp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kstar.sharp.aspnetcore.Controllers
{
    [Route("api/live")]
    [ApiController]
    public class LiveApiController : ControllerBase
    {
        private readonly DbService dbService;

        public LiveApiController(DbService dbService)
        {
            this.dbService = dbService;
        }

        // GET: api/live
        [HttpGet]
        public async Task<InverterDataGranular> Get()
        {
            InverterDataGranular vm = await dbService.GetLatest();
            return vm;
        }

        
    }
}
