namespace Constack.Widemapp.Common.Extensions
{
    public static class TypeExtension
    {
        public static bool HasProperty(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName) != null;
        }
    }
}
