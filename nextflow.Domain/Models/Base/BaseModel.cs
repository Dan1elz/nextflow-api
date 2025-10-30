using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Models.Base;

public class BaseModel
{
    [Key]
    public Guid Id { get; private set; }
    public DateTime CreateAt { get; private set; }
    public DateTime? UpdateAt { get; private set; }
    public bool IsActive { get; set; } = true;

    protected BaseModel()
    {
        Id = Guid.NewGuid();
        CreateAt = DateTime.UtcNow;
    }

    public void Update()
    {
        UpdateAt = DateTime.UtcNow;
    }
    public void Delete()
    {
        IsActive = false;
        UpdateAt = DateTime.UtcNow;
    }
    public void Reactivate()
    {
        if (!IsActive)
        {
            IsActive = true;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
