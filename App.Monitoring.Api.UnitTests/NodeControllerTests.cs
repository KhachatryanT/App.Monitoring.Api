using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateOrUpdateNode;
using AutoFixture;
using AutoFixture.Xunit2;
using MediatR;
using Moq;
using Xunit;

namespace App.Monitoring.Api.UnitTests;

/// <summary>
/// Тесты контроллера <see cref="NodeController"/>.
/// </summary>
public class NodeControllerTests
{
    /// <summary>
    /// Добавить узел.
    /// Должен быть вызван метод репозитория для создание записи.
    /// </summary>
    /// <param name="nodesRepositoryMoq">Moq репозитория узлов.</param>
    /// <param name="senderMoq">Moq MediatR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <param name="nodeIdRequest">Идентификатор узла.</param>
    /// <param name="nodeRequest">Узел.</param>
    /// <returns>Task.</returns>
    [Theory, AutoMoqData]
    public async Task CreateOrUpdateNode__ShouldCreateRepositoryMethodCalled(
        [Frozen] Mock<INodesRepository> nodesRepositoryMoq,
        [Frozen] Mock<ISender> senderMoq,
        NodeController controller,
        Guid nodeIdRequest,
        CreateNodeRequest nodeRequest)
    {
        // Arrange
        nodesRepositoryMoq.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(() => Task.FromResult(default(NodeEntity?)));

        var handler = new CreateOrUpdateNodeCommandHandler(nodesRepositoryMoq.Object);

        senderMoq.Setup(x => x.Send(It.IsAny<CreateOrUpdateNodeCommand>(), It.IsAny<CancellationToken>()))
            .Returns<CreateOrUpdateNodeCommand, CancellationToken>((request, cToken) => handler.Handle(request, cToken));

        // Act
        _ = await controller.CreateOrUpdateNode(nodeIdRequest, nodeRequest);

        // Assert
        nodesRepositoryMoq.Verify(x => x.CreateAsync(It.IsAny<NodeEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Обновить узел.
    /// Должен быть вызван метод репозитория для обновления записи.
    /// </summary>
    /// <param name="nodesRepositoryMoq">Moq репозитория узлов.</param>
    /// <param name="senderMoq">Moq MediatR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <param name="nodeIdRequest">Идентификатор узла.</param>
    /// <param name="nodeRequest">Узел.</param>
    /// <returns>Task.</returns>
    [Theory, AutoMoqData]
    public async Task CreateOrUpdateNode__ShouldUpdateRepositoryMethodCalled(
        [Frozen] Mock<INodesRepository> nodesRepositoryMoq,
        [Frozen] Mock<ISender> senderMoq,
        NodeController controller,
        Guid nodeIdRequest,
        CreateNodeRequest nodeRequest)
    {
        // Arrange
        nodesRepositoryMoq.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(() => Task.FromResult(new Fixture().Create<NodeEntity?>()));

        var handler = new CreateOrUpdateNodeCommandHandler(nodesRepositoryMoq.Object);

        senderMoq.Setup(x => x.Send(It.IsAny<CreateOrUpdateNodeCommand>(), It.IsAny<CancellationToken>()))
            .Returns<CreateOrUpdateNodeCommand, CancellationToken>((request, cToken) => handler.Handle(request, cToken));

        // Act
        _ = await controller.CreateOrUpdateNode(nodeIdRequest, nodeRequest);

        // Assert
        nodesRepositoryMoq.Verify(x => x.UpdateAsync(It.IsAny<NodeEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

}
