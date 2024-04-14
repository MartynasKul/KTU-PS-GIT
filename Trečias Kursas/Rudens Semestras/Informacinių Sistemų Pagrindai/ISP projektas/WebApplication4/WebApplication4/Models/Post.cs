
namespace WebApplication4.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Count { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public int ItemId { get; set; }
        public List<Comment> comments { get; set; }
        public string User_ { get; set; }
        
        public string Item { get; set; }

        public Post()
        {

        }

        public Post(int Id, string Text, int UserId, string Title, int ItemId)
        {
            this.Id = Id;
            this.Date = DateTime.Now;
            this.Text = Text;
            this.Likes = 0;
            this.Count = 0;
            this.UserId = UserId;
            this.Title = Title;
            this.ItemId = ItemId;
            this.comments = new List<Comment>();
            this.User_ = "";
            this.Item = "";
        }

    }
}
