namespace Constack.Widemapp.Application.Services.Groups.Queries.GetByName
{
    public class GetByNameCommandResponseModel
    {
        public Guid Id { get; set; }
        public GenerationTypeModel GenerationType { get; set; }
    }

    public class GenerationTypeModel
    {
        public Guid? Id { get; set; }
        public string Value { get; set; }
    }
}
