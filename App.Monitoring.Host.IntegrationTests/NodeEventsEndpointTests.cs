using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Host.IntegrationTests.Helpers;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace App.Monitoring.Host.IntegrationTests;

/// <summary>
/// Тесты апи по работе с событиями узла..
/// </summary>
[Collection(nameof(ApplicationCollection))]
public class NodeEventsEndpointTests
{
    private readonly HttpClient _client;
    private readonly INodeEventsRepository _nodeEventsRepository;
    private readonly INodesRepository _nodesRepository;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="factory">Фабрика приложения.</param>
    public NodeEventsEndpointTests(AppWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _nodesRepository = factory.Services.GetRequiredService<INodesRepository>();
        _nodeEventsRepository = factory.Services.GetRequiredService<INodeEventsRepository>();
    }

    /// <summary>
    /// Добавить события узла.
    /// События должны успешно записаться в БД.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task CreateNodeEvents()
    {
        // Arrange
        var nodeId = Guid.NewGuid();
        var expectedNodeEvents = new[]
        {
            new NodeEvent("Вход", DateTimeOffset.Parse("2023-03-20 11:00:00")),
            new NodeEvent("Поиск", DateTimeOffset.Parse("2023-03-20 11:00:10")),
            new NodeEvent("Выход", DateTimeOffset.Parse("2023-03-20 11:00:20")),
        };

        // Act
        var actualResponse = await _client.PostAsJsonAsync($"/nodes/{nodeId}/events", expectedNodeEvents);

        // Assert
        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

        var actualNodeEvents = await _nodeEventsRepository.GetByNodeIdAsync(nodeId, default);
        Assert.Equal(expectedNodeEvents.Length, actualNodeEvents.Count());

        Assert.All(expectedNodeEvents, expected =>
            Assert.Contains(actualNodeEvents, actual =>
                actual.Name == expected.Name &&
                actual.Date == expected.Date));
    }

    /// <summary>
    /// Получение всех событие узла.
    /// Должны вернуться все события.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task GetNodeEventsRequest_ReturnAllNodeEventsExpected()
    {
        // Arrange
        var expectedNode = new NodeEntity(Guid.NewGuid(), DeviceType.Android, "Бажена", "1.0.3", DateTimeOffset.Parse("2023-03-20 10:00:00"));
        var expectedNodeEvents = new[]
        {
            new NodeEventEntity(expectedNode.Id, "Вход", DateTimeOffset.Parse("2023-03-20 11:00:00")),
            new NodeEventEntity(expectedNode.Id, "Поиск", DateTimeOffset.Parse("2023-03-20 11:00:10")),
            new NodeEventEntity(expectedNode.Id, "Выход", DateTimeOffset.Parse("2023-03-20 11:00:20")),
        };
        await _nodesRepository.CreateAsync(expectedNode, default);
        await _nodeEventsRepository.CreateAsync(expectedNodeEvents, default);

        // Act
        var actualResponse = await _client.GetAsync($"/nodes/{expectedNode.Id}/events");

        // Assert
        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

        var actualResult = await actualResponse.DeserializeResponseAsync<GetNodeEventsResult>();
        Assert.Equal(expectedNode.Id, actualResult.Id);
        Assert.Equal(expectedNodeEvents.Length, actualResult.Events.Count());
        Assert.All(expectedNodeEvents, expected =>
            Assert.Contains(actualResult.Events, actual =>
                actual.Name == expected.Name &&
                actual.Date == expected.Date));
    }
}
