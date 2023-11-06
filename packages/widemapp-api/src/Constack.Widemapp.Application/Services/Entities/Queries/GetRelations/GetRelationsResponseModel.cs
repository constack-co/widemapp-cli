namespace Constack.Widemapp.Application.Services.Entities.Queries.GetRelations
{
    public class GetRelationsResponseModel
    {
        public Guid From { get; set; }
        public Guid To { get; set; }
        public string FromPort { get; set; }
        public string ToPort { get; set; }
    }
}
