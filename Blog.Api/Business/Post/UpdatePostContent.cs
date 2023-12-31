using Blog.Api.DbContexts;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;


namespace Blog.Api.Business.Post;

public record UpdatePostContent(int PostId, string Content,string? UserId) : ICommand
{
    public class Handler : IRequestHandler<UpdatePostContent>
    {
        private readonly BlogInfoContext _context;

        public Handler(BlogInfoContext context) => this._context = context;

        public async Task<Unit> Handle(UpdatePostContent request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleRequiredAsync(x => x.Id == request.PostId && x.UserId == request.UserId, cancellationToken);
            post.Content = request.Content;
            await _context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}