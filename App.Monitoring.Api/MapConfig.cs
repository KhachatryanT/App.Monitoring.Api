using App.Monitoring.Api.Contracts;
using App.Monitoring.Entities.Models;
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
    public void Register(TypeAdapterConfig config) => config.NewConfig<DeviceStatistic, DeviceStatisticResult>();
}
