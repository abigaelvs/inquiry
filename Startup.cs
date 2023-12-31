using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

using CNDS.Configuration;

using InqService.Config;
using InqService.Repository;

namespace InqService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new ConfigurationLoader(Configuration));

            StartupRepository.Init();
            StartupRepository.InitGlobalParam();

            services.AddSingleton<EmailConfig>();
            services.AddSingleton<IEmailRepository>(sp =>
            {
                EmailConfig emailConfig = sp.GetService<EmailConfig>();
                return new EmailRepository(Configuration, emailConfig);
            });
            services.AddSingleton<IGlobalRepository>(sp =>
            {
                IEmailRepository emailRepo = sp.GetService<IEmailRepository>();
                return new GlobalRepository(emailRepo);
            });
            services.AddSingleton<IInquiryRepository>(new InquiryRepository(Configuration));
            services.AddSingleton<ICustomerRepository, CustomerRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseVerifySignature();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
