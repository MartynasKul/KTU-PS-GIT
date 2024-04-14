namespace WebApplication4.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public DateTime Date { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public Comment()
        {

        }

        public Comment(int Id, string Text, int Likes, int PostId, int UserId)
        {
            this.Id = Id;
            this.Text = Text;
            this.Likes = Likes;
            this.Date = DateTime.Now;
            this.PostId = PostId;
            this.UserId = UserId;
        }
        

        
    }
}
