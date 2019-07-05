using System;
using System.Collections.Generic;

namespace ContextFree
{
    public class CFG
    {
        public CFG(string name, string alphabet, string[][] pro)
        {
            this.Name = name;
            this.Alphabet = alphabet;
            this.Pro = pro;
        }

        public string Name;
        public string Alphabet;
        public string[][] Pro;

        /// <summary>
        /// convert CFG to suitable data structure
        /// </summary>
        /// <returns>List<CFG></returns>
        public static List<CFG> convert()
        {
            List<CFG> CFG = new List<CFG>();              //example:  (q0$q0)->a(q00q0)(q0$q0)|a(q00q1)(q1$q0)
                                                          //CFG[0].Name      = "(q0$q0)"     CFG[0].Alphabet  = "a"   
                                                          //CFG[0].Pro[0][0] = "(q00q0)"     CFG[0].Pro[0][1] = "(q0$q0)"
                                                          //CFG[0].Pro[1][0] = "(q00q1)"     CFG[0].Pro[1][1] = "(q1$q0)"

                                                          //      alphabet  Pro[0][1]       Pro[1][1]
                                                          //         |          |               |
                                                          //(q0$q0)->a(q00q0)(q0$q0)|a(q00q1)(q1$q0)
                                                          //   |         |               |
                                                          //  Name   Pro[0][0]       Pro[1][0]
                                                          
            List<string[]> line = Stream.StreamReadre2();
            
            string[][]  name = new string[line[0].Length][];    
            for (int j = 0; j < line[0].Length; j++)
            {
                name[j] = line[0][j].Split('-');
                name[j][1] = name[j][1].ToString().Replace(">", "");
            }

            Console.WriteLine();
            for (int i = 0; i < line[0].Length; i++)
            {
                string Name = name[i][0];
                string Alphabet = name[i][1][0].ToString();
                string[] Pr = name[i][1].Split('|');

                List<string[]> p = new List<string[]>();
                for (int j = 0; j < Pr.Length; j++)
                {
                    Pr[j] = Pr[j].ToString().Replace(Pr[j][0].ToString(), "");
                }
                p.Add(Pr);

                for (int j = 0; j < Pr.Length; j++)
                {
                    p[0][j] = p[0][j].ToString().Replace(")(",")|(");
                }
                string[][] Prod = new string[line.Count][];
                for (int j = 0; j < Pr.Length; j++)
                {
                    string[] Pro = p[0][j].Split('|');
                    Prod[j] = Pro;
                }
                CFG Cf = new CFG(Name,Alphabet,Prod);
                CFG.Add(Cf);
            }
            return CFG;
        }
    }
}