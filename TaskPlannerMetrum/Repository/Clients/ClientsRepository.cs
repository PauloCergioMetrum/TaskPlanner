
using System;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using ClientsModel = TaskPlannerMetrum.Model.Clients;

namespace TaskPlannerMetrum.Repository.Clients
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly MSSQLContext _context;

        public ClientsRepository(MSSQLContext context)
        {
            _context = context;
        }

        public string NormalizeCnpj(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return new string(value.Where(char.IsDigit).ToArray());
        }

        public ClientsModel GetByCnpjIncludingSoftDeleted(string normalizedCnpj)
        {
            return _context.Clients.FirstOrDefault(c => c.Cnpj == normalizedCnpj);
        }

        public bool CreateClients(ClientCreateDto dto)
        {
            if (dto == null) return false;

            var entity = new ClientsModel
            {
                Cnpj = NormalizeCnpj(dto.Cnpj),
                Name = dto.Name,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                Address = dto.Address,
                PMContactName = dto.PMContactName,
                PMPhoneNumber = dto.PMPhoneNumber,
                WorkspaceID = dto.WorkspaceID,
                SoftDelete = false,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Clients.Add(entity);
            _context.SaveChanges();

            return true;
        }

        public bool ReactivateClient(ClientsModel existing, ClientCreateDto dto)
        {
            if (existing == null || dto == null) return false;

            existing.SoftDelete = false;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.Cnpj = NormalizeCnpj(dto.Cnpj);
            existing.Name = dto.Name;
            existing.City = dto.City;
            existing.State = dto.State;
            existing.Country = dto.Country;
            existing.Address = dto.Address;
            existing.PMContactName = dto.PMContactName;
            existing.PMPhoneNumber = dto.PMPhoneNumber;
            existing.WorkspaceID = dto.WorkspaceID;

            _context.SaveChanges();

            return true;
        }

        public bool ExistClientIdInContracts(int id)
        {
            return _context.Contracts.Any(x => x.ClientID == id && (x.IsDeleted == null || x.IsDeleted == false));
        }

        public bool DeleteClientById(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id && (c.SoftDelete == null || c.SoftDelete == false));
            if (client == null) return false;

            client.SoftDelete = true;
            client.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }

        public bool UpdateClients(ClientsModel clients)
        {
            if (clients == null) return false;
            if (clients.Id <= 0) return false;

            var existing = _context.Clients.FirstOrDefault(c => c.Id == clients.Id);
            if (existing == null) return false;

            existing.Name = clients.Name;
            existing.City = clients.City;
            existing.State = clients.State;
            existing.Country = clients.Country;
            existing.Address = clients.Address;
            existing.PMContactName = clients.PMContactName;
            existing.PMPhoneNumber = clients.PMPhoneNumber;
            existing.WorkspaceID = clients.WorkspaceID;
            existing.UpdatedAt = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(clients.Cnpj))
                existing.Cnpj = NormalizeCnpj(clients.Cnpj);
            _context.SaveChanges();

            return true;
        }

        public string UpsertClientByCnpj(ClientCreateDto dto)
        {
            if (dto == null) return null;

            var normalized = NormalizeCnpj(dto.Cnpj);
            if (string.IsNullOrWhiteSpace(normalized)) return null;

            var existing = _context.Clients.FirstOrDefault(c => c.Cnpj == normalized);

          
            if (existing == null)
            {
                var entity = new ClientsModel
                {
                    Cnpj = normalized,
                    Name = dto.Name,
                    City = dto.City,
                    State = dto.State,
                    Country = dto.Country,
                    Address = dto.Address,
                    PMContactName = dto.PMContactName,
                    PMPhoneNumber = dto.PMPhoneNumber,
                    WorkspaceID = dto.WorkspaceID,
                    SoftDelete = false,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Clients.Add(entity);
                _context.SaveChanges();
                return "created";
            }

     
            if (existing.SoftDelete == null || existing.SoftDelete == false)
            {
                return "exists";
            }

         
            existing.SoftDelete = false;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.Cnpj = normalized;
            existing.Name = dto.Name;
            existing.City = dto.City;
            existing.State = dto.State;
            existing.Country = dto.Country;
            existing.Address = dto.Address;
            existing.PMContactName = dto.PMContactName;
            existing.PMPhoneNumber = dto.PMPhoneNumber;
            existing.WorkspaceID = dto.WorkspaceID;

            _context.SaveChanges();
            return "reactivated";
        }

    }
}
