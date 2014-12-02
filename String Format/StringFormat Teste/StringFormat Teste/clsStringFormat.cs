using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormat_Teste
{
    class clsStringFormat
    {
        #region Methods
        /// <summary>
        /// Returns the input string with the "Aaaaa" format.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string CaseFormat(string input)  
        {
            string result = "";
            try
            {
                result = input.Substring(0, 1).ToUpper() + input.Substring(1, input.Length - 1).ToLower();
            }
            catch { result = input; }
            return result;
        }

        /// <summary>
        /// Returns the input string  without extra whitespaces.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string RemoveExtraSpaces(string texto)
        {
            while (texto.Contains("  "))
            {
                texto = texto.Replace("  ", " ");
            }
            return texto;
        }

        /// <summary>
        /// Returns the input string without special characters.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="UseSpace"></param>
        /// <returns></returns>
        public string RemoveSpecialCharacters(string input)
        {
            string value = "";
            foreach (char ch in input)
            {
                if (char.IsLetter(ch) || char.IsNumber(ch) || char.IsWhiteSpace(ch))
                    value += ch;
            }
            return value;
        }
        public string RemoveSpecialCharacters(string input, bool allowSpace)
        {
            string value = "";
            foreach (char ch in input)
            {
                if (char.IsLetter(ch) || char.IsNumber(ch))
                    value += ch;
                else if (allowSpace && char.IsWhiteSpace(ch))
                    value += ch;
            }
            return value;
        }

        /// <summary>
        /// Returns the inverse of the input string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Revert(string input)
        {
            string value = "";
            foreach (char ch in input)
            {
                value = ch + value;
            }
            return value;

        }
        #endregion
    }
}
