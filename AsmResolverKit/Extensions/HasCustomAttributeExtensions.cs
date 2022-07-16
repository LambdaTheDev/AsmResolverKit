using System.Linq;
using AsmResolver.DotNet;

namespace LambdaTheDev.AsmResolverKit.Extensions
{
    // Extension methods for custom attribute providers
    public static class HasCustomAttributeExtensions
    {
        // This method returns parsed CustomAttribute, or null if attribute is not present
        public static T? FindAndParseCustomAttribute<T>(this IHasCustomAttribute provider, string attrNamespace, string attrName) where T : class, new()
        {
            // Try obtain CustomAttribute
            CustomAttribute? attribute = provider.FindCustomAttributes(attrNamespace, attrName).FirstOrDefault();
            if (attribute == null) return null;

            // & parse it
            return attribute.Parse<T>();
        }
    }
}