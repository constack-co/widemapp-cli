using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Application.Services.Templates.Commands.AddGroup;
using Constack.Widemapp.Common.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Entities.Commands.Add
{
    public class AddGroupCommandValidator : AbstractValidator<AddGroupCommand>
    {
        private readonly IAutomationDbContext _context;
        public AddGroupCommandValidator(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.GroupId).MustAsync(async (id, cancellation) =>
            {
                return await _context.Groups.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
            }).WithMessage(ValidatorMessages.NotFound("Group")).When(x => !string.IsNullOrEmpty(x.GroupId.ToString()));

            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Name")).DependentRules(() =>
            {
                RuleFor(x => new { x.GenerationTypeId, x.Name, x.GroupId, x.TemplateId }).MustAsync(async (command, cancellation) =>
                {
                    if (command.GenerationTypeId != null)
                    {
                        return !await _context.Groups.AsNoTracking()
                        .Where(x => x.TemplateId == command.TemplateId)
                        .Where(x => x.GroupId == null)
                        .AnyAsync(x => x.Name == command.Name && x.GenerationTypeId == command.GenerationTypeId, cancellation);
                    }
                    else if (command.GroupId != null)
                    {
                        return !await _context.Groups.AsNoTracking()
                        .Where(x => x.TemplateId == command.TemplateId)
                        .Where(x => x.GroupId != null)
                        .AnyAsync(x => x.Name == command.Name && x.GroupId == command.GroupId, cancellation);
                    }
                    return true;
                }).WithMessage(ValidatorMessages.AlreadyExists("Group with provided Name and GenerationType"));
            });

            RuleFor(x => x.GenerationTypeId).NotEmpty()
                .WithMessage(ValidatorMessages.NotEmpty("GenerationTypeId"))
                .When(x => x.Type == "group");
        }
    }
}
