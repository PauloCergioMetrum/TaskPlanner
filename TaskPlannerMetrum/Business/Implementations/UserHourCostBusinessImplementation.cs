using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SixLabors.ImageSharp.Processing.Processors.Filters;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.UserHourCostRepository;
using TaskPlannerMetrum.Repository.Users;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class UserHourCostBusinessImplementation : IUserHourCostBusiness
    {
        private readonly IUserHourCostRepository _repository;
        private readonly IUserRepository _repositoryUsers;

        public UserHourCostBusinessImplementation(IUserHourCostRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _repositoryUsers = userRepository;
        }

        public bool CreateHourCost(UserHourCosts userHourCost)
        {
            try
            {


                if (_repository.ExistUserCost(userHourCost.ID))
                {

                    return _repository.UpdateUserHourCost(userHourCost);
                }
                else
                {
                    return _repository.CreateUserHourCost(userHourCost);

                }


            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool DeleteHourCost(string id)
        {
            try
            {
                _repository.DeleteUserHourCost(id);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        public bool UpdateHourCost(UserHourCosts userHourCost)
        {
            try
            {
                _repository.UpdateUserHourCost(userHourCost);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<UserHourCosts> ListUserHoursCost(int userID)
        {
            try
            {
                return _repository.GetAllUserHourCost(userID);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<bool> CreatHoursCostByCSV(HoursCostCSV HoursCostCSV)
        {

            List<User> allUsers = _repositoryUsers.GetAllUsers();
            using (var reader = new StreamReader(HoursCostCSV.FileCSV.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await foreach (var record in csv.GetRecordsAsync<CsvRecord>())
                {

                    bool CreatHourasCost = _repository.CreateUserHourCost(new UserHourCosts
                    {
                        UserID = allUsers.Where(n => n.FullName.ToUpper() == record.COLABORADOR).Select(i => i.Id).FirstOrDefault(),
                        HourCost = record.HH,
                        StartDate  = HoursCostCSV.StartDate,
                        EndDate = HoursCostCSV.EndDate,
                        ID = Guid.NewGuid().ToString(),


                    });
                    if (CreatHourasCost)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public async Task<bool> CreatHoursCostByExcel(IFormFile excelFile, DateTime startDate, DateTime endDate)
        {
            List<Functions> allFunction = _repository.GetAllFunction();
            List<Management> allManagements = _repository.GetAllManagement();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            throw new Exception("Planilha não encontrada no arquivo Excel.");
                        }
                        List<User> allUsers = _repository.GetAllUsers();
                        int rowCount = worksheet.RowsUsed().Count();
                        for (int row = 3; row <= rowCount; row++)
                        {

                            string colaborador = worksheet.Cell(row, 2).GetValue<string>()?.Trim();
                            double hourCost = worksheet.Cell(row, 7).GetValue<double>();
                            string functionName = worksheet.Cell(row, 6).GetValue<string>();
                            string managementName = worksheet.Cell(row, 5).GetValue<string>();
                            var user = allUsers.FirstOrDefault(u => u.FullName.ToUpper() == colaborador?.ToUpper());
                            if (user != null)
                            {
                                var userHourCost = new UserHourCosts
                                {
                                    UserID = user.Id,
                                    HourCost = hourCost,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    ID = Guid.NewGuid().ToString()
                                };
                                bool createSuccess = CreateOrUpdate(userHourCost);
                                if (createSuccess)
                                {
                                    _repository.updateUser(userHourCost.UserID, functionName, managementName);
                                }

                                if (!createSuccess)
                                {
                                    throw new Exception("Falha ao criar UserHourCost no repositório.");
                                }
                            }

                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar arquivo: {ex.Message}");
                return false;
            }
           
        }

        public bool CreateOrUpdate(UserHourCosts userHourCost)
        {
            var allHoursCosts = _repository.GetAllHours();

            bool userExists = allHoursCosts.Any(u => u.UserID == userHourCost.UserID);
            bool dateExists = allHoursCosts.Any(s => s.StartDate == userHourCost.StartDate && s.EndDate == userHourCost.EndDate && userHourCost.UserID ==userHourCost.UserID);
            if (userExists && dateExists)
            {
                return _repository.UpdateUserHourCost(userHourCost);
            }
            return _repository.CreateUserHourCost(userHourCost);
        }







    }
}
