using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace ContextFree
{
    /// <summary>
    /// Class NPDA
    /// </summary>
    public class NPDA
    {
        public NPDA(string name,string alphabet, string pop, string push, string nextstate,bool finalstate)
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

        /// <summary>
        /// Function for convert to suitable data structure
        /// </summary>
        /// <returns>List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[]>
        ///                     (nextstate,    start,      pop,      copystates,      stat,     states)</returns>
        public static List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string>> Convert()
        {
            string[][] States = Stream.StreamReader1();
            NPDA[] npda = new NPDA[States.Length - 4];
            List<string> final = new List<string>();
            string start = "";
            string fina = "";
            List<string> stat = new List<string>();
            for (int i = 4; i < States.Length; i++)
            {
               
                string Name = States[i][0];
                if (Name[0].ToString() == "-" && Name[1].ToString() == ">")
                {
                    start = Name.Replace("->", "");
                }
                Name = States[i][0].Replace("->", "");
                string Alpahbet = States[i][1];
                string Pop = States[i][2];
                string Push = States[i][3];
                string NextState = States[i][4].Replace("\r", "");
                if (NextState[0].ToString() == "*")
                {
                    fina = NextState.Replace("*", "");
                }
                NextState = States[i][4].Replace("\r", "").Replace("*", "");
                bool FinalState = false;
                if (Name[0].ToString() == "*")
                {
                    FinalState = true;
                }
                if (stat.Count == 0)
                {
                    stat.Add(Name.Replace("->", ""));
                }
                else
                {
                    for (int j = 0; j < stat.Count; j++)
                    {
                        if (Name.Replace("->", "") != stat[j])
                        {
                            stat.Add(Name.Replace("->", ""));
                        }
                        if (NextState.Replace("*", "") != stat[j])
                        {
                            stat.Add(NextState.Replace("*", ""));
                            break;
                        }
                    }
                }
                NPDA Npda = new NPDA(Name, Alpahbet, Pop, Push, NextState, FinalState);
                npda[i - 4] = Npda;
            }

            List<NPDA> copystates = new List<NPDA>();
            for (int i = 0; i < npda.Length; i++)
            {
                if (npda[i].Push != "_")
                {
                    copystates.Add(npda[i]);
                }
            }

            List<string> pop = new List<string>();
            List<string> push = new List<string>();
            List<string> nextstate = new List<string>();
            for (int i = 0; i < npda.Length; i++)
            {
                if (npda[i].Push != "_")
                {
                    pop.Add(npda[i].Pop);
                    push.Add(npda[i].Push);

                }
                if (nextstate.Count == 0)
                {
                    nextstate.Add(npda[i].NextState);
                }
                else
                {
                    for (int j = 0; j < nextstate.Count; j++)
                    {
                        if (npda[i].NextState != nextstate[j])
                        {
                            nextstate.Add(npda[i].NextState);
                        }
                    }
                }
            }
            List <Tuple< List<string>,string, List< string > ,List < NPDA > ,List<string> ,NPDA[], string>> convert = new List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>,NPDA[], string>>();
            convert.Add(new Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string>(nextstate, start, pop, copystates, stat, npda,fina));
            return convert;
        }
    }
}