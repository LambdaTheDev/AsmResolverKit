using AsmResolver.DotNet;
using AsmResolver.DotNet.Signatures.Types;

namespace LambdaTheDev.AsmResolverKit
{
    // Helper methods for properties
    public static class PropertyHelper
    {
        // This method changes property return type
        public static void ChangePropertyType(PropertyDefinition property, TypeSignature newType)
        {
            if (property.Signature == null) return;
            
            // Update property return type
            property.Signature.ReturnType = newType;
            
            // Update getter & setter if present
            if (property.GetMethod != null)
                property.GetMethod.Signature!.ReturnType = newType;

            if (property.SetMethod != null)
                property.SetMethod.Signature!.ParameterTypes[0] = newType;
        }
    }
}