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
            List<CFG> CFG = new List<CFG>();
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