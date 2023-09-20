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
            List<RatingDTO> ratingDTOList = new List<RatingDTO>();            
            foreach (var executor in vRating)
            {
                if (!ratingDTOList.Any(s => s.UserID == executor.UserID) )
                {
                    ratingDTOList.Add(new RatingDTO
                    {
                        UserID = executor.UserID,
                        UserName = executor.UserName,
                        ProjectID = executor.ProjectID,
                        TeckLeader = executor.Type == "L" ? true : false,
                        

                        Rating = vRating.Where(u => u.UserID == executor.UserID).Select(r => new Model.DTO.RatingModel
                        {
                            RatingID = r.RatingID,
                           RatingName =r.RatingName,
                            RatingValue = r.RatingValue,
                            RatingDescriptionID = r.RatingDescriptionID,
                        }).ToList(),
                    }); 
                }

            }
            return ratingDTOList;

        }

       
        public bool UpdateRating(RatingDTOAll reatings) 

        {
            try
            {
                var rating = _context.Rating.Where(r => r.ID == reatings.RatingID).FirstOrDefault();
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






