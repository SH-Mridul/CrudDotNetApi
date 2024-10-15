using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
	// https://localhost:postNumber/api/students
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAllStudents()
		{
			string[] students = new string[] {"Mridul","Maruf","Sakib","Jahid","Rana","Bijoy","Mubassir","Ali","Jisan"};
			return Ok(students); //ok means successfull method which produce 200
		}
	}
}
