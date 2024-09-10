namespace OptimizelyDeleteMissingCommerceProperties;

public interface IMissingCommercePropertiesService
{
    List<PropertyModel> ListPropertiesToBeRemoved();
    void Remove(List<PropertyModel> listOfPropertiesToRemove);
}