using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using JetBrains.Annotations;

namespace App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateNode;

[UsedImplicitly]
internal sealed class CreateNodeCommandHandler: ICommandHandler<CreateNodeCommand>
{
    private readonly IDbContext _dbContext;

    public CreateNodeCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(CreateNodeCommand request, CancellationToken cancellationToken)
    {
        _dbContext.Nodes.Add(new Node
        {
            DeviceId = request.DeviceId,
            DeviceType = request.DeviceType,
            Date = request.Date,
            Name = request.Name,
            ClientVersion = request.ClientVersion
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
