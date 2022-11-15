using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regions);
        }

        [HttpGet("{id:guid}")]
        [ActionName("GetSingleRegionAsync")]
        public async Task<IActionResult> GetSingleRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);            
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Name= addRegionRequest.Name,
                Code= addRegionRequest.Code,
                Area= addRegionRequest.Area,
                Lat= addRegionRequest.Lat,
                Long= addRegionRequest.Long,
                Population= addRegionRequest.Population,
            };

            region = await regionRepository.AddAsync(region);

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return CreatedAtAction(nameof(GetSingleRegionAsync), new {id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region == null) 
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            var region = new Models.Domain.Region
            {
                Code= updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area= updateRegionRequest.Area,
                Lat= updateRegionRequest.Lat,
                Long= updateRegionRequest.Long,
                Population= updateRegionRequest.Population,
            };

            region = await regionRepository.UpdateAsync(id, region);
            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }
    }
}
