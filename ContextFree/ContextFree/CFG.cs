namespace ContextFree
{
    public class CFG
    {
        public CFG(string name, string alphabet, string[][] pro)
        {
            this.Name = name;
            this.Alphabet = alphabet;
            this.Pro = pro;
        }

        public string Name { get; set; }
        public string Alphabet { get; set; }
        public string[][] Pro { get; set; }
    }
}