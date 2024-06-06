namespace Domain.Comman.Interfaces;

public interface IAudiTableEntity
{
    public int? CreateBy { get; set; }
    public int? UpdateDateBy { get; set;}
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set;}
    public bool IsDeleted { get; set; }
}
