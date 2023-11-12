using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Menu.ValueObjects;

public sealed class MenuReviewId : ValueObject
{
    public Guid Value { get; private set; }
    
    private MenuReviewId(Guid value)
    {
        Value = value;
    }

    private MenuReviewId()
    {
    }

    public static MenuReviewId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}