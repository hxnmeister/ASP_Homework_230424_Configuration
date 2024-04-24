using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace ASP_Homework_230424_Configuration
{
    public class Startup
    {
        private IConfiguration configuration;

        public IConfiguration Configuration
        {
            get => configuration;
            set => configuration = value;
        }

        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            builder.SetBasePath(env.ContentRootPath + "\\configurations");
            builder.AddXmlFile("data.xml");
            builder.AddJsonFile("tags.json");
            builder.AddJsonFile("styles.json");
            builder.AddJsonFile("props.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            string response = $@"
                {Configuration["docType"]}
                <html>
                    <head>
                        <style>
                            {Configuration["global"]}
                            {Configuration["tags:body"]}
                            {Configuration["tags:a"]}
                            {Configuration["classes:container"]}
                            {Configuration["classes:title"]}
                            {Configuration["classes:info"]}
                            {Configuration["classes:infoAfter"]}
                            {Configuration["classes:studyHover"]}
                            {Configuration["classes:infoAfterHover"]}
                            {Configuration["classes:personalInfoBlock"]}
                            {Configuration["classes:personalInfoBlockHover"]}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='name'>
                                {Configuration["titles:nameTitle"]}
                                <p class='info'>{Configuration["personalInfo:firstName"]} {Configuration["personalInfo:lastName"]}</p>
                            </div>
                            <div class='birth-date'>
                                {Configuration["titles:birthDateTitle"]}
                                <p class='info'>{Configuration["personalInfo:birthDate"]}</p>
                            </div>
                            <div class='study'>
                                {Configuration["titles:studyTitle"]}
                                <a {Configuration["hrefs:school"]} class='info'> - {Configuration["personalInfo:studyPlaces:school"]}</a>
                                <a {Configuration["hrefs:college"]} class='info'> - {Configuration["personalInfo:studyPlaces:college"]}</a>
                                <a {Configuration["hrefs:itCourses"]} class='info'> - {Configuration["personalInfo:studyPlaces:ITCourses"]}</a>
                            </div>
                            <div class='languages'>
                                {Configuration["titles:languagesTitle"]}
                                <p class='info'> - {Configuration["personalInfo:languages:german"]}</p>
                                <p class='info'> - {Configuration["personalInfo:languages:english"]}</p>
                                <p class='info'> - {Configuration["personalInfo:languages:italian"]}</p>
                                <p class='info'> - {Configuration["personalInfo:languages:ukrainian"]}</p>
                            </div>
                            <div class='about-myself'>
                                {Configuration["titles:aboutMeTitle"]}
                                <p class='info'>
                                   {Configuration["personalInfo:aboutMe"]}
                                </p>
                            </div>
                        </div>
                    </body>
                </html>
            ";

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(response);
            });
        }
    }
}
