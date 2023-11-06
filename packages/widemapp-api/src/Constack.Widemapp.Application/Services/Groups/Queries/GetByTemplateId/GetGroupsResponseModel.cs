namespace Constack.Widemapp.Application.Services.Groups.Queries.GetByTemplateId
{
    public class GetGroupsResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TemplateId { get; set; }
        public string Type { get; set; }
    }
}
