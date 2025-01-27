namespace OptimizelyDeleteMissingCommerceProperties;

public class PropertyModel(ContentType type, PropertyDefinition propertyDefinition)
{
    public ContentType Type { get; set; } = type ?? throw new ArgumentNullException(nameof(type));
    public PropertyDefinition PropertyDefinition { get; set; } = propertyDefinition ?? throw new ArgumentNullException(nameof(propertyDefinition));
    
    public string Name() => PropertyDefinition.Name;
}