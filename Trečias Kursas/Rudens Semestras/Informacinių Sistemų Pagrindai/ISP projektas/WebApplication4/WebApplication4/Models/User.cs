namespace WebApplication4.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Points {get;set;}
        public string UserName { get; set; }
        public int UserType {get;set;}

        public string Salt { get; set; }// passwordo hashingui
        public string Address{get;set;}
        public string PhoneNumber {get; set;}

        public List<string> UnlockedTitles { get; set; } = new List<string>();
        
    
    }
}
