using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.NewContract;

namespace TaskPlannerMetrum.Controllers
{


    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,1")]

    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;

   
        private IProjectsBusiness _projectBusiness;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectsBusiness projectBusiness)
        {
            _logger = logger;
            _projectBusiness = projectBusiness;

        }

      
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]      
        public IActionResult Get()
        {

            try
            {
                return Ok(_projectBusiness.FindAll());
            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }



        [HttpGet("ContractProjects")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult ContractProjects()
        {
            try
            {
                return Ok(_projectBusiness.GetAllContracts());

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }




        [HttpGet("ManagerPlanner")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
       
        public IActionResult GetManagerPlanner()
        {
            try
            {
                return Ok(_projectBusiness.FindPlannerManager());

            }catch (Exception e)
            {
                Logger.Log(e.Message, ELoggerType.Debug);

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]      
        public IActionResult Create(ProjectDTO project)
        {
            try
            {
                return Ok(_projectBusiness.Create(project));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.ToString());
            }


        }

        [HttpPut("UpdateStatus")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public bool UpdateStatus(TaskPlannerMetrum.Model.ProjectStauts newProject)
        {
            try
            {
                return _projectBusiness.UpdateStatus(newProject);

            }

            catch (Exception e)
            {
                Logger.Log(e.Message, ELoggerType.Debug);

                return false;
            }

        }
        [HttpGet("UserByDep")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UserByDep(string departament)
        {
            try
            {
                return Ok(_projectBusiness.UserByDep(departament));

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.ToString());    
            }
        }

        [HttpGet("projecprogress")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult ProgressProject()
        {
            try
            {
                return Ok(_projectBusiness.ProgressProject());

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.ToString());    
            }
        }

        [HttpGet("GetDepFinances")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetDepFinances(int id)
        {
            try
            {
                return Ok(_projectBusiness.GetDepFinances(id));

            }catch(Exception ex) 
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetAllUsersDep")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllUsersDep(string depname)
        {
            try
            {
                return Ok(_projectBusiness.GetAllUsersDep(depname));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("NewCreat")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult NewCreat(Model.NewContract.NewProject newproject)
        {
            try
            {
                return Ok(_projectBusiness.NewCreat(newproject));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);   
            }
        }

        [HttpGet("GetAllProjectDep")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllProjectDep(int contractID)
        {
            try
            {
                return Ok(_projectBusiness.GetAllProjectDep(contractID));

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);   
            }
        }
       
        [HttpPut("UpdateProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateProject(CreateProjectRetroactiveDate newProject )
        {
            try
            {
                return Ok(_projectBusiness.UpdateProject(newProject));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }


        }

        [HttpPut("ActiveProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult ActiveProject(int id)
        {
            try
            {
                return Ok(_projectBusiness.ActiveProject(id));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }


        [HttpGet("getActiveProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult getActiveProject()
        {
            try
            {
                return Ok(_projectBusiness.getActiveProject());

            }catch( Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            
            }

        }

        [HttpGet("getInfoProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult getInfoProject(int id)
        {
            try
            {
                return Ok(_projectBusiness.getInfoProject(id ));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);

            }

        }





        [HttpGet("FavoriteProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FavoriteProject(int ContractID, int UserID)
        {
            try
            {
                _projectBusiness.FavoriteProject(ContractID, UserID);
                return Ok();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);

            }

        }


        [HttpDelete("DeletFavoritProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeletFavoritProject(int ContractID, int UserID)
        {
            try
            {
                _projectBusiness.DeletFavoritProject(ContractID, UserID);
                return Ok();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);

            }

        }



        [HttpPut("UpdateRetroactiveDate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateRetroactiveDate(int contractID, DateTime retroactiveDate)
        {
            try
            {
                _projectBusiness.UpdateRetroactiveDate(contractID, retroactiveDate);
                return Ok();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);

            }

        }

        [HttpGet("GetAllContractProjectByTechLeader")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllContractProjectByTechLeader(int? techLeaderID, string InspectorName)
        {
            try
            {
                return Ok(_projectBusiness.GetAllContractProjectByTechLeader(techLeaderID, InspectorName));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }



        [HttpGet("Tapscope")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Tapscope(int ContractID)
        {
            try
            {
                return Ok(_projectBusiness.Tapscope(ContractID));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProjectStatus")]
        [ProducesResponseType(typeof(ProjectStatusInfo), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetProjectStatus([FromQuery] int id, [FromQuery] string internalCode)
        {
            try
            {
                var result = _projectBusiness.GetProjectStatusById(id, internalCode);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
       
                return BadRequest(ex.Message);
            }
        }
    }



}

