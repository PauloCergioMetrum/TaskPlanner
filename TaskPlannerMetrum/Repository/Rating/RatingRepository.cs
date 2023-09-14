using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MySqlConnector;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;

namespace TaskPlannerMetrum.Repository.Rating
{
    public class RatingRepository :IRatingRepository
    {

        private MSSQLContext _context;


        public RatingRepository(MSSQLContext context) { _context = context; }
        public  dynamic  GetAllRatingforUser (int projectID)
        {
            List<dynamic> retorno = new List<dynamic>();
            List<int> executorID = _context.ActivityPlan.Where(c => c.ContractID == projectID).Select(e => e.ExecutorTeamID).ToList();
            List<Model.Rating> listRating = _context.Rating.ToList();


            List<dynamic> rating = new List<dynamic>();
            foreach(var executor in  executorID)
            {
               if(retorno.Where( e=> e.ExecutorTeamID == executor).Count() == 0)
                {
                    retorno.Add(new
                    {
                        ExecutorTeamID = executor,
                        ExecutorName = _context.Users.Where(u => u.Id == _context.Team.Where(u => u.ID == executor).Select(u => u.UserID).FirstOrDefault()).Select(n => n.FullName).FirstOrDefault(),
                        Rating = setDescriptionRating(executor, projectID),
                        isLeader = _context.Contracts.Where(c => c.id == projectID && c.inspectorID == executor).FirstOrDefault() == null ? false : true

                    }); ;

                }
            }
            return retorno;
        }

        public List<dynamic>  setDescriptionRating( int executorID, int projectID)
        {
            List<Model. Rating> ratings = _context.Rating.Where(e=> e.ExecutorTeamID ==executorID && e.RatingProjectID == _context.RatingProject.Where(p => p.ProjectID == projectID).Select(p => p.ID).FirstOrDefault()).ToList();
            List<dynamic> retorno = new List<dynamic>();
            foreach(var value in ratings) 
            {
                
                
                    retorno.Add(new
                    {
                        Description = _context.RatingDescription.Where(i => i.ID == value.RatingDescriptionID).Select(d => d.Description).FirstOrDefault(),
                        DescriptionID = value.RatingDescriptionID,
                        value = value.Value,
                        ExecutTeamID = value.ExecutorTeamID,
                        UserName = _context.Users.Where(u => u.Id == _context.Team.Where(i => i.ID == executorID).Select(u => u.UserID).FirstOrDefault()).Select(n => n.FullName).FirstOrDefault(),

                    });
                
                
            }
            return retorno.ToList();
        }
        


       
    }
}






