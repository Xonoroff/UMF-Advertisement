using System.Threading;
using Cysharp.Threading.Tasks;
using MF.Advertisement.src.IronSource.Entities;

namespace MF.Advertisement.src.IronSource.Managers
{
    public interface IIronSourceManager
    {
        UniTask<IronSourceDataEntity> GetDataEntity(CancellationToken ct);
    }
}