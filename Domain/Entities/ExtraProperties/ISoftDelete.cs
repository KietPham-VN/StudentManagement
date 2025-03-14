namespace StudentManagement.Domain.Entities.ExtraProperties
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        int? DeletedBy { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}