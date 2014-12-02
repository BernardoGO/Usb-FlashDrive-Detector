using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormat_Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            clsStringFormat a = new clsStringFormat();

            Console.WriteLine(a.CaseFormat("queixo"));
            Console.WriteLine(a.CaseFormat("123quexo"));
            Console.WriteLine(a.RemoveExtraSpaces("1      3   2 9 a  s s d  s"));
            Console.WriteLine(a.RemoveSpecialCharacters("a s ( ) *&^^HDJ2 / *"));
            Console.WriteLine(a.RemoveSpecialCharacters("a s ( ) *&^^HDJ2 / *", false));
            Console.WriteLine(a.Revert("quexo"));
            
            Console.ReadKey();
        }
    }
}
