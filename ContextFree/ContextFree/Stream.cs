using System;
using System.Collections.Generic;
using System.IO;
namespace ContextFree
{
    /// <summary>
    /// Read File 
    /// </summary>
    public class Stream
    {
        public static string[][] StreamReader1()
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

        public static List<string[]> StreamReadre2()
        {
            string[][] s = StreamReader1();
            string[] numstate = new []{s[0][0].ToString()};
            StreamReader input = new StreamReader("..\\..\\output1.txt");
            string lines = input.ReadToEnd();
            List<string[]> line = new List<string[]>();
            line.Add(lines.Split('\n'));
            line.Add(numstate);
            return line;
        }
    }
}