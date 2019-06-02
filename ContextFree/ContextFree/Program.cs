using System;
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
            StremReader();

        }
        static void StremReader()
        {
            StreamReader input = new StreamReader("..\\..\\input.txt");

            string lines = input.ReadToEnd();         //read input
            string[] line = lines.Split('\n');        //remove '\n'
            string[][] listline = new string[line.Length][];  
            string stringline = "";
            for (int i = 0; i < line.Length; i++)
            {
                stringline = line[i];
                listline[i] = stringline.ToString().Split(',');           //listline[4][0] = "->q0" //listline[4][2] = "$"
            }

            Console.WriteLine(listline[4][2]);

            input.Close();

        }
    }
}
