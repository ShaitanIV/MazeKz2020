﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMaze.DbStuff;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.UserAccount;
using WebMaze.DbStuff.Model.Medicine;
using WebMaze.DbStuff.Repository;
using WebMaze.DbStuff.Repository.MedicineRepository;
using WebMaze.Models.Account;
using WebMaze.Models.Department;
using WebMaze.Models.Bus;
using WebMaze.Models.Certificates;
using WebMaze.Models.HealthDepartment;
using WebMaze.Models.UserTasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using WebMaze.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebMaze.Models.Police;
using WebMaze.DbStuff.Model.Police;
using WebMaze.DbStuff.Repository.MedicineRepo;
using WebMaze.Models.Roles;
using WebMaze.Models.Police.Violation;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using WebMaze.Infrastructure;
using WebMaze.Models.HDDoctor;
using WebMaze.Models.HDManager;

namespace WebMaze
{
    public class Startup
    {
        public const string AuthMethod = "CoockieAuth";
        public const string PoliceAuthMethod = "PoliceAuth";
        public const string MedicineAuth = "CookieMedicineAuth";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=WebMazeKz;Trusted_Connection=True;MultipleActiveResultSets=true;";
            services.AddDbContext<WebMazeContext>(option => option.UseSqlServer(connectionString));

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "User.Auth";
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Account/AccessDenied";
                })
                .AddCookie(PoliceAuthMethod, config =>
                {
                    config.Cookie.Name = "PUser";
                    config.LoginPath = "/Police/Login";
                });

            services.AddAuthentication(AuthMethod)
                .AddCookie(MedicineAuth, config =>
                {
                    config.Cookie.Name = "Med.Auth";
                    config.LoginPath = "/HealthDepartment/Login";
                    config.AccessDeniedPath = "/HealthDepartment/AccessDenied";
                });

            services.AddTransient<IAuthorizationHandler, RestrictAccessToBlockedUsersHandler>(s =>
                new RestrictAccessToBlockedUsersHandler(s.GetService<CitizenUserRepository>()));

            services.AddTransient<IAuthorizationHandler, RestrictAccessToDeadUsersHandler>(s =>
                new RestrictAccessToDeadUsersHandler(s.GetService<CitizenUserRepository>()));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admins", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                    policy.Requirements.Add(new RestrictAccessToBlockedUsersRequirement());
                    policy.Requirements.Add(new RestrictAccessToDeadUsersRequirement());
                });
            });

            RegistrationMapper(services);

            RegistrationRepository(services);

            services.AddScoped(s => new UserService(s.GetService<CitizenUserRepository>(),
                s.GetService<RoleRepository>(),
                s.GetService<IHttpContextAccessor>()));

            services.AddHttpContextAccessor();

            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddHttpClient<CertificateService>();
        }

        private void RegistrationMapper(IServiceCollection services)
        {
            var configurationExpression = new MapperConfigurationExpression();

            configurationExpression.CreateMap<CitizenUser, ProfileViewModel>();
            configurationExpression.CreateMap<ProfileViewModel, CitizenUser>();

            configurationExpression.CreateMap<CitizenUser, RegistrationViewModel>();
            configurationExpression.CreateMap<RegistrationViewModel, CitizenUser>();

            configurationExpression.CreateMap<Adress, AdressViewModel>();
            configurationExpression.CreateMap<AdressViewModel, Adress>();

            configurationExpression.CreateMap<HealthDepartment, HealthDepartmentViewModel>();
            configurationExpression.CreateMap<HealthDepartmentViewModel, HealthDepartment>();

            configurationExpression.CreateMap<Bus, BusViewModel>();
            configurationExpression.CreateMap<BusViewModel, Bus>();

            configurationExpression.CreateMap<BusRoute, BusRouteViewModel>();
            configurationExpression.CreateMap<BusRouteViewModel, BusRoute>();

            configurationExpression.CreateMap<BusOrder, BusOrderViewModel>();
            configurationExpression.CreateMap<BusOrderViewModel, BusOrder>();

            configurationExpression.CreateMap<BusStop, BusStopViewModel>();
            configurationExpression.CreateMap<BusStopViewModel, BusStop>();

            configurationExpression.CreateMap<Bus, BusOrderViewModel>();
            configurationExpression.CreateMap<BusOrderViewModel, Bus>();

            configurationExpression.CreateMap<RecordForm, RecordFormViewModel>();
            configurationExpression.CreateMap<RecordFormViewModel, RecordForm>();

            configurationExpression.CreateMap<RecordForm, ListRecordFormViewModel>();
            configurationExpression.CreateMap<ListRecordFormViewModel, RecordForm>();

            configurationExpression.CreateMap<UserTask, UserTaskViewModel>();
            configurationExpression.CreateMap<UserTaskViewModel, UserTask>();

            configurationExpression.CreateMap<Certificate, CertificateViewModel>()
                .ForMember(dest => dest.OwnerLogin, opt => opt.MapFrom(src => src.Owner.Login));

            configurationExpression.CreateMap<CertificateViewModel, Certificate>();

            configurationExpression.CreateMap<Policeman, PolicemanViewModel>()
                .ForMember(dest => dest.ProfileVM, opt => opt.MapFrom(p => p.User));

            configurationExpression.CreateMap<Violation, ViolationItemViewModel>()
                .ForMember(dest => dest.BlamedUserName, opt => opt.MapFrom(v => v.BlamedUser.FirstName + " " + v.BlamedUser.LastName))
                .ForMember(dest => dest.PolicemanName, opt => opt.MapFrom(v => v.ViewingPoliceman.User.FirstName + " " + v.ViewingPoliceman.User.LastName));

            configurationExpression.CreateMap<ViolationDeclarationViewModel, Violation>().ReverseMap();

            configurationExpression.CreateMap<CitizenUser, FoundUsersViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(u => u.FirstName + " " + u.LastName));

            configurationExpression.CreateMap<CitizenUser, UserVerificationViewModel>();

            configurationExpression.CreateMap<Violation, CriminalItemViewModel>()
                .ForMember(dest => dest.BlamingUserName, opt => opt.MapFrom(v => v.BlamingUser.FirstName + " " + v.BlamingUser.LastName))
                .ForMember(dest => dest.BlamedUserName, opt => opt.MapFrom(v => v.BlamedUser.FirstName + " " + v.BlamedUser.LastName))
                .ForMember(dest => dest.ViewingPolicemanName, opt => opt.MapFrom(v => v.ViewingPoliceman.User.FirstName + " " + v.ViewingPoliceman.User.LastName))
                .ForMember(dest => dest.PolicemanLogin, opt => opt.MapFrom(v => v.ViewingPoliceman.User.Login))
                .ForMember(dest => dest.BlamedUserAvatar, opt => opt.MapFrom(v => v.BlamedUser.AvatarUrl));

            configurationExpression.CreateMap<MedicalInsurance, MedicalInsuranceViewModel>();
            configurationExpression.CreateMap<MedicalInsuranceViewModel, MedicalInsurance>();

            configurationExpression.CreateMap<CitizenUser, ForDHLoginViewModel>();
            configurationExpression.CreateMap<ForDHLoginViewModel, CitizenUser>();

            configurationExpression.CreateMap<MedicalInsurance, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, MedicalInsurance>();

            configurationExpression.CreateMap<CitizenUser, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, CitizenUser>();

            configurationExpression.CreateMap<CitizenUser, DoctorPageViewModel>();
            configurationExpression.CreateMap<DoctorPageViewModel, CitizenUser>();

            configurationExpression.CreateMap<Role, RoleViewModel>()
                .ForMember(dest => dest.UserLogins, opt => opt.MapFrom(src => src.Users.Select(t => t.Login)));

            configurationExpression.CreateMap<RoleViewModel, Role>();

            configurationExpression.CreateMap<MedicineCertificate, MedicineCertificateViewModel>();
            configurationExpression.CreateMap<MedicineCertificateViewModel, MedicineCertificate>();

            configurationExpression.CreateMap<ReceptionOfPatients, ReceptionOfPatientsViewModel>();
            configurationExpression.CreateMap<ReceptionOfPatientsViewModel, ReceptionOfPatients>();

            configurationExpression.CreateMap<ReceptionOfPatients, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, ReceptionOfPatients>();

            var mapperConfiguration = new MapperConfiguration(configurationExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(s => mapper);
        }

        private void RegistrationRepository(IServiceCollection services)
        {
            services.AddScoped<CitizenUserRepository>(serviceProvider =>
            {
                var webContext = serviceProvider.GetService<WebMazeContext>();
                return new CitizenUserRepository(webContext);
            });

            services.AddScoped(s => new AdressRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new PolicemanRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new ViolationRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new HealthDepartmentRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new RecordFormRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new BusRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusStopRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusRouteRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusOrderRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusWorkerRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusRouteTimeRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new UserTaskRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new CertificateRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new RoleRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new MedicalInsuranceRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new MedicineCertificateRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new ReceptionOfPatientsRepository(s.GetService<WebMazeContext>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Кто ты?
            app.UseAuthentication();

            // Куда у тебя есть доступ?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
