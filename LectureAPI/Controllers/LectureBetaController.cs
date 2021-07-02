using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LectureAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureBetaController : ControllerBase
    {
        private readonly QueueServiceBeta _queueService;

        public LectureBetaController(QueueServiceBeta queueService)
        {
            _queueService = queueService;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "test LectureBetaController";
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] string inputMessage)
        {
            return new ActionResult<bool>(await _queueService.PostValue(inputMessage));
        }
    }
}