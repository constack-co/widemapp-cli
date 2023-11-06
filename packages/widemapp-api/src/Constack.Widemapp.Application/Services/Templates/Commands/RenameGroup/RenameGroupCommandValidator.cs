using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Templates.Commands.RenameGroup
{
    public class RenameGroupCommandValidator : AbstractValidator<RenameGroupCommand>
    {
        private readonly IAutomationDbContext _context;
        public RenameGroupCommandValidator(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
            {
                return await _context.Groups.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
            }).WithMessage(ValidatorMessages.NotFound("Group Id"));

            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Name")).DependentRules(() =>
            {
                RuleFor(x => new { x.Id, x.Name }).MustAsync(async (command, cancellation) =>
                {
                    var group = await _context.Groups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == command.Id);
                    if (group.GroupId == null)
                    {
                        return !await _context.Groups.AsNoTracking()
                        .Where(x => x.TemplateId == group.TemplateId)
                        .Where(x => x.GroupId == null)
                        .Where(x => x.GenerationTypeId == group.GenerationTypeId)
                        .AnyAsync(x => x.Name == command.Name && x.Id != command.Id);
                    }
                    else
                    {
                        return !await _context.Groups.AsNoTracking()
                            .Where(x => x.GroupId == group.GroupId)
                            .AnyAsync(x => x.Name == command.Name && x.Id != command.Id);
                    }
                }).WithMessage(ValidatorMessages.AlreadyExists("Group with provied name"));
            });
        }
    }
}
