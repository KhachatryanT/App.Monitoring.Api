using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using App.Monitoring.UseCases.Handlers.NodesEvents.Commands.CreateNodeEvents;
using App.Monitoring.UseCases.Handlers.NodesEvents.Queries.GetNodeEvents;
using AutoFixture.Xunit2;
using MediatR;
using Moq;
using Xunit;

namespace App.Monitoring.Api.UnitTests;

/// <summary>
/// Тесты контроллера <see cref="NodeEventsController"/>.
/// </summary>
public class NodeEventsControllerTests
{
    /// <summary>
    /// Добавить события узла.
    /// Должен быть вызван метод репозитория для добавления записей.
    /// </summary>
    /// <param name="nodesRepositoryMoq">Moq репозитория узлов.</param>
    /// <param name="nodesEventsRepositoryMoq">Moq репозитория событий узлов.</param>
    /// <param name="senderMoq">Moq MediatR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <param name="nodeIdRequest">Идентификатор узла.</param>
    /// <param name="nodeEventsRequest">События.</param>
    /// <returns>Task.</returns>
    [Theory]
    [AutoMoqData]
    public async Task CreateNodeEvents_ShouldCreateRepositoryMethodCalled(
        [Frozen] Mock<INodesRepository> nodesRepositoryMoq,
        [Frozen] Mock<INodeEventsRepository> nodesEventsRepositoryMoq,
        [Frozen] Mock<ISender> senderMoq,
        NodeEventsController controller,
        Guid nodeIdRequest,
        IEnumerable<NodeEvent> nodeEventsRequest)
    {
        // Arrange
        nodesRepositoryMoq.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(() => Task.FromResult((NodeEntity?)new NodeEntity(nodeIdRequest, DeviceType.Android, "Ваня", "1.0.0", DateTimeOffset.UtcNow)));

        nodesEventsRepositoryMoq.Setup(repo => repo.CreateAsync(It.IsAny<IEnumerable<NodeEventEntity>>(), It.IsAny<CancellationToken>()));

        var handler = new CreateNodeEventsCommandHandler(nodesRepositoryMoq.Object, nodesEventsRepositoryMoq.Object);

        senderMoq.Setup(x => x.Send(It.IsAny<CreateNodeEventsCommand>(), It.IsAny<CancellationToken>()))
            .Returns<CreateNodeEventsCommand, CancellationToken>((request, cToken) => handler.Handle(request, cToken));

        // Act
        _ = await controller.CreateNodeEvents(nodeIdRequest, nodeEventsRequest);

        // Assert
        nodesEventsRepositoryMoq.Verify(x => x.CreateAsync(It.IsAny<IEnumerable<NodeEventEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Получение событий узла.
    /// Должны вернуться все события узла.
    /// </summary>
    /// <param name="nodesEventsRepositoryMoq">Moq репозитория событий узлов.</param>
    /// <param name="senderMoq">Moq MediartR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <param name="nodeIdRequest">Идентификатор узла.</param>
    /// <param name="expectedNodeEvents">События.</param>
    /// <returns>Task.</returns>
    [Theory]
    [AutoMoqData]
    public async Task GetNodeEvents_ShouldReturnEvents(
        [Frozen] Mock<INodeEventsRepository> nodesEventsRepositoryMoq,
        [Frozen] Mock<ISender> senderMoq,
        NodeEventsController controller,
        Guid nodeIdRequest,
        IEnumerable<NodeEventEntity> expectedNodeEvents)
    {
        // Arrange
        nodesEventsRepositoryMoq.Setup(repo => repo.GetByNodeIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(() => Task.FromResult(expectedNodeEvents));

        var handler = new GetNodeEventsQueryHandler(nodesEventsRepositoryMoq.Object);

        senderMoq.Setup(x => x.Send(It.IsAny<GetNodeEventsQuery>(), It.IsAny<CancellationToken>()))
            .Returns<GetNodeEventsQuery, CancellationToken>((request, cToken) => handler.Handle(request, cToken));

        // Act
        var actualNodeEvents = await controller.GetNodeEvents(nodeIdRequest);

        // Assert
        Assert.Equal(expectedNodeEvents.Count(), actualNodeEvents.Events.Count());
    }
}
