namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public interface IAttributeDescription
{
    public Type AttributeType { get;  }
    public string AttributeName { get; set;  }
    public string? Description { get; set;  }
    public string? Synonym { get;  set; }
    public Type? Type { get;  }
   
    
}