using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Templates.Commands.RenameFile
{
    public class RenameFileCommandValidator : AbstractValidator<RenameFileCommand>
    {
        private readonly IAutomationDbContext _context;
        public RenameFileCommandValidator(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
            {
                return await _context.Files.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
            }).WithMessage(ValidatorMessages.NotFound("File Id"));

            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Name")).DependentRules(() =>
            {
                RuleFor(x => new { x.Id, x.Name }).MustAsync(async (command, cancellation) =>
                {
                    var file = await _context.Files.AsNoTracking().FirstOrDefaultAsync(x => x.Id == command.Id);

                    return !await _context.Files.AsNoTracking()
                            .Where(x => x.GroupId == file.GroupId)
                            .AnyAsync(x => x.Name == command.Name && x.Id != command.Id);
                    
                }).WithMessage(ValidatorMessages.AlreadyExists("File with provied name"));
            });
        }
    }
}
