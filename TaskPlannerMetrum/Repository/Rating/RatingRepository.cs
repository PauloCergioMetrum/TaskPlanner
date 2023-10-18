using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Xml.Schema;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.Rating
{
    public class RatingRepository : IRatingRepository
    {

        private MSSQLContext _context;


        public RatingRepository(MSSQLContext context) { _context = context; }


        public dynamic GetAllRatingProject(int projectID, int? userID)
        {
            List<Model.ModelViews.vRating> vRating = _context.vRating.Where(i => i.ProjectID == projectID && i.UserID != userID && i.Type != "M").ToList();
            List<RatingDTO> ratingDTOList = new List<RatingDTO>();
            foreach (var executor in vRating)
            {
                if (!ratingDTOList.Any(s => s.UserID == executor.UserID))
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
                            RatingName = r.RatingName,
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
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public bool IsTechLeader(int userID, int contractId)
        {
            try
            {

                var user = _context.DepartmentProjects.Where(t => t.ContractID == contractId && t.TechLeaderID == userID).FirstOrDefault();

                if (user != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public void CreateRatings(bool techLeader, int userID, int contractID)
        {
            try
            {
                var ratingList = _context.RatingDescription.ToList();

                if (techLeader)
                {
                    ratingList = ratingList.Where(r => r.Type == "L").ToList();
                    int ratingProjectID = _context.RatingProject.Where(r => r.ProjectID == contractID && r.UserID == userID).Select(s => s.ID).FirstOrDefault();
                    foreach (var rating in ratingList)
                    {
                        if (_context.Rating.Where(r => r.RatingDescriptionID == rating.ID  && r.RatingProjectID == ratingProjectID).FirstOrDefault() == null)
                        {
                            _context.Rating.Add(new Model.Rating { RatingDescriptionID = rating.ID, RatingProjectID = ratingProjectID, Value = 0 });

                        }

                    }
                }
                else
                {
                    ratingList = ratingList.Where(r => r.Type == "E").ToList();
                    int ratingProjectID = _context.RatingProject.Where(r => r.ProjectID == contractID && r.UserID == userID).Select(s => s.ID).FirstOrDefault();
                    foreach (var rating in ratingList)
                    {
                        if (_context.Rating.Where(r => r.RatingDescriptionID == rating.ID  && r.RatingProjectID == ratingProjectID).FirstOrDefault() == null)
                        {
                            _context.Rating.Add(new Model.Rating { RatingDescriptionID = rating.ID, RatingProjectID = ratingProjectID, Value = 0 });

                        }
                    }
                }

                _context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public void CreateRatingProjects(int userId, int contractId)
        {
            try
            {
                if (_context.RatingProject.Where(p => p.ProjectID == contractId && p.UserID == userId).FirstOrDefault() == null)
                {
                    _context.RatingProject.Add(new RatingProject { UserID = userId, ProjectID = contractId });
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public bool ExistRating(int userID, int contractId)
        {
            try
            {
                var ratingExist = _context.vRating.Where(r => r.ProjectID == contractId && r.UserID == userID).FirstOrDefault();



                if (ratingExist != null)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {

            }
            return false;
        }



        public bool ExistLeaderID(int contractID)
        {
            try
            {   // consulta onde  retorna a lista de TeacLeaders , da DepartmentProjects baseada  no contractID
                var techLeaders = _context.DepartmentProjects.Where(d => d.ContractID == contractID  && d.TechLeaderID  !=0).ToList();

                var ratingDescription = _context.RatingDescription.Where(t => t.Type == "L").ToList();

                foreach (var teachLeader in techLeaders)
                {
                    var teamID = _context.Team.Where(t => t.ID == teachLeader.TechLeaderID).FirstOrDefault();

                    var ratingProject = _context.RatingProject.Where(p => p.ProjectID == contractID && p.UserID == teamID.UserID).FirstOrDefault();
                    if (ratingProject == null)
                    {
                        _context.RatingProject.Add(new RatingProject { UserID = teamID.UserID, ProjectID = teachLeader.ContractID });
                        _context.SaveChanges();
                        //Cadastrar na rating as avaliações 

                        var userRatingProjectID = _context.RatingProject.Where(p => p.ProjectID == teachLeader.ContractID && p.UserID == teamID.UserID).FirstOrDefault();
                        foreach (var description in ratingDescription)
                        {
                            _context.Rating.Add(new Model.Rating
                            {
                                RatingDescriptionID = description.ID,
                                RatingProjectID = userRatingProjectID.ID,
                                Value = 0


                            });
                            _context.SaveChanges();
                        }

                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;


            }



        }

        public void UpdateTechLeader(int contractID)
        {
            var techLeaderes = _context.DepartmentProjects.Where(c => c.ContractID == contractID).ToList();

            foreach (var leader in techLeaderes)
            {
                var teanList = _context.Team.Where(t => t.ID == leader.TechLeaderID).Select(i => i.UserID).FirstOrDefault();

                if (_context.RatingProject.Where(p => p.ProjectID == contractID && p.UserID ==teanList) == null)
                {

                    _context.RatingProject.Add(new Model.RatingProject
                    {
                        ProjectID =contractID,
                        UserID = teanList,

                    });
                    _context.SaveChanges();

                }
            }
        }


        public void UpdateManager(int contractID)
        {
            var getManager = _context.Contracts.Where(c=> c.id == contractID).FirstOrDefault();
            var getDescriptions = _context.RatingDescription.Where(t => t.Type == "M").ToList();
            if(_context.RatingProject.Where(p=> p.ProjectID == contractID && p.UserID == getManager.inspectorID).FirstOrDefault() == null)
            {
                _context.RatingProject.Add(new RatingProject
                {
                    ProjectID=contractID,
                    UserID=getManager.inspectorID,
                });
                _context.SaveChanges(); 
            }
            var getManegarRatingProjectID = _context.RatingProject.Where(r => r.ProjectID == contractID && r.UserID == getManager.inspectorID).FirstOrDefault();
            foreach (var description in getDescriptions)
            {
                if (_context.Rating.Where(r => r.RatingDescriptionID == description.ID  && r.RatingProjectID == getManegarRatingProjectID.ID).FirstOrDefault() == null)
                {
                    _context.Add(new Model.Rating
                    {
                        RatingProjectID = getManegarRatingProjectID.ID,
                        RatingDescriptionID = description.ID,
                    }) ;
                    _context.SaveChanges(); 
                }
            }


        }

        public dynamic GetRatingManager(int contractID)
        {
            List<Model.ModelViews.vRating> vRating = _context.vRating.Where(i => i.ProjectID == contractID && i.Type == "M").ToList();
            List<RatingDTO> ratingDTOList = new List<RatingDTO>();
            foreach (var executor in vRating)
            {
                if (!ratingDTOList.Any(s => s.UserID == executor.UserID))
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
                            RatingName = r.RatingName,
                            RatingValue = r.RatingValue,
                            RatingDescriptionID = r.RatingDescriptionID,
                        }).ToList(),
                    });
                }

            }
            return ratingDTOList;
        }
    }
}

















