namespace API.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRerository {get;}
    IMessageRerository MessageRerository {get;}
    ILikesRepository LikesRepository {get;}
    Task<bool> Complete();
    bool HasChanges();
}
