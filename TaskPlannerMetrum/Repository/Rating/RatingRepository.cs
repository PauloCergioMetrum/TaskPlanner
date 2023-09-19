using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using MySqlConnector;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.Rating
{
    public class RatingRepository : IRatingRepository
    {

        private MSSQLContext _context;


        public RatingRepository(MSSQLContext context) { _context = context; }


        public dynamic GetAllRatingProject(int projectID)
        {
            List<Model.ModelViews.vRating> vRating = _context.vRating.Where(i => i.ProjectID ==projectID).ToList();
            List<RatingDTO> retorno = new List<RatingDTO>();            
            foreach (var executor in vRating)
            {
                if (!retorno.Any(s => s.UserID == executor.UserID) )
                {
                    retorno.Add(new RatingDTO
                    {
                        UserID = executor.UserID,
                        UserName = executor.UserName,
                        ProjectID = executor.ProjectID,
                        TeckLeader = executor.Type == "L" ? true : false,
                        Rating = vRating.Where(u => u.UserID == executor.UserID).Select(r => new Model.DTO.Rating
                        {
                            RatingName=r.RatingName,
                            UserName = r.UserName,
                            RatingValue = r.RatingValue
                        }).ToList(),
                    }); 
                }

            }
            return retorno;

        }
    }
}






