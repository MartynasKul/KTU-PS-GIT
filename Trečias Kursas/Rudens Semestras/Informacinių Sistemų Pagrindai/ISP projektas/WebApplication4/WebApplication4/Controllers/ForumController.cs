using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace WebApplication4.Controllers
{
    public class ForumController : Controller
    {
        private static List<Post> Posts = new List<Post>();
        private static List<Item> Items = new List<Item>();
        private static User user;
        private readonly UserRepo _userRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ForumController(UserRepo userRepo, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _httpContextAccessor = httpContextAccessor;
            ReadItems();
        }

        public void ReadItems()
        {
            Posts = PostRepo.GetAllPosts();
            Items = ItemRepo.GetAllItems();
            user = _userRepo.FindUserByEmail(_httpContextAccessor.HttpContext.Session.GetString("Email"));

        }

       

        public Post GetPost(int id)
        {

            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return Posts.Find(p => p.Id == id);
        }

        [HttpPost]
        public IActionResult CreatePost(CreatePostViewModel model, int UserId, int ItemId)
        {


            if (ModelState.IsValid)
            {
                var newPost = new Post(0, model.Text, user.ID, model.Title, model.ItemId);
                PostRepo.AddPost(newPost);

                return RedirectToAction("Forum");
            }

            return View(model);
        }



        public IActionResult ViewPosts(int id)
        {

            // Find the post by ID
            var post = GetPost(id);

            if (post == null)
            {
                // Handle the case where the post is not found, e.g., return a "Not Found" view
                return NotFound();
            }

            return View(post);
        }


        public IActionResult ViewPost(int id)
        {

            var Posts = GetPost(id);
            return View(Posts);
        }

        public IActionResult Forum()
        {
            ViewBag.Items = new SelectList(ItemRepo.GetAllItems(), "Id", "Name");
            return View(Posts);
        }

        public IActionResult AddComment(int postId, string commentText, int currentUserId)
        {
            

            var newComment = new Comment
            {
                Id = 0, // Assuming the database generates the ID
                Text = commentText,
                Likes = 0,
                Date = DateTime.Now,
                PostId = postId,
                UserId = user.ID
            };

            CommentRepo.CreateComment(newComment);

            return RedirectToAction("Forum");
        }


        public IActionResult AddLike(int postId)
        {
            
            // Assuming you have a method to get the current user's ID
            var currentUserId = user.ID; // Replace with your actual implementation

            Likes likes = LikesRepo.GetLikeByUserIdAndPostId(currentUserId, postId);

            // Check if the user has already liked the post
            if (LikesRepo.GetLikeByUserIdAndPostId(currentUserId, postId) != null)
            {
                // User has already liked the post, return an error message
                TempData["ErrorMessage"] = "You have already liked this post.";
                return RedirectToAction("Forum");
            }

            // User has not liked the post, add a new like
            var newLike = new Likes
            {
                CreatedAt = DateTime.Now,
                PostId = postId,
                UserId = currentUserId
            };

            LikesRepo.AddLike(newLike);

            return RedirectToAction("Forum");
        }


        [HttpPost]
        public IActionResult DeletePost(int postId)
        {

            // Assuming you have a method to get the current user's ID
            // Replace GetCurrentUserId() with your actual implementation
            var currentUserId = user.ID;

            var post = PostRepo.GetPost(postId);

            if (post == null)
            {
                return NotFound();
            }

            // Ensure that the user attempting to delete the post is the post owner
            /*if (post.UserId != currentUserId)
            {
                return Forbid(); // Or return a different status code based on your authentication logic
            }*/

            // Delete comments associated with the post
            CommentRepo.DeleteCommentsByPostID(postId);
            LikesRepo.DeletelIKESsByPostID(postId);
            Console.WriteLine("CANSER");
            // Delete the post
            PostRepo.DeletePost(postId);

            return RedirectToAction("Forum");
        }
    }
}
