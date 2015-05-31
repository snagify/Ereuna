namespace Ereuna.Web.Data
{
    public class UserType
    {
        public static readonly int FacebookUser = 1;
        public static readonly int EmailUser = 2;


        public int Id { get; set; } 
        public string Title { get; set; }
    }

    
}