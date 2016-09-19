namespace Ereuna.Web.Data
{
    public class CharacterType
    {
        public static readonly int Protagonist = 1;
        public static readonly int Antagonist = 2;
        public static readonly int Other = 3;

        public int Id { get; set; }

        public string Name { get; set; }
    }
}