using Nominatim.Entities;
using Shib.Common.Interfaces.Address;
using Shib.Common.YandexAPI.Entities;
using Shib.XAF.Address.BusinessObjects.NonPersistent;

namespace Sunday.Blazor.Server.Services
{
    public static class AddressServiceEx
    {



        public static IServiceCollection AddAddressService(this IServiceCollection services, IConfiguration configuration)
        {
            var apiSettings = configuration.GetSection("ApiSettings");

            string selectedAPI = apiSettings.GetSection("AddressApi").GetSection("SelectedApi").Value;
            switch (selectedAPI.ToUpper())
            {
                case "YANDEX":
                    var geocoderKey = apiSettings.GetSection("YANDEX").GetSection("Geocoder").GetSection("Key").Value;

                    services.AddScoped<IAddressSearcher>(x => new YandexAddressManager(
                        new GeocoderClient(geocoderKey), () => new AddressEntity()));
                    break;
                case "NOMINATIM":
                default:
                    services.AddScoped<IAddressSearcher>(x =>
                new NominatimAddressSearcher(() => new AddressEntity()));
                    break;

            }
            return services;
        }

    }
}
