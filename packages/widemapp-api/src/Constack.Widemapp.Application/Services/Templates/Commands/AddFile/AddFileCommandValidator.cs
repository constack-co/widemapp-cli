using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Templates.Commands.AddFile
{
    public class AddFileCommandValidator : AbstractValidator<AddFileCommand>
    {
        private readonly IAutomationDbContext _context;
        public AddFileCommandValidator(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.GroupId).MustAsync(async (id, cancellation) =>
            {
                return await _context.Groups.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
            }).WithMessage(ValidatorMessages.NotFound("Group"));

            RuleFor(x => x.TemplateId).MustAsync(async (id, cancellation) =>
            {
                return await _context.Templates.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
            }).WithMessage(ValidatorMessages.NotFound("Template"));

            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Name")).DependentRules(() =>
            {
                RuleFor(x => new { x.Name, x.GroupId }).MustAsync(async (command, cancellation) =>
                {
                    return !await _context.Files.AsNoTracking()
                        .AnyAsync(x => x.Name == command.Name && x.GroupId == command.GroupId, cancellation);
                }).WithMessage(ValidatorMessages.AlreadyExists("File with provided Name"));
            });
        }
    }
}
