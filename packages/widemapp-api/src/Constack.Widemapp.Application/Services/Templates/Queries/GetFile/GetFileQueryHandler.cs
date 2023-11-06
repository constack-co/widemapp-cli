using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, GetFileResponseModel>
    {
        private readonly IAutomationDbContext _context;
        public GetFileQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetFileResponseModel> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _context.Files
                .AsNoTracking()
                .Include(x => x.FileEdits)
                .Select(file => new GetFileResponseModel
                {
                    Id = file.Id,
                    Name = file.Name,
                    Action = file.Action,
                    ContentAdd = file.ContentAdd,
                    FileEdits = file.FileEdits.Select(fileEdit => new FileEditModel
                    {
                        Id = fileEdit.Id,
                        Placeholder = fileEdit.Placeholder,
                        Content = fileEdit.Content,
                        FileId = fileEdit.FileId
                    }).ToList(),
                    GroupId = file.GroupId,
                    TemplateId = file.TemplateId
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (file == null) throw new NotFoundException($"{request.Id} not found");
            return file;
        }
    }
}
