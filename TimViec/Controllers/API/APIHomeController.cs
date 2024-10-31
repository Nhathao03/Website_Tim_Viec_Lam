using Microsoft.AspNetCore.Mvc;
using TimViec.Respository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimViec.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class APIHomeController : ControllerBase
	{
		private readonly IJobRespository _jobRepository;
		// GET: api/<HomeController>

		public APIHomeController(IJobRespository jobRepository)
		{
			_jobRepository = jobRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetJobs()
		{
			var jobs = await _jobRepository.GetAllAsync();
			return Ok(jobs);
		}

		// GET api/<HomeController>/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{	
			var getid = await _jobRepository.GetByIdAsync(id);
			return Ok(getid);
		}

		// POST api/<HomeController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<HomeController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<HomeController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
