using System.Threading.Tasks;
using AdsModule.src.Feature.Entities;

namespace AdsModule.src.Infrastructure.Messaging.Providers
{
    public interface IAdsConfigProvider
    {
        Task<AdsConfigEntity> GetConfig();
    }
}