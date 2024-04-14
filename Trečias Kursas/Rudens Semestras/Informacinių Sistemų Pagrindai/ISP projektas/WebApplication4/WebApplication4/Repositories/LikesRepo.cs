using WebApplication4.Models;
using System.Security.Cryptography;
namespace WebApplication4.Repositories
{
    public class LikesRepo
    {

        public static void AddLike(Likes newLike)
        {
            var query = @"INSERT INTO `likes` (Date, fk_PostID, UserID) 
                  VALUES (?, ?, ?)";

            Sql.Query(query, args =>
            {
                args.Add("?Date_", newLike.CreatedAt);
                args.Add("?fk_PostID", newLike.PostId);
                args.Add("?fk_UserID", newLike.UserId);
            });
        }

        public static List<Likes> GetLikesByPostId(int postId)
        {
            var query = $@"SELECT * FROM `likes` WHERE fk_PostID = ?postId";
            var dataReader = Sql.Query(query, args =>
            {
                args.Add("?postId", postId);
            });

            try
            {
                var results = new List<Likes>();
                Sql.MapAll<Likes>(dataReader, (dataRecord, like) =>
                {
                    like.Id = dataRecord.From<int>("ID");
                    like.CreatedAt = dataRecord.From<DateTime>("Date");
                    like.PostId = dataRecord.From<int>("fk_PostID");
                    like.UserId = dataRecord.From<int>("UserID");

                    results.Add(like);
                });

                return results;
            }
            catch
            {
                Console.WriteLine("Likes for post ID '{0}' not found", postId);
                return new List<Likes>();
            }
        }


        public static Likes GetLikeByUserIdAndPostId(int userId, int postId)
        {
            var query = $@"SELECT * FROM `likes` WHERE UserID = ?userId AND fk_PostID = ?postId";
            var dataReader = Sql.Query(query, args =>
            {
                args.Add("?userId", userId);
                args.Add("?postId", postId);
            });

            try
            {
                Likes like = null;

                Sql.MapAll<Likes>(dataReader, (dataRecord, result) =>
                {
                    like = new Likes
                    {
                        Id = dataRecord.From<int>("ID"),
                        CreatedAt = dataRecord.From<DateTime>("Date"),
                        PostId = dataRecord.From<int>("fk_PostID"),
                        UserId = dataRecord.From<int>("UserID")
                    };
                });

                return like;
            }
            catch
            {
                // Handle exceptions if needed
                Console.WriteLine($"Error retrieving like for user ID '{userId}' and post ID '{postId}'");
                return null;
            }
        }

        public static void DeletelIKESsByPostID(int postId)
        {
            var query = "DELETE FROM `likes` WHERE fk_PostID = ?postId";

            Sql.Query(query, args =>
            {
                args.Add("?postId", postId);
            });
        }

        public void CalculateAndAddLikesAsPoints()
        {
            // Get the start date and end date of the past week
            DateTime startDate = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek - 6);
            DateTime endDate = startDate.AddDays(8).AddSeconds(-1);

            var query = $@"SELECT post.fk_UserID, COUNT(*) AS LikesCount
                        FROM likes
                        JOIN post ON likes.fk_PostID = post.PostID
                        WHERE likes.Date >= ?startDate AND likes.Date <= ?endDate
                        GROUP BY post.fk_UserID;";

            var dataReader = Sql.Query(query, args =>
            {
                args.Add("?startDate", startDate);
                args.Add("?endDate", endDate);
            });

            try
            {
                // Dictionary to store total likes for each user
                var likesByUser = new Dictionary<int, int>();

                // Map the results to the dictionary
                Sql.MapAll<Likes>(dataReader, (dataRecord, like) =>
                {
                    // Use the correct column names
                    int userId = dataRecord.From<int>("fk_UserID");
                    int likeCount = dataRecord.From<int>("LikesCount");

                    // Accumulate likes for each user
                    if (likesByUser.ContainsKey(userId))
                    {
                        likesByUser[userId] += likeCount;
                    }
                    else
                    {
                        likesByUser[userId] = likeCount;
                    }
                });

                // Print user information after populating the dictionary
                foreach (var entry in likesByUser)
                {
                    int userId = entry.Key;
                    int totalLikes = entry.Value;
                    Console.WriteLine($"User ID: {userId}, Total Likes: {totalLikes}");
                }

                // Update user points based on total likes
                foreach (var entry in likesByUser)
                {
                    int userId = entry.Key;
                    int totalLikes = entry.Value;
                    UserRepo.AddPointsToUser(userId, totalLikes);
                }
            }
            catch
            {
                Console.WriteLine("Error calculating and adding likes as points for users in the past week");
            }
        }
    }
}
