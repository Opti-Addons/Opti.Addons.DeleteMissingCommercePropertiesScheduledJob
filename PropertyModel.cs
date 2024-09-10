namespace OptimizelyDeleteMissingCommerceProperties;

public class PropertyModel(Type type, PropertyDefinition propertyDefinition)
{
    public Type Type { get; set; } = type ?? throw new ArgumentNullException(nameof(type));
    
    public PropertyDefinition PropertyDefinition { get; set; } = propertyDefinition ?? throw new ArgumentNullException(nameof(propertyDefinition));
    
    public string Name() => PropertyDefinition.Name;
}