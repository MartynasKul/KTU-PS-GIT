using WebApplication4.Models;
using WebApplication4.Repositories;
namespace WebApplication4.Repositories
{
    public class PostRepo
    {



        public static Post? GetPost(int postId)
        {
            var query = $@"SELECT * FROM `post` WHERE PostID = ?postId";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?postId", postId);
            });

            try
            {
                var result = Sql.MapOne<Post>(drc, (dre, post) =>
                {
                    post.Id = dre.From<int>("PostID");
                    post.Date = dre.From<DateTime>("Date_");
                    post.Text = dre.From<string>("Text_");
                    post.Title = dre.From<string>("Title");
                    post.Likes = dre.From<int>("Likes");
                    post.UserId = dre.From<int>("fk_UserID");
                    post.ItemId = dre.From<int>("fk_ItemID");
                });
                return result;
            }
            catch
            {
                Console.WriteLine("Post with ID '{0}' not found", postId);
                return null; // or new Post() if you prefer
            }
        }

        public static void AddPost(Post newPost)
        {
            var query = @"INSERT INTO `post` (Date_, Text_, Title, Likes, fk_UserID, fk_ItemID) 
                  VALUES (?, ?, ?, ?, ?, ?)";

            Sql.Query(query, args =>
            {
                args.Add("?Date_", newPost.Date);
                args.Add("?Text_", newPost.Text);
                args.Add("?Title", newPost.Title);
                args.Add("?Likes", newPost.Likes);
                args.Add("?fk_UserID", newPost.UserId);
                args.Add("?fk_ItemID", newPost.ItemId);
            });
        }



        public static List<Post> GetAllPosts()//cia reiks pakeisti priklausomai nuo duomenu lenteles , kokia bus users visa lentele kad viska grazintu , nes naudojamas Promote items dalyke kur siuncia visiem useriam i ju emailus laiskus apie itemus
        {
            var query = "SELECT * FROM `post`";

            var posts = new List<Post>();

            Sql.MapAll<Post>(Sql.Query(query), (extractor, user) =>
            {
                user.Id = extractor.From<int>("PostID");
                user.Date = extractor.From<DateTime>("Date_");
                user.Text = extractor.From<string>("Text_");
                user.Title = extractor.From<string>("Title");
                user.Likes = LikesRepo.GetLikesByPostId(user.Id).Count();
                Console.WriteLine(user.Likes);
                user.UserId = extractor.From<int>("fk_UserID");
                user.ItemId = extractor.From<int>("fk_ItemID");
                user.comments = CommentRepo.FindCommentsByPostID(user.Id);
                user.User_ = UserRepo.FindUserById(user.UserId).UserName;
                user.Item = ItemRepo.GetItemById(user.ItemId).Name;
                posts.Add(user);
            });

            return posts;
        }

        public static List<Post> GetAllPostsByUserId(int userId)
        {
            var query = $@"SELECT * FROM `post` WHERE fk_UserID = ?userId";

            var posts = new List<Post>();

            Sql.MapAll<Post>(Sql.Query(query, args => args.Add("?userId", userId)), (extractor, post) =>
            {
                post.Id = extractor.From<int>("PostID");
                post.Date = extractor.From<DateTime>("Date_");
                post.Text = extractor.From<string>("Text_");
                post.Title = extractor.From<string>("Title");
                post.Likes = extractor.From<int>("Likes");
                post.UserId = extractor.From<int>("fk_UserID");
                post.ItemId = extractor.From<int>("fk_ItemID");
                posts.Add(post);
            });

            return posts;
        }


        public static void DeletePost(int postId)
        {
            // Validate input
            if (postId <= 0)
            {
                throw new ArgumentException(nameof(postId));
            }

            // Delete associated comments first
            CommentRepo.DeleteCommentsByPostID(postId);

            // Write your SQL query for deleting the post by ID
            var query = "DELETE FROM `post` WHERE PostID = ?postId";

            // Execute the query
            Sql.Delete(query, args =>
            {
                args.Add("?postId", postId);
            });
        }
    }
}
