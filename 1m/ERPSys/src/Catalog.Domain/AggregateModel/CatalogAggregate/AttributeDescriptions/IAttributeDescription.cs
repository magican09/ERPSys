namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public interface IAttributeDescription
{
    public Type AttributeType { get;  }
    public string Name { get;  }
    public string? Description { get;  }
    public string? Synonym { get;  }
}