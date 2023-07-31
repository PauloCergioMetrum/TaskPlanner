using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.ModelViews;

using System.Data;
using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Policy;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Repository.ProjectProgress
{
    public class DashboardRepository : IDashboardRepository
    {
        private MSSQLContext _context;
        public DashboardRepository(MSSQLContext context)
        {
            _context = context;
        }
        public dynamic GetAllStatus(){
            
            var allprojects = _context.Projects.ToList();
            var concluido = _context.Projects.Where(i => i.Status == "1").ToList();
            var progress = _context.Projects.Where(i => i.Status == "2").ToList();
            var notstarted = _context.Projects.Where(i => i.Status == "5").ToList();
            
            double statusfinal = ((double)concluido.Count() / allprojects.Count()) * 100;
            double statusProgress = ((double)progress.Count() / allprojects.Count()) * 100;
            double statusNotStarted = ((double)notstarted.Count() / allprojects.Count()) * 100;

            var Concluido = new
            {
                status = "Finalizado",
                porcentagem = Convert.ToDouble(statusfinal.ToString("F2")),
                color = "#248708"
            };
            var EmProgress = new
            {
                status = "Em progresso",
                porcentagem = Convert.ToDouble(statusProgress.ToString("F2")),
                color =  "#FFB020"
            };
            var NotStarted = new
            {
                status = "Não iniciado",
                porcentagem = Convert.ToDouble(statusNotStarted.ToString("F2")),
                color = "#5048E5"
            };
            var result = new List<object> { Concluido, EmProgress, NotStarted };

            return result ;
        }
    }
}
