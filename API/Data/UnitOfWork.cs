using System;
using API.Interfaces;

namespace API.Data;

public class UnitOfWork (DataContext context, 
    IUserRepository userRerository, IMessageRerository messageRerository,
    ILikesRepository likesRepository) : IUnitOfWork
{
    public IUserRepository UserRerository => userRerository;
    public IMessageRerository MessageRerository => messageRerository;
    public ILikesRepository LikesRepository => likesRepository;
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
