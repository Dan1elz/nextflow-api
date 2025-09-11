using System.ComponentModel.DataAnnotations;

namespace nextflow.Domain.Models.Base;

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
        CreateAt = DateTime.Now;
    }

    public void Update()
    {
        UpdateAt = DateTime.Now;
    }
    public void Delete()
    {
        IsActive = false;
        UpdateAt = DateTime.Now;
    }
    public void Reactivate()
    {
        if (!IsActive)
        {
            IsActive = true;
            UpdateAt = DateTime.Now;
        }
    }
}
