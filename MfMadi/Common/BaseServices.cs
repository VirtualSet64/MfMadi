using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using MfMadi.Services;
using MfMadi.Services.Interfaces;
using Microsoft.Extensions.Hosting.Internal;

namespace MfMadi.Common
{
    public static class BaseServices
    {
        public static void AddDBService(this IServiceCollection services)
        {
            services.AddSingleton<AuthOptions>();
            services.AddScoped<IInteractionFileOnServer, InteractionFileOnServer>();
            services.AddScoped<IGenerateHtmlContent, GenerateHtmlContent>();

            #region Repositories
            services.AddScoped<IAdvertisingRepository, AdvertisingRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IFileModelRepository, FileModelRepository>();
            services.AddScoped<IMainMenuRepository, MainMenuRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            #endregion                        
        }
    }
}
