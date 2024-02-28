namespace Gultan.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Token> Tokens { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}