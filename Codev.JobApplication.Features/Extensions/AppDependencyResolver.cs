using Codev.JobApplication.Dapper;
using Codev.JobApplication.DataRepository;
using Codev.JobApplication.Domain.Config;
using Codev.JobApplication.Features.Persistence.Applicant;
using Codev.JobApplication.Features.Persistence.Job;
using Codev.JobApplication.Features.Persistence.JobApplication;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppDependencyResolver
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            SqlConfig sqlConfig = new SqlConfig();
            configuration.GetSection("SqlConfig").Bind(sqlConfig);
            services.AddSingleton(sqlConfig);

            //object mappings
            TypeAdapterConfig<Job, JobRequestDto>
                .NewConfig();
            TypeAdapterConfig<Applicant, ApplicantRequestDto>
                .NewConfig();

            services.TryAddScoped<IDapperBase, DapperBase>();

            services.TryAddScoped<IDataRepository<Job>, JobDataService>();
            services.TryAddScoped<IJobQueryService, JobDataService>();
            services.TryAddScoped<IDataRepository<Job>, JobDataService>();
            services.TryAddScoped<IDataRepository<Applicant>, ApplicantDataService>();
            services.TryAddScoped<IJobApplicationManager, JobApplicationDataService>();
            services.TryAddScoped<IApplicantQueryService, JobApplicationDataService>();
        }
    }
}
