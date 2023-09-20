using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using MySqlConnector;
using System;
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
                        RatingID =executor.RatingID,

                        Rating = vRating.Where(u => u.UserID == executor.UserID).Select(r => new Model.DTO.RatingModel
                        {
                            
                           
                            RatingValue = r.RatingValue,
                            RatingDescriptionID = r.RatingDescriptionID,
                        }).ToList(),
                    }); 
                }

            }
            return retorno;

        }

       
        public dynamic UpdateRating(RatingDTOAll reatings)

        {
            try
            {
                // IDENTIFICAR QUAL O RATING ESTOU  ALTERANDO 
                var ratingID = _context.RatingProject.Where(r => r.ProjectID == reatings.ProjectID && r.UserID == reatings.UserId).Select(d => d.ID).FirstOrDefault();
                var  rating = _context.Rating.Where(r => r.RatingProjectID == ratingID &&  r.RatingDescriptionID== reatings.RatingDescriptioID).FirstOrDefault();




                if (rating != null)
                {
                    rating.Value = reatings.RatingValue;
                    _context.Rating.Update(rating);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception ex)
            {
                return false; 
            }
        }
        
           
        
    }
}






