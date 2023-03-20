using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
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
    /// Должен быть вызван MediatR с правильным типом.
    /// </summary>
    /// <param name="senderMoq">Moq MediatR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <param name="nodeIdRequest">Идентификатор узла.</param>
    /// <param name="nodeEventsRequest">События.</param>
    /// <returns>Task.</returns>
    [Theory]
    [AutoMoqData]
    public async Task CreateNodeEvents_SenderMethodWasCalledWithCorrectTypeExpected(
        [Frozen] Mock<ISender> senderMoq,
        NodeEventsController controller,
        Guid nodeIdRequest,
        IEnumerable<NodeEvent> nodeEventsRequest)
    {
        // Act
        await controller.CreateNodeEvents(nodeIdRequest, nodeEventsRequest);

        // Assert
        senderMoq.Verify(x => x.Send(It.IsAny<CreateNodeEventsCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Получение событий узла.
    /// Должен быть вызван MediatR с правильным типом.
    /// </summary>
    /// <param name="senderMoq">Moq MediartR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <param name="nodeIdRequest">Идентификатор узла.</param>
    /// <returns>Task.</returns>
    [Theory]
    [AutoMoqData]
    public async Task GetNodeEvents_SenderMethodWasCalledWithCorrectTypeExpected(
        [Frozen] Mock<ISender> senderMoq,
        NodeEventsController controller,
        Guid nodeIdRequest)
    {
        // Act
        await controller.GetNodeEvents(nodeIdRequest);

        // Assert
        senderMoq.Verify(x => x.Send(It.IsAny<GetNodeEventsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
