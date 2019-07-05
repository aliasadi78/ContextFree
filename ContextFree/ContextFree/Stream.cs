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
        /// <summary>
        /// Read file input.txt
        /// </summary>
        /// <returns>string[][]</returns>
        public static string[][] StreamReader1()
        {
            StreamReader input = new StreamReader("..\\..\\input.txt");

            string lines = input.ReadToEnd();         //read input
            string[] line = lines.Split('\n');        //remove '\n'
            string[][] listline = new string[line.Length][]; 
            for (int i = 0; i < line.Length; i++)
            {
                listline[i] = line[i].Replace("\r","").Split(',');     //example: listline[6][0] = "q0" listline[6][1] = "a" listline[6][2] = "1" listline[6][3] = "_" listline[6][4] = "q0"
            }
            input.Close();
            
            return listline;
        }
        /// <summary>
        /// Read file output1.txt
        /// </summary>
        /// <returns>List<string[]></string></returns>
        public static List<string[]> StreamReadre2()
        {
            string[] NumberStates = new []{StreamReader1()[0][0]}; //Numebr of Statest
            StreamReader input = new StreamReader("..\\..\\output1.txt");     //Read file
            List<string[]> line = new List<string[]>();
            line.Add(input.ReadToEnd().Replace("\r","").Split('\n'));
            line.Add(NumberStates);
            return line;                                                      // example: line[0][1] = "(q0$q1)->a(q00q0)(q0$q1)|a(q00q1)(q1$q1)" line[1][0] = 2
        }
    }
}