using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogicLibrary
{
    public static class Validator
    {
        public static string ValidateEmpty(string one)
        {
            if (!String.IsNullOrEmpty(one))
            {
                return CheckSpaces(one);
            }
            else throw new ArgumentNullException("Empty input!");
        }

        public static string CheckSpaces(string input, char chosenChar = '.')
        {
            if (!String.IsNullOrEmpty(input))
            {
                string output = "";
                string[] parts = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                if (parts.Length > 1)
                {
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if(parts[i] != "")
                            output += parts[i] + " ";
                    }
                }
                else 
                    output = input; 

                return CheckFinal(output, chosenChar) ;
            }
            else  
                return input; 
        }


        public static string CheckFinal(string one, char chosenChar)
        {
            char[] chars = checkSlashR(one);
            string outcome = "";

            for (int i = 1; i < chars.Length; i++)
            {
                if (chars[i - 1] == ' ' && chars[i] == ' ')
                {
                    continue;
                }
                else
                {
                    outcome += chars[i - 1].ToString();
                }
            }

            chars = (outcome + chars[chars.Length - 1]).ToCharArray();

            if (chars.Length > 1)
                one = otherChecks(chars, one, chosenChar);

            return one;
        }

        private static string otherChecks(char[] chars, string one, char chosenChar)
        {
            if (chars[chars.Length - 2] == '.' && chars[chars.Length - 1] == ' ')
            {
                string output = "";
                for (int i = 0; i < chars.Length - 1; i++)
                {
                    output += chars[i].ToString();
                }
                return output;
            }
            else if (chars[chars.Length - 2] == ' ' && chars[chars.Length - 1] == '.')
            {
                string output = "";
                for (int i = 0; i < chars.Length - 2; i++)
                {
                    output += chars[i].ToString();
                }
                return output + chosenChar;
            }
            else if (chars[chars.Length - 1] == ' ' || chars[chars.Length - 1] == '.')
            {
                string output = "";
                for (int i = 0; i < chars.Length - 1; i++)
                {
                    output += chars[i].ToString();
                }
                return output + chosenChar;
            }
            else 
                return one + chosenChar; 
        }

        private static char[] checkSlashR(string input)
        {
            char[] chars = input.ToCharArray();

            string output = "";

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] != '\r')
                    output += chars[i].ToString();
            }

            return output.ToCharArray();
        }
    }
}
