namespace OptimizelyDeleteMissingCommerceProperties;

public class MissingCommercePropertiesService(IContentTypeRepository contentTypeRepository,
                                              IPropertyDefinitionRepository propertyDefinitionRepository) : IMissingCommercePropertiesService
{
    private readonly IContentTypeRepository _contentTypeRepository = contentTypeRepository ?? throw new ArgumentNullException(nameof(contentTypeRepository));
    private readonly IPropertyDefinitionRepository _propertyDefinitionRepository = propertyDefinitionRepository ?? throw new ArgumentNullException(nameof(propertyDefinitionRepository));

    public List<PropertyModel> ListPropertiesToBeRemoved()
    {
        var propsToRemove = new List<PropertyModel>();
        var allTypes = _contentTypeRepository.List().ToList();

        // get types that derives from CatalogContentBase:
        var catalogContentBaseTypes = allTypes.Where(t => typeof(CatalogContentBase).IsAssignableFrom(t.ModelType)).ToList();

        foreach (var contentType in catalogContentBaseTypes)
        {
            var objectProps = contentType.ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var pd in contentType.PropertyDefinitions)
            {
                if (objectProps.All(p => p.Name != pd.Name)) propsToRemove.Add(new PropertyModel(contentType, pd));
            }
        }

        return propsToRemove;
    }
    
    public void Remove(List<PropertyModel> listOfPropertiesToRemove)
    {
        var ctx = CatalogContext.MetaDataContext;

        var metaClassCollection = MetaClass.GetList(ctx);
        var metaClasses = new List<MetaClass>();

        // ReSharper disable once GenericEnumeratorNotDisposed - this does not need to be disposed
        var enumerator = metaClassCollection.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Current is MetaClass metaClass) metaClasses.Add(metaClass);
        }

        foreach (var property in listOfPropertiesToRemove)
        {
            var propertyDefinitionType = _propertyDefinitionRepository.Load(property.PropertyDefinition.ID);
            var propertyToRemove = propertyDefinitionType.CreateWritableClone();
            _propertyDefinitionRepository.Delete(propertyToRemove);
            
            // In Commerce, there cannot be a class with the same Name:
            var metaClass = metaClasses.FirstOrDefault(x => x.Name == property.Type.Name);
            
            if (metaClass != null)
            {
                var mc = MetaClass.Load(ctx, metaClass.Id);
                mc.DeleteField(property.Name());
            }
        }
    }
}