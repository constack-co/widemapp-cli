using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Commands.SaveFile
{
    public class SaveFileCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string? ContentAdd { get; set; }
        public IList<FileEditModel>? FileEdits { get; set; }
    }

    public class FileEditModel
    {
        public Guid? Id { get; set; }
        public string Placeholder { get; set; }
        public string Content { get; set; }
    }
}