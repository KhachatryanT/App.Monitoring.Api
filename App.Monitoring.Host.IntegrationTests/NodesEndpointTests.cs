using System;
using System.Collections.Generic;
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
/// Тесты апи по работе с узлами.
/// </summary>
[Collection(nameof(ApplicationCollection))]
public class NodesEndpointTests
{
    private readonly HttpClient _client;
    private readonly INodesRepository _nodesRepository;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="factory">Фабрика приложения.</param>
    public NodesEndpointTests(AppWebApplicationFactory factory)
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

    /// <summary>
    /// Получение узла.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task GetNodeRequest_ReturnNodesExpected()
    {
        // Arrange
        var expectedNodeId = Guid.NewGuid();
        var expectedNode = new NodeEntity(expectedNodeId, DeviceType.Windows, "Заслава", "1.0.3", DateTimeOffset.Now);
        await _nodesRepository.CreateAsync(expectedNode, default);

        // Act
        var actualResponse = await _client.GetAsync($"/nodes/{expectedNodeId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
        var actualNode = await actualResponse.DeserializeResponseAsync<Node>();
        Assert.Equal(expectedNodeId, actualNode.Id);
        Assert.Equal(expectedNode.UserName, actualNode.Name);
        Assert.Equal(expectedNode.DeviceType, actualNode.Os);
        Assert.Equal(expectedNode.ClientVersion, actualNode.Version);
    }

    /// <summary>
    /// Получение всех узлов.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task GetNodesRequest_ReturnAllNodesExpected()
    {
        // Arrange
        var expectedNodes = new[]
        {
            new NodeEntity(Guid.NewGuid(), DeviceType.Android, "Бажена", "1.0.3", DateTimeOffset.Now),
            new NodeEntity(Guid.NewGuid(), DeviceType.Iphone, "Владислава", "1.0.2", DateTimeOffset.Now),
            new NodeEntity(Guid.NewGuid(), DeviceType.Windows, "Заслава", "1.0.3", DateTimeOffset.Now),
        };
        foreach (var expectedNode in expectedNodes)
        {
            await _nodesRepository.CreateAsync(expectedNode, default);
        }

        // Act
        var actualResponse = await _client.GetAsync("/nodes");

        // Assert
        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
        var actualNodes = await actualResponse.DeserializeResponseAsync<IEnumerable<Node>>();
        Assert.All(expectedNodes, expected =>
            Assert.Contains(actualNodes, actual =>
                actual.Id == expected.Id &&
                actual.Name == expected.UserName &&
                actual.Os == expected.DeviceType &&
                actual.Version == expected.ClientVersion)
        );
    }
}
