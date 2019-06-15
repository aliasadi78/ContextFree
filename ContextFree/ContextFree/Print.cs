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
            List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string>> qwe = new List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string>>();
            qwe = NPDA.Convert();
            printt1(qwe[0].Item1, qwe[0].Item2, qwe[0].Item3, qwe[0].Item4, qwe[0].Item5, qwe[0].Item6);
        }
        /// <summary>
        /// Convert NPDA to CFG
        /// Print & Write in File => output.txt
        /// </summary>
        public static void printt1(List<string> nextstate, string start, List<string> pop, List<NPDA> copystates, List<string> stat, NPDA[] npda)
        {
            TextWriter write = new StreamWriter("..\\..\\output1.txt");
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
            for (int i = 0; i < npda.Length; i++)
            {
                if (npda[i].Push == "_")
                {
                    if (i + 1 >= npda.Length)
                    {
                        Console.Write("(" + npda[i].Name + npda[i].Pop + npda[i].NextState + ")->" + npda[i].Alpahbet);
                        write.Write("(" + npda[i].Name + npda[i].Pop + npda[i].NextState + ")->" + npda[i].Alpahbet);
                    }
                    else
                    {
                        Console.WriteLine("(" + npda[i].Name + npda[i].Pop + npda[i].NextState + ")->" + npda[i].Alpahbet);
                        write.WriteLine("(" + npda[i].Name + npda[i].Pop + npda[i].NextState + ")->" + npda[i].Alpahbet);
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
            TextWriter write = new StreamWriter("..\\..\\output2.txt");
            List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string>> qwe = new List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string>>();
            qwe = NPDA.Convert();
            string Start = qwe[0].Item2;
            string final = qwe[0].Item7;
            string StartVariable = "(" + Start + "$" + final + ")";
            List<CFG> C = new List<CFG>();
            C = CFG.convert();
            Console.Write("Input:");
            write.Write("Input:");
            string input = Console.ReadLine();
            write.WriteLine(input);
            string outp = StartVariable;
            string inp = "";
            int E = 0;
            string alpha = "";
            bool T = false;
            List<CFG> single = new List<CFG>();
            for (int i = 0; i < C.Count; i++)
            {
                if (C[i].Pro[1] == null)
                {
                    single.Add(C[i]);
                }
            }

            int w = 0;
            for (int k = 0; k < input.Length; k++)
            {
                alpha += input[k];
                for (int i = 0; i < C.Count; i++)
                {
                    if (StartVariable == C[i].Name && input[k].ToString() == C[i].Alphabet.ToString())
                    {
                        for (int j = 0; j < C[i].Pro.Length; j++)
                        {
                            if (C[i].Pro[j][1] == StartVariable)
                            {
                                
                                if (E==0)
                                {
                                    
                                    outp += "=>" + alpha + C[i].Pro[j][0] + C[i].Pro[j][1];
                                    w = i;
                                    if (k + 1 >= input.Length)
                                    {
                                        outp += "=>" + alpha;
                                        T = true;
                                    }
                                    E++;
                                }
                                else
                                {
                                    if (k%2 == 0)
                                    {
//                                        w = i;
                                        outp += "=>" + alpha + C[i].Pro[j][0] + C[i].Pro[j][1];
                                        if (k + 1 >= input.Length)
                                        {
                                            outp += "=>" + alpha;
                                            T = true;
                                        }
                                    }
                                    else
                                    {
                                        for (int l = 0; l < single.Count; l++)
                                        {
                                            if (C[w].Pro[j][0] == single[l].Name && input[k].ToString() == single[l].Alphabet)
                                            {
                                                w = i;
                                                outp += "=>" + alpha + C[i].Pro[j][1];
                                                if (k + 1 >= input.Length)
                                                {
                                                    outp += "=>" + alpha;
                                                    T = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Output:");
            write.WriteLine("Output:");
            if (T)
            {
                Console.WriteLine("True");
                write.WriteLine("True");
                Console.WriteLine(outp);
                write.WriteLine(outp);
            }
            else
            {
                Console.WriteLine("False");
                write.WriteLine("False");
            }
            write.Close();
        }
    }
}