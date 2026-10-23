using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnOut.Application.Contracts;
using OnOut.Domain;

namespace OnOut.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/dietarychoices")]
public class DietaryChoiceController : ControllerBase
{
    private readonly IBaseRepository<DietaryChoice> _repository;

    public DietaryChoiceController(IBaseRepository<DietaryChoice> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<DietaryChoice>>> GetAll()
    {
        var dietaryChoices = await _repository.GetAllAsync();
        return Ok(dietaryChoices);
    }
}
