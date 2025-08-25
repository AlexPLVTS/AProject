using System;
using System.Collections.Generic;
using Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using Application.Activities.DTOs;
using AutoMapper;
using FluentValidation;
using Application.Core;
using Application.Interfaces;

namespace Application.Activities.Command;

public class CreateActivity
{
    public class Command : IRequest<Result<string>>
    {
        public required CreateActivityDTO ActivityDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = mapper.Map<Activity>(request.ActivityDto);

            context.Activities.Add(activity);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<string>.Failure("Failed to create activity", 400);

            return Result<string>.Success(activity.Id);
        }
    }
}
