using CrudApi.Data;
using CrudApi.Models.Domain;
using CrudApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
	//https://localhost:1234/api/regions
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly  CrudDbContext _context;
        public RegionsController(CrudDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
		public IActionResult GetAll()
		{
			//get data from database - domain models
			var regionsDomain = _context.Regions.ToList();

			//map domain models to DTOs
			var regionsDto = new List<RegionDto>();
			foreach (var regionDomain in regionsDomain)
			{
				regionsDto.Add(new RegionDto
				{
					Id = regionDomain.Id,
					Name = regionDomain.Name,
					Code = regionDomain.Code,
					RegionImageUrl = regionDomain.RegionImageUrl
				});
			}

			return Ok(regionsDto);
		}

		//get single region(get region by id)
		[HttpGet]
		[Route("{id:Guid}")]
		public IActionResult GetById([FromRoute] Guid id)
		{
			// get region from domain model
			var regionDomain = _context.Regions.Find(id);

			// checking null
			if(regionDomain == null)
				return NotFound();

			// map dto
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Name = regionDomain.Name,
				Code = regionDomain.Code,
				RegionImageUrl = regionDomain.RegionImageUrl
			};

			//return DTO back client
			return Ok(regionDto);
		}


		//POST to create new reagion
		//POST https://localhost:1234/api/regions
		[HttpPost]
		public IActionResult Create([FromBody] AddRegionRequestDto region)
		{

			// Map with the domain model
			var regionDomain = new Region
			{
				Code = region.Code,
				Name = region.Name,
				RegionImageUrl = region.RegionImageUrl
			};

			//use domain model to creat a region
			_context.Regions.Add(regionDomain);
			_context.SaveChanges();

			//map domain model back to dto
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImageUrl = regionDomain.RegionImageUrl
			};

            Console.WriteLine(nameof(GetById));

			return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}

		// DELETE https://localhost:1234/api/regions/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		public IActionResult Delete([FromRoute] Guid id)
		{
			// Get the region from the domain model
			var regionDomain = _context.Regions.Find(id);

			// Check if the region exists
			if (regionDomain == null)
				return NotFound();

			// Remove the region from the database
			_context.Regions.Remove(regionDomain);
			_context.SaveChanges();

			// Return NoContent after successful deletion
			return NoContent();
		}


		// PUT https://localhost:1234/api/regions/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto region)
		{
			// Get the existing region from the database
			var regionDomain = _context.Regions.Find(id);

			// Check if the region exists
			if (regionDomain == null)
				return NotFound();

			// Update the region's properties
			regionDomain.Name = region.Name;
			regionDomain.Code = region.Code;
			regionDomain.RegionImageUrl = region.RegionImageUrl;

			// Save the changes to the database
			_context.SaveChanges();

			// Map the updated domain model to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImageUrl = regionDomain.RegionImageUrl
			};

			// Return the updated region DTO
			return Ok(regionDto);
		}


	}
}
