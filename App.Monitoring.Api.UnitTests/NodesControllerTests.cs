using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
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
    /// Должны вернуться все узлы.
    /// </summary>
    /// <param name="nodesRepositoryMoq">Moq репозитория узлов.</param>
    /// <param name="senderMoq">Moq MediartR.</param>
    /// <param name="controller">Контроллер.</param>
    /// <param name="expectedNodes">Узлы.</param>
    /// <returns>Task.</returns>
    [Theory]
    [AutoMoqData]
    public async Task GetNodes_ShouldReturnNodes(
        [Frozen] Mock<INodesRepository> nodesRepositoryMoq,
        [Frozen] Mock<ISender> senderMoq,
        NodesController controller,
        IEnumerable<NodeEntity> expectedNodes)
    {
        // Arrange
        nodesRepositoryMoq.Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
            .Returns(() => Task.FromResult(expectedNodes));

        var handler = new GetNodesQueryHandler(nodesRepositoryMoq.Object);

        senderMoq.Setup(x => x.Send(It.IsAny<GetNodesQuery>(), It.IsAny<CancellationToken>()))
            .Returns<GetNodesQuery, CancellationToken>((request, cToken) => handler.Handle(request, cToken));

        // Act
        var actualNodes = await controller.GetNodes();

        // Assert
        Assert.Equal(expectedNodes.Count(), actualNodes.Count());
    }
}
