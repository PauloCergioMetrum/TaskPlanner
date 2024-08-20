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
using System.Text;
using System.Threading.Tasks;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.UserHourCostRepository;
using TaskPlannerMetrum.Repository.Users;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public async Task<bool> CreatHoursCostByExcel(IFormFile excelFile, DateTime startDate, DateTime endDate)
        {
            List<Functions> functionsList = _repository.GetAllFunction();
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
                            return false;
                        }
                        List<User> allUsers = _repository.GetAllUsers();
                        int rowCount = worksheet.RowsUsed().Count();
                        for (int row = 3; row <= rowCount; row++)
                        {
                            string colaborador = worksheet.Cell(row, 2).GetValue<string>()?.Trim();
                            double hourCost;
                            try
                            {
                                hourCost = worksheet.Cell(row, 7).GetValue<double>();
                            }
                            catch (Exception ex)
                            {
                                return false;
                            }
                            string functionName = worksheet.Cell(row, 6).GetValue<string>();
                            functionName = RemoveAccents(functionName);
                            int functionID = functionsList.Where(n=> n.Name.ToUpper().Replace(" ", "") == functionName.ToUpper().Replace(" ","")).Select(i=> i.ID).FirstOrDefault();
                            string managementName = worksheet.Cell(row, 5).GetValue<string>();
                            var user = allUsers.FirstOrDefault(u => RemoveDiacritics(u.FullName).ToUpper() == RemoveDiacritics(colaborador)?.ToUpper());
                            if (user != null)
                            {
                                var userHourCost = new UserHourCosts
                                {
                                    UserID = user.Id,
                                    HourCost = hourCost,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    ID = Guid.NewGuid().ToString(),
                                    FunctionID = functionID,    
                                };
                                bool createSuccess = CreateOrUpdate(userHourCost);
                                if (createSuccess)
                                {
                                    _repository.updateUser(userHourCost.UserID, functionID, managementName);
                                }
                                else
                                {
                                    return false;
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
            var  userExists = allHoursCosts.Where(u => u.UserID == userHourCost.UserID);
            var dateExists = allHoursCosts.Where(s => s.StartDate == userHourCost.StartDate && s.EndDate == userHourCost.EndDate && s.UserID ==userHourCost.UserID).ToList().FirstOrDefault();
            if (dateExists != null)
            {
                return _repository.UpdateUserHourCost(userHourCost);
            }
            return _repository.CreateUserHourCost(userHourCost);
        }

        public string RemoveAccents(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in text)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }







    }
}
