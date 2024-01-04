using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.UserHourCostRepository;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class UserHourCostBusinessImplementation : IUserHourCostBusiness
    {
        private readonly IUserHourCostRepository _repository;

        public UserHourCostBusinessImplementation(IUserHourCostRepository repository)
        {
            _repository = repository;
        }

        public bool CreateHourCost(UserHourCosts userHourCost)
        {
            try
            {

               
                if(_repository.ExistUserCost(userHourCost.ID))
                {
              
                    return _repository.UpdateUserHourCost(userHourCost);
                }
                else
                {
                   return _repository.CreateUserHourCost(userHourCost);
                    
                }
                
               
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        

        public bool DeleteHourCost(string id)
        {
            try
            {
                _repository.DeleteUserHourCost(id);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }



        public bool UpdateHourCost(UserHourCosts userHourCost)
        {
            try
            {
                _repository.UpdateUserHourCost(userHourCost);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<UserHourCosts> ListUserHoursCost(int userID)
        {
            try
            {
                return _repository.GetAllUserHourCost(userID);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        



    }
}
