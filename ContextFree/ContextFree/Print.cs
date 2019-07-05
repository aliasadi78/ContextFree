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
            List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string[]>> qwe = new List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string[]>>();
            qwe = NPDA.Convert();
            printt1(qwe[0].Item1, qwe[0].Item2, qwe[0].Item3, qwe[0].Item4, qwe[0].Item5, qwe[0].Item6);
            //example:
            //(q0$q0)->a(q00q0)(q0$q0) | a(q00q1)(q1$q0)
            //(q0$q1)->a(q00q0)(q0$q1) | a(q00q1)(q1$q1)
            //(q00q0)->a(q00q0)(q00q0) | a(q00q1)(q10q0)
            //(q00q1)->a(q00q0)(q00q1) | a(q00q1)(q10q1)
            //(q0$q0)->b(q01q0)(q0$q0) | b(q01q1)(q1$q0)
            //(q0$q1)->b(q01q0)(q0$q1) | b(q01q1)(q1$q1)
            //(q01q0)->b(q01q0)(q01q0) | b(q01q1)(q11q0)
            //(q01q1)->b(q01q0)(q01q1) | b(q01q1)(q11q1)
            //(q01q0)->a
            //(q00q0)->b
            //(q0$q1)->_
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
                    //example:
                    //(q0$q0)->a(q00q0)(q0$q0) | a(q00q1)(q1$q0)
                    //(q0$q1)->a(q00q0)(q0$q1) | a(q00q1)(q1$q1)
                    //(q00q0)->a(q00q0)(q00q0) | a(q00q1)(q10q0)
                    //(q00q1)->a(q00q0)(q00q1) | a(q00q1)(q10q1)
                    //(q0$q0)->b(q01q0)(q0$q0) | b(q01q1)(q1$q0)
                    //(q0$q1)->b(q01q0)(q0$q1) | b(q01q1)(q1$q1)
                    //(q01q0)->b(q01q0)(q01q0) | b(q01q1)(q11q0)
                    //(q01q1)->b(q01q0)(q01q1) | b(q01q1)(q11q1)
                    
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
            //example:
            //(q01q0)->a
            //(q00q0)->b
            //(q0$q1)->_
            write.Close();
        }
        /// <summary>
        /// Check & Print Part2
        /// </summary>
        public static void print2()
        {
            TextWriter write = new StreamWriter("..\\..\\output2.txt");
            List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string[]>> qwe = new List<Tuple<List<string>, string, List<string>, List<NPDA>, List<string>, NPDA[], string[]>>();
            qwe = NPDA.Convert();
            string Start = qwe[0].Item2;
            string final = qwe[0].Item7[0];
            string symbol = qwe[0].Item7[1].Replace("\r", "");
            string StartVariable = "(" + Start + symbol + final + ")";
            List<CFG> cfg = new List<CFG>();
            cfg = CFG.convert();
            Console.Write("Input:");
            write.Write("Input:");
            string input = Console.ReadLine();
            write.WriteLine(input);
            string outp = StartVariable;     //(q0$q1)
            string inp = "";
            int E = 0;
            string alpha = "";
            bool T = false;
            List<CFG> single = new List<CFG>();
            for (int i = 0; i < cfg.Count; i++)
            {
                if (cfg[i].Pro[1] == null)
                {
                    single.Add(cfg[i]);
                }
            }

            int w = 0;
            for (int k = 0; k < input.Length; k++)
            {
                alpha += input[k];
                for (int i = 0; i < cfg.Count; i++)
                {
                    if (StartVariable == cfg[i].Name && input[k].ToString() == cfg[i].Alphabet.ToString())
                    {
                        for (int j = 0; j < cfg[i].Pro.Length; j++)
                        {
                            if (cfg[i].Pro[j][1] == StartVariable)
                            {
                                
                                if (E==0)
                                {
                                    outp += "=>" + alpha + cfg[i].Pro[j][0] + cfg[i].Pro[j][1];
                                    //example: (q0$q1)=>a(q00q0)(q0$q1)
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
                                    if (alpha.Length%2 != 0)
                                    {
                                        w = i;
                                        outp += "=>" + alpha + cfg[i].Pro[j][0] + cfg[i].Pro[j][1];
                                    }
                                    else
                                    {
                                        for (int l = 0; l < single.Count; l++)
                                        {
                                            if (cfg[w].Pro[j][0] == single[l].Name)
                                            {
                                                if(input[k].ToString() == single[l].Alphabet)
                                                {   
//                                                    w = i;
                                                    outp += "=>" + alpha + cfg[i].Pro[j][1];
                                                    if (k + 1 >= input.Length)
                                                    {
                                                        outp += "=>" + alpha;
                                                        T = true;
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                //example: 
                                //1->(q0$q1)=>a(q00q0)(q0$q1)
                                //2->(q0$q1)=>a(q00q0)(q0$q1)=>ab(q0$q1)
                                //3->(q0$q1)=>a(q00q0)(q0$q1)=>ab(q0$q1)=>abb(q01q0)(q0$q1)
                                //4->(q0$q1)=>a(q00q0)(q0$q1)=>ab(q0$q1)=>abb(q01q0)(q0$q1)=>abba(q0$q1)=>abba
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