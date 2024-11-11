using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
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

        public List<UserHourCostsDTO> ListUserHoursCost(int userID)
        {
            try
            {
                var userHoursCostList = _repository.GetAllUserHourCost(userID);

                return userHoursCostList.Select(i => new UserHourCostsDTO
                {
                    UserID = userID,   
                    ID =i.ID,
                    HourCost = i.HourCost,
                    EndDate = i.EndDate,
                    StartDate = i.StartDate,
                    FunctionName = i.FunctionName
                }).ToList();
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
                        StartDate = HoursCostCSV.StartDate,
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
        public List<Functions> GetAllFunctions()
        {
            return _repository.GetAllFunction();

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
          
            List<Functions> functionsList = _repository.GetAllFunction().Select(f => new Functions
            {
                ID = f.ID,
                Name = RemoveAccents(f.Name).ToUpper().Replace(" ", "")
            }).ToList();
            var functionsDict = functionsList.ToDictionary(f => f.Name, f => f.ID);

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
                        var usersDict = allUsers.ToDictionary(u => RemoveDiacritics(u.FullName).ToUpper(), u => u);

                  
                        var allHoursCosts = _repository.GetAllHours();

             
                        var hoursCostsDict = allHoursCosts.GroupBy(u => new { u.UserID, u.StartDate, u.EndDate })
                                                          .ToDictionary(g => g.Key, g => g.First());

                        int rowCount = worksheet.LastRowUsed().RowNumber();

               
                        List<UserHourCosts> userHourCostsToCreate = new List<UserHourCosts>();
                        List<UserHourCosts> userHourCostsToUpdate = new List<UserHourCosts>();
                        List<User> usersToUpdate = new List<User>();

                        for (int row = 3; row <= rowCount; row++)
                        {
                            string colaborador = worksheet.Cell(row, 2).GetValue<string>()?.Trim();
                            if (string.IsNullOrEmpty(colaborador))
                                continue;

                            if (!double.TryParse(worksheet.Cell(row, 7).GetValue<string>(), out double hourCost))
                                continue;

                            string functionNameRaw = worksheet.Cell(row, 6).GetValue<string>() ?? "";
                            string functionName = RemoveAccents(functionNameRaw).ToUpper().Replace(" ", "");
                            if (!functionsDict.TryGetValue(functionName, out int functionId))
                                continue;

                            string managementName = worksheet.Cell(row, 5).GetValue<string>();

                            string userNameKey = RemoveDiacritics(colaborador).ToUpper();
                            if (!usersDict.TryGetValue(userNameKey, out User user))
                                continue;

                            var userHourCost = new UserHourCosts
                            {
                                UserID = user.Id,
                                HourCost = hourCost,
                                StartDate = startDate,
                                EndDate = endDate,
                                ID = Guid.NewGuid().ToString(),
                                FunctionName = functionNameRaw,
                            };

                            var key = new { userHourCost.UserID, userHourCost.StartDate, userHourCost.EndDate };
                            if (hoursCostsDict.ContainsKey(key))
                            {
                                
                                userHourCostsToUpdate.Add(userHourCost);
                            }
                            else
                            {
                               
                                userHourCostsToCreate.Add(userHourCost);
                            }

                            user.FunctionID = functionId;
                         
                            usersToUpdate.Add(user);
                        }

                    
                        if (userHourCostsToCreate.Any())
                            _repository.CreateUserHourCostsBulk(userHourCostsToCreate);

                        if (userHourCostsToUpdate.Any())
                            _repository.UpdateUserHourCostsBulk(userHourCostsToUpdate);

                        if (usersToUpdate.Any())
                            _repository.UpdateUsersBulk(usersToUpdate);

                        return true;
                    }
                }
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
            var userExists = allHoursCosts.Where(u => u.UserID == userHourCost.UserID);
            var dateExists = allHoursCosts.Where(s => s.StartDate == userHourCost.StartDate && s.EndDate == userHourCost.EndDate && s.UserID == userHourCost.UserID).ToList().FirstOrDefault();
            if (dateExists != null)
            {
                return _repository.UpdateUserHourCost(userHourCost);
            }
            return _repository.CreateUserHourCost(userHourCost);
        }

        public string RemoveAccents(string text)
        {
            text = text.Replace("-", "");
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

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToUpper();
        }







    }
}
