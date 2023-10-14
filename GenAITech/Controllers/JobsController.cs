using System.Formats.Asn1;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;
using GenAITech.Models;
using CsvHelper.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace GenAITech.Controllers
{
    public class JobsController : Controller

    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public JobsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: JobsController
        public ActionResult Index()
        {

            var jobDataList = new List<JobData>();

            using (var reader = new StreamReader(_webHostEnvironment.WebRootPath+ "/csv/salaries2023.csv"))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                jobDataList = csv.GetRecords<JobData>().ToList();
            }
            

            var top10JobTitlesBySalary = jobDataList
                .GroupBy(j => j.job_title)
                 .Select(g => new
                         {
                          JobTitle = g.Key,
                    AverageSalary = g.Average(j => j.salary)
                })
                .OrderByDescending(x => x.AverageSalary)
                .Take(10)
                .ToDictionary(x => x.JobTitle, x => x.AverageSalary);
            ViewBag.TopJobTitlesBySalary = JsonConvert.SerializeObject(top10JobTitlesBySalary);

            var top5Companies = jobDataList
                .GroupBy(j => j.company_size)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .SelectMany(g => g)
                .Take(5)
                .ToList();

            ViewBag.TopCompanies = top5Companies;

            var currentYear = DateTime.Now.Year;
            var salaryTotals = new Dictionary<int, decimal>();
            for (int year = currentYear - 3; year <= currentYear; year++)
            {
                decimal totalSalary = jobDataList.Where(j => j.work_year == year.ToString())
                                                 .Sum(j => j.salary);
                salaryTotals.Add(year, totalSalary);
            }
            ViewBag.SalaryTotals = salaryTotals;

            return View(top5Companies);


        }


    }
}
    

