using System.Threading.Tasks;
using LectureAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
	{
		private readonly IQueueService _queueService;

        public LectureController(IQueueService queueService)
		{
			_queueService = queueService;
		}

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "test LectureController";
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] string inputMessage)
		{
			return new ActionResult<bool>(await _queueService.PostValue(inputMessage)); 
		}
    }
}
