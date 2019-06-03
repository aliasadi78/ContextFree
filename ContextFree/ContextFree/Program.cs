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
            string[][] States = StremReader();
//            States state = new States();
            States[] states = new States[States.Length - 4];
            for (int i = 4; i < States.Length; i++)
            {
                string Name = States[i][0];
                string Alpahbet = States[i][1];
                string Pop = States[i][2];
                string Push = States[i][3];
                string NextState = States[i][4];
                States state = new States(Name,Alpahbet,Pop,Push,NextState,false);
                states[i - 4] = state;
            }

            Console.WriteLine(states[1].Name);
            
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
                listline[i] = stringline.ToString().Split(',');           //example ===> listline[4][0] = "->q0" //listline[4][2] = "$"
            }
            input.Close();

            return listline;
        }
    }
}
