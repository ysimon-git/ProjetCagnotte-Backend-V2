using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Application.Mappers;
using ProjetCagnotte.Infrastructure.Repositories;

namespace ProjetCagnotte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {

        private readonly IContributionService _contributionService;

        public ContributionController(IContributionService contributionService)
        {
            _contributionService = contributionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddContribution(CreateContributionDto dto)
        {
            await _contributionService.AddAsync(dto);
            return Ok(dto);
        }

    

        [HttpGet]
        public async Task<decimal> GetTotalAmountByProductIdAsync(int productID)
        {
            return await _contributionService.GetTotalAmountByProductIdAsync(productID);


        }



    }
}
