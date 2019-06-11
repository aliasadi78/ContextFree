using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace ContextFree
{
    class Program
    {
        static void Main(string[] args)
        {
            string[][] States = StremReader();
            States[] states = new States[States.Length - 4];
            List<string> final = new List<string>();
            string start = "";
            List<string> stat = new List<string>();
            for (int i = 4; i < States.Length; i++)
            {
                string Name = States[i][0];
                if (Name[0].ToString() == "-" && Name[1].ToString() == ">")
                {
                    start = Name.Replace("->", "");
                }
                Name = States[i][0].Replace("->","");
                string Alpahbet = States[i][1];
                string Pop = States[i][2];
                string Push = States[i][3];
                string NextState = States[i][4].Replace("\r","").Replace("*","");
                bool FinalState = false;
                if (NextState[0].ToString() == "*")
                {
                    final.Add(NextState.Replace("*",""));
                }
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
                        if (NextState.Replace("*","") != stat[j])
                        {
                            stat.Add(NextState.Replace("*", ""));
                            break;
                        }
                    }
                }
                States state = new States(Name,Alpahbet,Pop,Push,NextState,FinalState);
                states[i - 4] = state;
            }

            List<States> copystates = new List<States>();
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].Push != "_")
                {
                    copystates.Add(states[i]);
                }
            }

            List<string> pop = new List<string>();            
            List<string> push = new List<string>();            
            List<string> nextstate = new List<string>();            
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].Push != "_")
                {
                    pop.Add(states[i].Pop);
                    push.Add(states[i].Push);
                    
                }
                if (nextstate.Count == 0)
                {
                    nextstate.Add(states[i].NextState);
                }
                else{ 
                    for (int j = 0; j < nextstate.Count; j++)
                    {
                        if (states[i].NextState != nextstate[j])
                        {
                            nextstate.Add(states[i].NextState);
                        }
                    }
                }
            }
            print(nextstate, start, pop, copystates, stat,states);    
        }
        
        static void print( List<string> nextstate, string start,List<string> pop, List<States> copystates,List<string> stat,States[] states)
        {
            TextWriter write = new StreamWriter("..\\..\\output.txt");
            for (int i = 0; i < pop.Count; i++)
            {
                for (int j = 0; j < nextstate.Count; j++)
                {
                    int w = j;
                    Console.Write("(" + start + pop[i] + nextstate[w] + ")->");
                    write.Write("(" + start + pop[i] + nextstate[w] + ")->");
                    for (int k = 0; k < nextstate.Count; k++)
                    {
                        j = 0;
                        if (j + k < nextstate.Count)
                        {
                            if (!(j + k + 1 >= nextstate.Count))
                            {
                                Console.Write(copystates[i].Alpahbet +
                                          "(" + start + copystates[i].Push.ToString()[0] + nextstate[j + k] + ")(" +
                                          nextstate[j + k] + copystates[i].Push.ToString()[1] + nextstate[w] + ")" + "|");
                                write.Write(copystates[i].Alpahbet +
                                        "(" + start + copystates[i].Push.ToString()[0] + nextstate[j + k] + ")(" +
                                        nextstate[j + k] + copystates[i].Push.ToString()[1] + nextstate[w] + ")" + "|");
                            }
                            else
                            {
                                Console.Write(copystates[i].Alpahbet +
                                              "(" + start + copystates[i].Push.ToString()[0] + nextstate[j + k] + ")(" +
                                              nextstate[j + k] + copystates[i].Push.ToString()[1] + nextstate[w] + ")");
                                write.Write(copystates[i].Alpahbet +
                                            "(" + start + copystates[i].Push.ToString()[0] + nextstate[j + k] + ")(" +
                                            nextstate[j + k] + copystates[i].Push.ToString()[1] + nextstate[w] + ")");
                            }
                            
                        }
                        j = w;
                    }
                    Console.WriteLine();
                    write.WriteLine();
                }
            }
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].Push == "_")
                {
                    Console.WriteLine("(" + states[i].Name + states[i].Pop + states[i].NextState + ")->" + states[i].Alpahbet);
                    write.WriteLine("(" + states[i].Name + states[i].Pop + states[i].NextState + ")->" + states[i].Alpahbet);
                }

            }
            write.Close();
        }

        static string[][] StremReader()
        {
            StreamReader input = new StreamReader("..\\..\\input.txt");

            string lines = input.ReadToEnd();         //read input
            string[] line = lines.Split('\n');        //remove '\n'
            string[][] listline = new string[line.Length][];  
            string stringline = "";
            for (int i = 0; i < line.Length; i++)
            {
                stringline = line[i];
                listline[i] = stringline.ToString().Split(',');           //example: listline[4][0] = "->q0" //listline[4][2] = "$"
            }
            input.Close();

            return listline;
        }
    }
}
