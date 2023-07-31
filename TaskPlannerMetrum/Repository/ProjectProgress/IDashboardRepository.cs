using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectProgress
{
    public interface IDashboardRepository
    {
        public dynamic GetAllStatus ();  
    }
}
