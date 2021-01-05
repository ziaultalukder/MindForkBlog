using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindForkBlog.DatabaseContext;
using MindForkBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MindForkBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext context;

        // GET: PostController

        public PostController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await context.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            var Post = await context.Posts.AddAsync(post);
            context.SaveChanges();
            return Ok(Post);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(Comment comment, int PostId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Comment cmnt = new Comment
            {
                CommentTile = comment.CommentTile,
                UserId = userId,
                CommentDate = DateTime.Now.Date,
                PostId = PostId
            };
            context.Comments.Add(cmnt);
            context.SaveChanges();

            var showComment = context.Comments.Where(c => c.PostId == PostId).ToListAsync();
            return Ok(showComment);
        }


        [HttpPost]
        public async Task<IActionResult> Like(Like like, int PostId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Like liked = new Like
            {
                LikeCount = like.LikeCount,
                UserId = userId,
                likeDate = DateTime.Now.Date,
                PostId = PostId
            };
            context.Likes.Add(liked);
            context.SaveChanges();

            var showlike = context.Likes.Where(c => c.PostId == PostId).ToListAsync();
            return Ok(showlike);
        }

        [HttpPost]
        public async Task<IActionResult> DisLike(Dislike disLike, int PostId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Dislike disliked = new Dislike
            {
                DisLikeCount = disLike.DisLikeCount,
                UserId = userId,
                DislikeDate = DateTime.Now.Date,
                PostId = PostId
            };
            context.Dislikes.Add(disliked);
            context.SaveChanges();

            var showDislike = context.Dislikes.Where(c => c.PostId == PostId).ToListAsync();
            return Ok(showDislike);
        }
    }
}
