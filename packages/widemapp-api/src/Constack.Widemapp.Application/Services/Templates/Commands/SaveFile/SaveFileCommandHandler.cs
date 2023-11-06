using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Exceptions;
using Constack.Widemapp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Commands.SaveFile
{
    public class SaveFileCommandHandler : IRequestHandler<SaveFileCommand, Unit>
    {
        private readonly IAutomationDbContext _context;
        public SaveFileCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(SaveFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (file == null) throw new NotFoundException($"{request.Id} not found");
            if (request.ContentAdd != null)
            {
                file.ContentAdd = request.ContentAdd;
            }

            IList<FileEdit> fileEdits = new List<FileEdit>();
            if (request.FileEdits != null && request.FileEdits.Count > 0)
            {
                foreach (var edit in request.FileEdits)
                {
                    if (edit.Id != null)
                    {
                        var fileEditToUpdate = await _context.FileEdits.FirstOrDefaultAsync(x => x.Id == edit.Id);
                        fileEditToUpdate.Placeholder = edit.Placeholder;
                        fileEditToUpdate.Content = edit.Content;
                    }
                    else
                    {
                        fileEdits.Add(new FileEdit
                        {
                            Id = Guid.NewGuid(),
                            Placeholder = edit.Placeholder,
                            Content = edit.Content,
                            FileId = request.Id
                        });
                    }
                }
            }
            _context.FileEdits.AddRange(fileEdits);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
