using App.Monitoring.Api.Contracts;
using App.Monitoring.Entities.Entities;
using Mapster;

namespace App.Monitoring.Api;

/// <summary>
/// Конфигурации маппинга.
/// </summary>
public sealed class MapConfig: IRegister
{
    /// <summary>
    /// Регистрация конфигураций.
    /// </summary>
    /// <param name="config">Конфиг.</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<NodeEntity, Node>()
            .Map(dest=>dest.Os, src =>src.DeviceType)
            .Map(dest=>dest.Name, src =>src.UserName)
            .Map(dest=>dest.Version, src =>src.ClientVersion);
        config.NewConfig<NodeEventEntity, NodeEvent>();
    }
}
