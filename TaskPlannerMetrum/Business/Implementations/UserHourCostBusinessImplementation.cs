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
                    ID = i.ID,
                    HourCost = i.HourCost,
                    EndDate = i.EndDate,
                    StartDate = i.StartDate,
                    FunctionName = i.FunctionName,
                    CreationDate = i.CreationDate,
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




            try
            {


                var functionsDict = _repository.GetAllFunction()
                    .ToDictionary(f => RemoveAccents(f.Name).ToUpper().Replace(" ", ""), f => f.ID);

                var usersDict = _repository.GetAllUsers()
                    .ToDictionary(u => NormalizeString(u.FullName), u => u);

                var existingHoursDict = _repository.GetAllHours()
                    .GroupBy(u => new { u.UserID, u.StartDate, u.EndDate, u.HourCost, u.FunctionName })
                    .ToDictionary(g => g.Key, g => g.First());

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


                        int rowCount = worksheet.LastRowUsed().RowNumber();
                        List<UserHourCosts> userHourCostsToCreate = new List<UserHourCosts>();

                        List<User> usersToUpdate = new List<User>();

                        for (int row = 3; row <= rowCount; row++)
                        {
                            string colaborador = worksheet.Cell(row, 2).GetValue<string>()?.Trim();

                            if (string.IsNullOrEmpty(colaborador)) continue;


                            if (!double.TryParse(worksheet.Cell(row, 7).GetValue<string>(), out double hourCost))
                                continue;

                            string functionNameRaw = worksheet.Cell(row, 6).GetValue<string>() ?? "";
                            string functionName = RemoveAccents(functionNameRaw).ToUpper().Replace(" ", "");

                            if (!functionsDict.TryGetValue(functionName, out int functionId)) continue;

                            string managementName = worksheet.Cell(row, 5).GetValue<string>();

                            string userNameKey = NormalizeString(colaborador);


                            if (!usersDict.TryGetValue(userNameKey, out User user))
                            {

                                user = usersDict.Values
                                    .OrderByDescending(u => Similarity(NormalizeString(u.FullName), userNameKey))
                                    .FirstOrDefault(u => Similarity(NormalizeString(u.FullName), userNameKey) > 0.8);

                                if (user == null) continue;
                            }

                            var userHourCostKey = new { UserID = user.Id, StartDate = startDate, EndDate = endDate, HourCost = hourCost, FunctionName = functionNameRaw };
                            if (!existingHoursDict.ContainsKey(userHourCostKey))
                            {
                                var creationDate = startDate.Date.Add(DateTime.Now.TimeOfDay).AddTicks(-(DateTime.Now.TimeOfDay.Ticks % TimeSpan.TicksPerSecond));

                                var newUserHourCost = new UserHourCosts
                                {
                                    UserID = user.Id,
                                    HourCost = hourCost,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    ID = Guid.NewGuid().ToString(),
                                    FunctionName = functionNameRaw,
                                    CreationDate = creationDate
                                };
                                userHourCostsToCreate.Add(newUserHourCost);

                            }

                            user.FunctionID = functionId;

                            usersToUpdate.Add(user);
                        }


                        if (userHourCostsToCreate.Any())
                            _repository.CreateUserHourCostsBulk(userHourCostsToCreate);

                        if (usersToUpdate.Any())
                            _repository.UpdateUsersBulk(usersToUpdate);

                        await _repository.SaveChangesAsync();

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

        public static string NormalizeString(string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? string.Empty
                : string.Concat(input.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
                    .ToUpperInvariant();
        }


        public static int LevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s)) return t?.Length ?? 0;
            if (string.IsNullOrEmpty(t)) return s.Length;

            int[,] d = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++)
                d[i, 0] = i;
            for (int j = 0; j <= t.Length; j++)
                d[0, j] = j;

            for (int i = 1; i <= s.Length; i++)

            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = s[i - 1] == t[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[s.Length, t.Length];
        }

        public static double Similarity(string s, string t)
        {
            int maxLength = Math.Max(s.Length, t.Length);
            return maxLength == 0 ? 1.0 : 1.0 - (double)LevenshteinDistance(s, t) / maxLength;
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

        public Task<List<UserHourCosts>> GetLatestFunctionByAllUsersAsync()
        {
            return _repository.GetLatestFunctionByAllUsersAsync();
        }

        public List<UserHourCosts> GetUserCostsByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            return _repository.GetUserCostsByDateRange(userId, startDate, endDate);
        }
    }
}
