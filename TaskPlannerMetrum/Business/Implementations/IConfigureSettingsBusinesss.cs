using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business.Implementations
{
    public interface IConfigureSettingsBusinesss
    {
        public bool UpdateConectionString(ConfigureSettingsDTO configureSettings);
    }
}
