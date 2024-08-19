using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Controllers
{
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

        [HttpPost("CreateCustomerFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateCustomerFeedback( int ContractID, string clientResponse, int? clientRating, string receivedComplaint)
        {
            try
            {
                return Ok(_customerSatisfactionBusiness.CreateCustomerFeedback( ContractID ,clientResponse ,clientRating,receivedComplaint));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

    }
}
