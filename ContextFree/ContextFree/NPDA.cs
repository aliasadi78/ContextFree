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
        public NPDA(string name,string alphabet, string pop, string push, string nextstate)
        {
            this.Name = name;
            this.Alpahbet = alphabet;
            this.Pop = pop;
            this.Push = push;
            this.NextState = nextstate;
        }

        public string Name;
        public string Alpahbet;
        public string Pop;
        public string Push;
        public string NextState;

        /// <summary>
        /// Function for convert to suitable data structure
        /// </summary>
        /// <returns>List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[]>
        ///                     (nextstate,    start,      pop,      copystates,      stat,     states)</returns>
        public static List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string[]>> Convert()
        {
            string[][] States = Stream.StreamReader1();    //exmaple: listline[6][0] = "q0" listline[6][1] = "a" listline[6][2] = "1" listline[6][3] = "_" listline[6][4] = "q0"
            NPDA[] npda = new NPDA[States.Length - 4];
            string symbol = States[3][0].ToString();       //example: "$"
            string start = "";                             //example: "->q0"
            string[] final = new string[2];                //finalstate & symbol
            List<string> states = new List<string>();      //q0, q1, ...
            for (int i = 4; i < States.Length; i++)
            {
                string Name = States[i][0];
                //remove "->"
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
                    final[0] = NextState.Replace("*", "");
                }
                NextState = States[i][4].Replace("*", "");
                
                if (states.Count == 0)
                {
                    states.Add(Name.Replace("->", ""));
                }
                else
                {
                    for (int j = 0; j < states.Count; j++)
                    {
                        if (Name.Replace("->", "") != states[j])
                        {
                            states.Add(Name.Replace("->", ""));
                        }
                        if (NextState.Replace("*", "") != states[j])
                        {
                            states.Add(NextState.Replace("*", ""));
                            break;
                        }
                    }
                }
                NPDA Npda = new NPDA(Name, Alpahbet, Pop, Push, NextState);
                npda[i - 4] = Npda;                //example: npda[0].Alphabet = "a" npda[0].Name = "q0" npda[0].NextState = "q0" npda[0].Pop = "$" npda[0].Push = "0$"
            }

            //copystates for if npda[i].push != "_"
            List<NPDA> copystates = new List<NPDA>();
            for (int i = 0; i < npda.Length; i++)
            {
                if (npda[i].Push != "_")
                {
                    copystates.Add(npda[i]);
                }
            }

            List<string> pop = new List<string>();       //list pop's               example: [$,0,$,1]
            List<string> push = new List<string>();      //list push's              example: [0$,00,1$,11]
            List<string> nextstate = new List<string>(); //list nextstate's         example: [q0,q1]
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

            final[1] = symbol;
            List <Tuple< List<string>,string, List< string > ,List < NPDA > ,List<string> ,NPDA[], string[]>> convert = new List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>,NPDA[], string[]>>();
            convert.Add(new Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string[]>(nextstate, start, pop, copystates, states, npda,final));
            return convert;
        }
    }
}