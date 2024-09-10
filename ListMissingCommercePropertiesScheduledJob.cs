namespace OptimizelyDeleteMissingCommerceProperties;

[ScheduledPlugIn(DisplayName = "[Opti Addons] Delete missing Commerce data: List", 
    Description = "Lists properties that are not present in the Commerce model classes",
    GUID = "7F349163-030E-479C-A858-289570472B39")]
public class ListMissingCommercePropertiesScheduledJob : ScheduledJobBase
{
    private readonly IMissingCommercePropertiesService _missingCommercePropertiesService;

    public ListMissingCommercePropertiesScheduledJob(IMissingCommercePropertiesService missingCommercePropertiesService)
    {
        _missingCommercePropertiesService = missingCommercePropertiesService ?? throw new ArgumentNullException(nameof(missingCommercePropertiesService));
        
        IsStoppable = true;
    }

    public override string Execute()
    {
        var listPropertiesToBeRemoved = _missingCommercePropertiesService.ListPropertiesToBeRemoved()
                                                                         .OrderBy(p => p.Type.Name)
                                                                         .ThenBy(p => p.Name())
                                                                         .ToList();
        
        if (listPropertiesToBeRemoved.Count == 0) return "No properties to be removed";

        var sb = new StringBuilder();
        sb.AppendLine("Properties to be removed: (type.name)");
        listPropertiesToBeRemoved.ForEach(p => sb.Append($"{p.Type.Name}.{p.Name()}<br>"));
        
        return sb.ToString();
    }
}