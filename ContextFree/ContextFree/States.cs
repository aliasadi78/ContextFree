namespace ContextFree
{
    public class States
    {
        public States()
        {
        }

        public States(string name,string alphabet, string pop, string push, string nextstate,bool finalstate)
        {
            this.Name = name;
            this.Alpahbet = alphabet;
            this.Pop = pop;
            this.Push = push;
            this.NextState = nextstate;
            this.FinalState = finalstate;
        }

        public string Name;
        public string Alpahbet;
        public string Pop;
        public string Push;
        public string NextState;
        public bool FinalState;
    }
}