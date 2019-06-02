namespace ContextFree
{
    public class States
    {
        public States(string alphabet, string pop, string push, string nextstate,bool finalstate)
        {
            this.Alpahbet = alphabet;
            this.Pop = pop;
            this.Push = push;
            this.NextState = nextstate;
            this.FinalState = finalstate;
        }

        public string Alpahbet { get; set; }
        public string Pop { get; set; }
        public string Push { get; set; }
        public string NextState { get; set; }
        public bool FinalState { get; set; }
    }
}