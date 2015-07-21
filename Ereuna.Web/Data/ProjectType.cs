namespace Ereuna.Web.Data
{
    public class ProjectType
    {
        public static readonly int BookOrSeries = 1;
        public static readonly int Script = 2; // Movies or theatre
        public static readonly int Comic = 3;


        public int Id { get; set; }
        public string Title { get; set; }
    }
}