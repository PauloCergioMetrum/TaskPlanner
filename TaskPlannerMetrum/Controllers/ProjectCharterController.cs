using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlannerMetrum.Business;

namespace TaskPlannerMetrum.Controllers
{
  [ApiController]
  [Route("api/[controller]/v{version:apiVersion}")]  
  public class ProjectCharterController : ControllerBase
  {
    private readonly IProjectCharterBusiness _projectCharterBusiness;

    public ProjectCharterController(IProjectCharterBusiness projectCharterBusiness)
    {
      _projectCharterBusiness = projectCharterBusiness;
    }
    [HttpGet("ProjectCharterDatails")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult ProjectCharterDatails(int id )
    {
      return Ok(_projectCharterBusiness.ProjectCharterDatails(id));
    }
  }
}
