using CsvHelper.Configuration;
using Humanizer;
using NuGet.Packaging.Signing;
using System.IO;

namespace GenAITech.Models
{
    public sealed class JobDataMap : ClassMap<JobData>
    {
        public JobDataMap()
        {
            Map(m => m.work_year);
            Map(m => m.experience_level);
            Map(m => m.employment_type);
            Map(m => m.job_title);
            Map(m => m.salary);
            Map(m => m.salary_currency);
            Map(m => m.salary_in_usd);
            Map(m => m.employee_residence);
            Map(m => m.remote_ratio);
            Map(m => m.company_location);
            Map(m => m.company_size);
        }
    }
 


}
