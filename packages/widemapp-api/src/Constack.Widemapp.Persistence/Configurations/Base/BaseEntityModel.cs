namespace Constack.Widemapp.Persistence.Configurations.Base
{
    public class BaseEntityModel
    {
        public bool IsKey { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; } = true;

        public static IList<BaseEntityModel> GetList()
        {
            return new List<BaseEntityModel> {
                new BaseEntityModel { IsKey = true, Property = "Id", Value = "ID", },
                new BaseEntityModel { IsKey = false, Property = "CreatedAt", Value = "CreatedAt" },
                new BaseEntityModel { IsKey = false, Property = "UpdatedAt", Value = "UpdatedAt" },
            };
        }
    }
}
