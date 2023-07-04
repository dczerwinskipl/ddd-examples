using System.Text.Json.Serialization;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace DomainDrivenDesign.Core.ValueObjects;

public class ObjectId : ValueObject
{
    /// <summary>
    /// Type of object
    /// </summary>
    public string Type { get; }
    /// <summary>
    /// Identifier of object
    /// </summary>
    public string Id { get; }

    [JsonConstructor]
    private ObjectId(string type, string id)
    {
        Type = type;
        Id = id;
    }

    /// <summary>
    /// Create new isntance of source
    /// </summary>
    /// <param name="id">source identifier</param>
    /// <param name="type">source type</param>
    /// <returns>New SourceId instance</returns>
    public static ObjectId New(string type, string id) => new ObjectId(type, id);

    /// <summary>
    /// Create a copy of current SourceId
    /// </summary>
    /// <returns>New SourceId instance</returns>
    public ObjectId Copy() => new ObjectId(Id, Type);

    public override string ToString() => $"({Type},{Id})";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Type;
    }
}