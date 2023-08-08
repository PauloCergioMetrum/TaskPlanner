using System;

namespace TaskPlannerMetrum.Repository.ContractViewer
{
    public interface IContractViewerRepository
    {

        dynamic GetAllContractsView(string Date);
    }
}
