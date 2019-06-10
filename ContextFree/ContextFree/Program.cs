using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ContextFree
{
    class Program
    {
        static void Main(string[] args)
        {
            string[][] States = StremReader();
//            States state = new States();
            States[] states = new States[States.Length - 4];
            List<string> final = new List<string>();
            string start = "";
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
            
//            Console.WriteLine(states[1].Name);
            for (int i = 0; i < states.Length; i++)
            {
                Console.Write(states[i].Name+"   ");
                Console.Write(states[i].Pop+"   ");
                Console.Write(states[i].Push+"   ");
                Console.Write(states[i].NextState + "   ");
                Console.WriteLine();
                
            }

            Console.WriteLine("-----------------------------");
            for (int i = 0; i < copystates.Count; i++)
            {
                Console.Write(copystates[i].Name+"   ");
                Console.Write(copystates[i].Pop+"   ");
                Console.Write(copystates[i].Push+"   ");
                Console.Write(copystates[i].NextState + "   ");
                Console.WriteLine();
                
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

            for (int i = 0; i < pop.Count; i++)
            {
                Console.Write(pop[i] + "   ");                
                                
            }

            Console.WriteLine();
            for (int i = 0; i < push.Count; i++)
            {
                Console.Write(push[i] + "   ");
            }

            Console.WriteLine();
            for (int i = 0; i < nextstate.Count; i++)
            {
                Console.Write(nextstate[i] + "   ");
                
            }

            Console.WriteLine();
            
            for (int i = 0; i < pop.Count; i++)
            {
                for (int j = 0; j < nextstate.Count; j++)
                {
                    Console.Write("(" + start + pop[i] + nextstate[j] + ")->" + copystates[i].Alpahbet +
                                      "(" + start + copystates[i].Push.ToString()[0] + nextstate[j] + ")(" +
                                      start + copystates[i].Push.ToString()[1] + nextstate[j] + ")");
                    Console.WriteLine("|" + copystates[i].Alpahbet + "(" + start + copystates[i].Push.ToString()[0] +
                                      nextstate[j + 1] + ")(" + nextstate[j + 1] + copystates[i].Push.ToString()[1] +
                                      nextstate[j] + ")");
                    j++;
                    break;
                }

                for (int j = 1; j < nextstate.Count; j++)
                {
                    Console.Write("(" + start + pop[i] + nextstate[j] + ")->" + copystates[i].Alpahbet + 
                                      "(" + start + copystates[i].Push.ToString()[0] + nextstate[j - 1] + ")(" + 
                                      start + copystates[i].Push.ToString()[1] + nextstate[j] + ")");
                    Console.WriteLine("|" + copystates[i].Alpahbet + "(" + start + copystates[i].Push.ToString()[0] +
                                      nextstate[j] + ")(" + nextstate[j] + copystates[i].Push.ToString()[1] +
                                      nextstate[j] + ")");
                    j++;
                    break;
                }
            }

            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].Push == "_")
                {
                    Console.WriteLine("(" + states[i].Name + states[i].Pop + states[i].NextState + ")->" + states[i].Alpahbet);
                }
                
            }
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
