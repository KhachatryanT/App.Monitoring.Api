using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateOrUpdateNode;
using App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;
using AutoFixture.Xunit2;
using MediatR;
using Moq;
using Xunit;

namespace App.Monitoring.Api.UnitTests;

/// <summary>
/// Тесты контроллера <see cref="NodesController"/>.
/// </summary>
public class NodesControllerTests
{
    /// <summary>
    /// Получить узлы.
    /// Должен быть вызван MediatR с правильным типом.
    /// </summary>
    /// <param name="senderMoq">Moq MediartR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <returns>Task.</returns>
    [Theory]
    [AutoMoqData]
    public async Task GetNodes_SenderMethodWasCalledWithCorrectTypeExpected(
        [Frozen] Mock<ISender> senderMoq,
        NodesController controller)
    {
        // Act
        await controller.GetNodes();

        // Assert
        senderMoq.Verify(x => x.Send(It.IsAny<GetNodesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Добавить или обновить узел.
    /// Должен быть вызван MediatR с правильным типом.
    /// </summary>
    /// <param name="senderMoq">Moq MediatR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <param name="nodeIdRequest">Идентификатор узла.</param>
    /// <param name="nodeRequest">Узел.</param>
    /// <returns>Task.</returns>
    [Theory]
    [AutoMoqData]
    public async Task CreateOrUpdateNode_SenderMethodWasCalledWithCorrectTypeExpected(
        [Frozen] Mock<ISender> senderMoq,
        NodesController controller,
        Guid nodeIdRequest,
        CreateNodeRequest nodeRequest)
    {
        // Act
        await controller.CreateOrUpdateNode(nodeIdRequest, nodeRequest);

        // Assert
        senderMoq.Verify(x => x.Send(It.IsAny<CreateOrUpdateNodeCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
