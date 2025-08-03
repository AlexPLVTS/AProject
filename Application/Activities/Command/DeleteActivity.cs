using System;
using System.Collections.Generic;
using Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using AutoMapper;

namespace Application.Activities.Command;

public class DeleteActivity
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.Id], cancellationToken)
                                    ?? throw new Exception("Cannot find activity");

            context.Remove(activity);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
