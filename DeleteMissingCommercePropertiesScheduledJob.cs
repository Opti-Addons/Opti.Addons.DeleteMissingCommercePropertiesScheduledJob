namespace OptimizelyDeleteMissingCommerceProperties;

[ScheduledPlugIn(DisplayName = "[Opti Addons] Delete missing Commerce data: Delete", 
    Description = "Removes properties that are not present in the model class(es). ⚠️ Warning: Please make sure to backup your database before running this job. " +
                  "You may also run the 'list' job first to see what properties will be removed.",
    GUID = "5DDFBF06-5C95-4EB7-964C-72D44133E496")]
public class DeleteMissingCommercePropertiesScheduledJob : ScheduledJobBase
{
    private readonly IMissingCommercePropertiesService _missingCommercePropertiesService;

    public DeleteMissingCommercePropertiesScheduledJob(IMissingCommercePropertiesService missingCommercePropertiesService)
    {
        _missingCommercePropertiesService = missingCommercePropertiesService ?? throw new ArgumentNullException(nameof(missingCommercePropertiesService));
        
        IsStoppable = true;
    }

    public override string Execute()
    {
        var listPropertiesToBeRemoved = _missingCommercePropertiesService.ListPropertiesToBeRemoved();

        if (!listPropertiesToBeRemoved.Any()) return "No properties to be removed";
        
        _missingCommercePropertiesService.Remove(listPropertiesToBeRemoved);

        var sb = new StringBuilder();
        sb.AppendLine("Properties removed: (type.name)");
        listPropertiesToBeRemoved.ForEach(p => sb.Append($"{p.Type.Name}.{p.Name()};<br>"));
        
        return sb.ToString();
    }
}