namespace Domain.Entites.Core;

public interface IEntityTimeStamps
{
    DateTime CreatedDate { get; }
    DateTime? UpdatedDate { get; }


    void SetUpdatedDate(DateTime dateTime);
}
