namespace Catalogs.Domain.AggregateModel.Common;

public interface IValueable<T>
{
    public T Value { get; }
}