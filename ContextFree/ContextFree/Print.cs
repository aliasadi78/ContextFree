using System.Collections.Generic;
using System.IO;
using System;

namespace ContextFree
{
    /// <summary>
    /// Convert NPDA to CFG
    /// Print & Write in File => output.txt
    /// </summary>
    public class Print
    {
        public static void print()
        {
            List<Tuple<List<string>, string, List<string>, List<States>, List<string>, States[]>> qwe = new List<Tuple<List<string>, string, List<string>, List<States>, List<string>, States[]>>();
            qwe = States.Convert();
            printt(qwe[0].Item1, qwe[0].Item2, qwe[0].Item3, qwe[0].Item4, qwe[0].Item5, qwe[0].Item6);
        }
        public static void printt(List<string> nextstate, string start, List<string> pop, List<States> copystates, List<string> stat, States[] states)
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
    }
}