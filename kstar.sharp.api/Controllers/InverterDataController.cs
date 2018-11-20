using kstar.sharp.domain.Entities;
using kstar.sharp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace kstar.sharp.api.Controllers
{
    [Route("api/inverter-data")]
    [ApiController]
    public class InverterDataController : ControllerBase
    {
        private readonly DbService _dbService;

        public InverterDataController(DbService dbService)
        {
            _dbService = dbService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<InverterDataGranular>> Get(DateTime start, DateTime end)
        {
            //DateTimeOffset start = DateTimeOffset.Now.AddDays(-1).Date + new TimeSpan(0, 0, 0);
            //DateTimeOffset end = DateTimeOffset.Now.AddDays(-1).Date + new TimeSpan(23, 59, 59);

            var results = _dbService.Get(start, end);

            return results;
        }


        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

    }
}
