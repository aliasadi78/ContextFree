using System.Collections.Generic;
using System.IO;
using System;
using System.Data.Odbc;
using System.Security.Cryptography.X509Certificates;

namespace ContextFree
{
    
    public class Print
    {
        public static void print1()
        {
            List<Tuple<List<string>, string, List<string>, List<States>, List<string>, States[], string>> qwe = new List<Tuple<List<string>, string, List<string>, List<States>, List<string>, States[], string>>();
            qwe = States.Convert();
            printt1(qwe[0].Item1, qwe[0].Item2, qwe[0].Item3, qwe[0].Item4, qwe[0].Item5, qwe[0].Item6);
        }
        /// <summary>
        /// Convert NPDA to CFG
        /// Print & Write in File => output.txt
        /// </summary>
        public static void printt1(List<string> nextstate, string start, List<string> pop, List<States> copystates, List<string> stat, States[] states)
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
                    if (i + 1 >= states.Length)
                    {
                        Console.Write("(" + states[i].Name + states[i].Pop + states[i].NextState + ")->" + states[i].Alpahbet);
                        write.Write("(" + states[i].Name + states[i].Pop + states[i].NextState + ")->" + states[i].Alpahbet);
                    }
                    else
                    {
                        Console.WriteLine("(" + states[i].Name + states[i].Pop + states[i].NextState + ")->" + states[i].Alpahbet);
                        write.WriteLine("(" + states[i].Name + states[i].Pop + states[i].NextState + ")->" + states[i].Alpahbet);
                    } 
                }
            }
            write.Close();
        }
        /// <summary>
        /// Check & Print Part2
        /// </summary>
        public static void print2()
        {
            List<Tuple<List<string>, string, List<string>, List<States>, List<string>, States[], string>> qwe = new List<Tuple<List<string>, string, List<string>, List<States>, List<string>, States[], string>>();
            qwe = States.Convert();
            string start = qwe[0].Item2;
            string final = qwe[0].Item7;
            string StartVariable = "(" + start + "$" + final + ")";
            List<CFG> C = new List<CFG>();
            C = CFG.convert();
            Console.Write("Input:");
            string input = Console.ReadLine();
            string outp = StartVariable;
            string inp = "";
            int E = 0;
            string alpha = "";
            bool T = false;
            for (int k = 0; k < input.Length; k++)
            {
                for (int i = 0; i < C.Count; i++)
                {
                    if (StartVariable == C[i].Name && input[k].ToString() == C[i].Alphabet.ToString())
                    {
                        for (int j = 0; j < C[i].Pro.Length; j++)
                        {
                            if (C[i].Pro[j][1] == StartVariable)
                            {
                                alpha += input[k];
                                if (alpha.Length%2 != 0)
                                {
                                    outp += "=>" + alpha + C[i].Pro[j][0] + C[i].Pro[j][1];
                                }
                                else
                                {
                                    outp += "=>" + alpha + C[i].Pro[j][1];
                                    if (k + 1 >= input.Length)
                                    {
                                        outp += "=>" + alpha;
                                        T = true;
                                    }
                                }
                                E++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Output:");
            if (T)
            {
                Console.WriteLine("True");
                Console.WriteLine(outp);
            }
            else
            {
                Console.WriteLine("False");
            }
        }
    }
}