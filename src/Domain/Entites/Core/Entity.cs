namespace Domain.Entites.Core;

public class Entity : IEntityTimeStamps
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }

    public Entity()
    {
        if (Id == Guid.Empty)
        {
            Id = Guid.NewGuid();
        }
        CreatedDate = DateTime.UtcNow;
    }

    public void SetUpdatedDate(DateTime dateTime)
    {
        UpdatedDate = dateTime;
    }
}
