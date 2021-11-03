using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.BLL.Services;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using BnBAir.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BnBAir.IoC
{
    public class DependencyInjection
    {
        private readonly IConfiguration _configuration;

        public DependencyInjection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InjectDependencies(IServiceCollection services)
        {
            var connection = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ReservationContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IBnBAirUW, BnBAirUW>();
            services.AddScoped<IService<CategoryDateDTO>, CategoryDateService>();
        }
    }
}