using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class CommentRepo
    {
        public static void CreateComment(Comment newComment)
        {
            var query = @"INSERT INTO `comments` (Date_, Text_, Likes, fk_PostID, fk_UserID) 
                      VALUES (?, ?, ?, ?, ?)";

            Sql.Query(query, args =>
            {
                args.Add("?Date_", newComment.Date);
                args.Add("?Text_", newComment.Text);
                args.Add("?Likes", newComment.Likes);
                args.Add("?fk_PostID", newComment.PostId);
                args.Add("?fk_UserID", newComment.UserId);
            });
        }

        public static List<Comment> FindCommentsByPostID(int id)
        {
            var query = $@"SELECT * FROM `comments` WHERE fk_PostID = ?id";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?id", id);
            });

            try
            {
                var results = new List<Comment>();
                Sql.MapAll<Comment>(drc, (dre, t) =>
                {
                    t.Id = dre.From<int>("CommentID");
                    t.Text = dre.From<string>("Text_");
                    t.Date = dre.From<DateTime>("Date_");
                    t.Likes = dre.From<int>("Likes");
                    t.PostId = dre.From<int>("fk_PostID");
                    t.UserId = dre.From<int>("fk_UserID");

                    results.Add(t);
                });

                return results;
            }
            catch
            {
                Console.WriteLine("Comments for post ID '{0}' not found", id);
                return new List<Comment>();
            }
        }

        public static void DeleteCommentsByPostID(int postId)
        {
            var query = "DELETE FROM `comments` WHERE fk_PostID = ?postId";

            Sql.Query(query, args =>
            {
                args.Add("?postId", postId);
            });
        }



        public static List<Comment> FindCommentsByUserId(int userId)
        {
            var query = $@"SELECT * FROM `comments` WHERE fk_UserID = ?userId";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?userId", userId);
            });

            var comments = new List<Comment>();

            try
            {
                Sql.MapAll<Comment>(drc, (dre, comment) =>
                {
                    comment.Id = dre.From<int>("CommentID");
                    comment.Text = dre.From<string>("Text_");
                    comment.Date = dre.From<DateTime>("Date_");
                    comment.Likes = dre.From<int>("Likes");
                    comment.PostId = dre.From<int>("fk_PostID");
                    comment.UserId = dre.From<int>("fk_UserID");

                    comments.Add(comment);
                });
            }
            catch
            {
                Console.WriteLine("Comments for User with ID '{0}' not found", userId);
            }

            return comments;
        }



        public static void DeleteComment(int commentId)
        {
            var query = "DELETE FROM `comments` WHERE CommentID = ?commentId";

            Sql.Query(query, args =>
            {
                args.Add("?commentId", commentId);
            });
        }

    }
}
