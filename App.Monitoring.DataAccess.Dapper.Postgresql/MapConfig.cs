using App.Monitoring.DataAccess.Dapper.Postgresql.Entities;
using App.Monitoring.Entities.Models;
using Mapster;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Конфигурации маппинга.
/// </summary>
public sealed class MapConfig: IRegister
{
    /// <summary>
    /// Регистрация конфигураций.
    /// </summary>
    /// <param name="config">Конфиг.</param>
    public void Register(TypeAdapterConfig config) => config.NewConfig<DeviceStatisticEntity, DeviceStatistic>();
}
