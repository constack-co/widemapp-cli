using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Entities.Commands.AddRelation
{
    public class AddEntityRelationCommandValidator : AbstractValidator<AddEntityRelationCommand>
    {
        private readonly IAutomationDbContext _context;
        public AddEntityRelationCommandValidator(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.EntityFromId).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Entity from")).DependentRules(() =>
            {
                RuleFor(x => x.EntityFromId).MustAsync(async (id, cancellation) =>
                {
                    return await _context.Entities.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
                }).WithMessage(ValidatorMessages.NotFound("Entity from"));
            });

            RuleFor(x => x.EntityToId).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Entity to")).DependentRules(() =>
            {
                RuleFor(x => x.EntityToId).MustAsync(async (id, cancellation) =>
                {
                    return await _context.Entities.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
                }).WithMessage(ValidatorMessages.NotFound("Entity to"));
            });

            RuleFor(x => x.PropertyFromId).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Property from")).DependentRules(() =>
            {
                RuleFor(x => x.PropertyFromId).MustAsync(async (id, cancellation) =>
                {
                    return await _context.Properties.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
                }).WithMessage(ValidatorMessages.NotFound("Property from"));
            });

            RuleFor(x => x.PropertyToId).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Property to")).DependentRules(() =>
            {
                RuleFor(x => x.PropertyToId).MustAsync(async (id, cancellation) =>
                {
                    return await _context.Properties.AsNoTracking().AnyAsync(x => x.Id == id, cancellation);
                }).WithMessage(ValidatorMessages.NotFound("Property to"));
            });
        }
    }
}
