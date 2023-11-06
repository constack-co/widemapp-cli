namespace Constack.Widemapp.Application.Services.Templates.Queries.GetFile
{
    public class GetFileResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string ContentAdd { get; set; }
        public IList<FileEditModel> FileEdits { get; set; } = new List<FileEditModel>();
        public Guid GroupId { get; set; }
        public Guid TemplateId { get; set; }
    }

    public class FileEditModel
    {
        public Guid Id { get; set; }
        public string Placeholder { get; set; }
        public string Content { get; set; }
        public Guid FileId { get; set; }
    }
}
