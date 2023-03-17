using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace App.Monitoring.Host.IntegrationTests;

/// <summary>
/// Тесты апи по работе с узлом.
/// </summary>
[Collection(nameof(ApplicationCollection))]
public class NodeEndpointTests
{
    private readonly HttpClient _client;
    private readonly INodesRepository _nodesRepository;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="factory">Фабрика приложения.</param>
    public NodeEndpointTests(AppWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _nodesRepository = factory.Services.GetRequiredService<INodesRepository>();
    }

    /// <summary>
    /// Добавление узла.
    /// Узел должен создаться в БД.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task CreateNodeRequest_NodeWasCreatedInDbExpected()
    {
        // Arrange
        var nodeId = Guid.NewGuid();
        var expectedNode = new CreateNodeRequest(DeviceType.Android, "Иван Петрович", "1.0.1");

        // Act
        var actualResponse = await _client.PostAsJsonAsync($"/nodes/{nodeId}", expectedNode);

        // Assert
        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
        var actualNode = await _nodesRepository.GetAsync(nodeId, default);
        Assert.NotNull(actualNode);
        Assert.Equal(expectedNode.DeviceType, actualNode.DeviceType);
        Assert.Equal(expectedNode.UserName, actualNode.UserName);
        Assert.Equal(expectedNode.ClientVersion, actualNode.ClientVersion);
    }
}
