using System;
using System.Reflection;
using AsmResolver;
using AsmResolver.DotNet;

namespace LambdaTheDev.AsmResolverKit.Extensions
{
    // This class allows you to parse CustomAttribute into a provided model (with the same properties name)
    public static class CustomAttributeParserExtensions
    {
        public static T? Parse<T>(this CustomAttribute attribute) where T : class, new()
        {
            // Prepare result data
            T result = new T();
            Type resultType = typeof(T);

            if (attribute.Signature == null) return null;
        
            // Loop through argument properties & find result properties that match
            foreach (var attributeArg in attribute.Signature.NamedArguments)
            {
                PropertyInfo? resultProperty = resultType.GetProperty(attributeArg.MemberName?.Value!);
                if(resultProperty == null) continue;

                // String param is parsed to UTF8 string by AsmResolver. Here I fix this issue
                object? propertyValue = attributeArg.Argument.Element;
                if (propertyValue != null && propertyValue is Utf8String utf8String)
                    propertyValue = utf8String.Value;
            
                // Set value
                resultProperty.SetValue(result, propertyValue);
            }

            return result;
        }
    }
}