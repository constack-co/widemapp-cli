namespace Constack.Widemapp.Application.Services.Plans.Commands.Finish
{
    public class FinishPlanResponseModel
    {
        public Guid PlanId { get; set; }
        public IList<ApiResonseModel> Apis { get; set; }
    }

    public class ApiResonseModel
    {
        public Guid ApiId { get; set; }
        public Guid PlanApiId { get; set; }
    }
}
