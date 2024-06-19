using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class Goal : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }
}