using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimViec.Respository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimViec.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class APIJobController : ControllerBase
	{
		private readonly IJobRespository _jobRepository;
		private readonly ICompanyRespository _companyRespository;
		// GET: api/<HomeController>

		public APIJobController(IJobRespository jobRepository,
			ICompanyRespository companyRespository)
		{
			_jobRepository = jobRepository;
			_companyRespository = companyRespository;
		}

		//[HttpGet]
		//public async Task<IActionResult> GetJobs()
		//{
		//	var jobs = await _jobRepository.GetAllAsync();
		//	return Ok(jobs);
		//}
		
		[HttpGet]
		public async Task<IActionResult> GetCompany()
		{
			var cpn = await _companyRespository.GetAllAsync();
			return Ok(cpn);
		}

		// GET api/<HomeController>/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var getid = await _jobRepository.GetByIdAsync(id);
			return Ok(getid);
		}
		// POST api/<JobController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<JobController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<JobController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
