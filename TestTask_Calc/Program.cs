using System;
using System.Text.RegularExpressions;

namespace TestTask_Calc
{
    public static class ExtensionMethods
    {
        public static string Calc(this string value)
        {
            /* Method to calculate 2 digits and read what kind of operation is needed. */


            string pattern = "[-*+/]";
            value = value.Replace('.', ',');
            Match act = Regex.Match(value, pattern); //Get operation
            string action = String.Format("{0}", act); // Convert Match to String
            string[] nums = Regex.Split(value, pattern); // Get numbers
            double result;
            double num1 = Convert.ToDouble(nums[0]);
            double num2 = Convert.ToDouble(nums[1]);
           
            if (action == "+")
            {
                result = num1 + num2;
            }
            else if (action == "-")
            {
                result = num1 - num2;
            }
            else if (action == "*")
            {
                result = num1 * num2;
            }
            else if (action == "/")
            {
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                else
                {
                    return "ERROR: Cannot divide by 0" ;
                }
            }
            else
            { 
                Console.WriteLine("ERROR in Calculation");
                return "ERROR";
            }

            return Convert.ToString(result);
        }
        public static bool IsValid(this string value)
        {
            string valid = @"^(\d+[.,]\d+|\d+)[-*+/](\d+[.,]\d+|\d+)";
            Match match = Regex.Match(value, valid);

            if (string.IsNullOrWhiteSpace(value)) return false;
            if (!match.Success) return false;
      
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Calculator";
            Console.WriteLine("Enter your calculation. \nExample: '2 + 2.5', '5.8 / 6' \nWrite 'exit' to exit this app" +
                "\n+ is to addition \n- is to substract \n* is to multiply \n/ is to divide");
            while (true)
            {
                Console.WriteLine("INPUT YOUR EXAMPLE:");
                string value = Console.ReadLine();

                if (value.IsValid())
                {
                    value = value.Calc();
                }
                else if( value.ToLower() == "exit")
                {
                    break;
                }
                else
                {
                    value = "Error";
                }

                Console.WriteLine($"\nResult is {value} \n");
            }
           
        }
    }
}
