using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Controllers
{
    [Route("api/[controller]/v{version:apiVersion}")]
    public class CustomerSatisfactionController:ControllerBase
    {

        private readonly ICustomerSatisfactionBusiness _customerSatisfactionBusiness;

        public CustomerSatisfactionController(ICustomerSatisfactionBusiness customerSatisfactionBusiness)
        {
          
            _customerSatisfactionBusiness = customerSatisfactionBusiness;   

        }

        [HttpGet("CustomerSatisfaction")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CustomerSatisfaction(int ContractID)
        {
            try
            {
                return Ok(_customerSatisfactionBusiness.ClientFeedbackDetailsModel(ContractID));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("CreateCustomerFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateCustomerFeedback (DateTime? FeedbackDate, int contractID, string clientResponse, int? clientRating, string receivedComplaint , int? Status)
        {
            try
            {
                return Ok(_customerSatisfactionBusiness.CreateCustomerFeedback(FeedbackDate, contractID ,clientResponse ,clientRating,receivedComplaint , Status));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetCustomerSatisfactions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CustomerSatisfactions()
        {
            try
            {
                return Ok(_customerSatisfactionBusiness.CustomerSastifaction());
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }





    }
}
