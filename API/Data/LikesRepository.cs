using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


namespace API.Data;

public class LikesRepository(DataContext context, IMapper mapper) : ILikesRepository
{
    public void AddLike(UserLike like)
    {
        context.Likes.Add(like);
    }

    public void DeleteLike(UserLike like)
    {
        context.Likes.Remove(like);
    }

    public async Task<IEnumerable<int>> GetCurrentUserLikeId(int currentUserId)
    {
        return await context.Likes.Where(x => x.SourseUserId == currentUserId)
            .Select(x=> x.TargetUserId)
            .ToListAsync();
    }

    public async Task<UserLike?> GetUserLike(int sourseUserId, int targetUserId)
    {
        return await context.Likes.FindAsync(sourseUserId, targetUserId);
    }

    public async Task<PagedList<MemberDto>> GetUserLikes(LikesParams likesParams)
    {
        var likes = context.Likes.AsQueryable();

        IQueryable<MemberDto> query;

        switch (likesParams.Predicate) 
        {
            case "liked":
                query = likes.Where(x=> x.SourseUserId == likesParams.UserId)
                    .Select(x=> x.TargetUser)
                    .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                break;
            case "likedBy":
               query = likes.Where(x=> x.TargetUserId == likesParams.UserId)
                    .Select(x=> x.SourseUser)
                    .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                break;
            default:
                var likedIds = await GetCurrentUserLikeId(likesParams.UserId);

                query = likes.Where(x=> x.TargetUserId == likesParams.UserId && likedIds.Contains(x.SourseUserId))
                    .Select(x=> x.SourseUser)
                    .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                break;      
        }

        return await PagedList<MemberDto>.CreateAsync(query, likesParams.pageNumber, likesParams.PageSize);
    }
}
