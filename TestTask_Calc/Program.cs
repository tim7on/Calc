using System;
using System.Text.RegularExpressions;

namespace TestTask_Calc
{
    public static class Calc
    {
        public static string Result(this string value)
        {
            /* Method to calculate and read what kind of operation is needed. */
            string pattern = "[-*+/]";
            value = value.Replace('.', ',');
            MatchCollection options = Regex.Matches(value, pattern);
            string[] nums = Regex.Split(value, pattern); // Get numbers
            double total = Convert.ToDouble(nums[0]);
            string action;
            for (int i = 1; i < nums.Length; i++)
            {
                try
                {
                    action = Convert.ToString(options[i - 1]);
                    if (action == "*")
                    {
                        Console.WriteLine("{0} * {1} = {2}", total, Convert.ToDouble(nums[i]), total * Convert.ToDouble(nums[i]));
                        total *= Convert.ToDouble(nums[i]);
                    }
                    else if (action == "/")
                    {
                        if (nums[i] != "0")
                        {
                            Console.WriteLine("{0} / {1} = {2}", total, Convert.ToDouble(nums[i]), total / Convert.ToDouble(nums[i]));
                            total /= Convert.ToDouble(nums[i]);
                        }
                        else
                        {
                            return "ERROR: Cannot divide by 0";
                        }
                    }

                    else if (action == "-")
                    {
                        Console.WriteLine("{0} - {1} = {2}", total, Convert.ToDouble(nums[i]), total - Convert.ToDouble(nums[i]));
                        total -= Convert.ToDouble(nums[i]);
                    }
                    else if (action == "+")
                    {
                        Console.WriteLine("{0} + {1} = {2}", total, Convert.ToDouble(nums[i]), total + Convert.ToDouble(nums[i]));
                        total += Convert.ToDouble(nums[i]); ;
                    }
                    else
                    {
                        
                        Console.WriteLine("ERROR in Calculation");
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Example error, check your example");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return "None";
                }
            }
           
            return total.ToString("0.00");

        }
        public static string Calculate(this string value)
        {
            string validTwoNum = @"^(\d+[.,]\d+|\d+)[-*+/](\d+[.,]\d+|\d+)";
            var matchTwoNum = Regex.Match(value, validTwoNum);
            string complicatedExample = @"(?=.*\()(?=.*\)).*";
            var matchComplicatedExample = Regex.Match(value, complicatedExample);
            string number = @"(\d+[.,]\d+|\d+)$";
            var matchNumber = Regex.Match(value, number);

            if (string.IsNullOrWhiteSpace(value)) return "ERROR Empty";
            if (matchComplicatedExample.Success) return value.Brakets();
            if (matchTwoNum.Success) return value.Result();
            if (matchNumber.Success) return value;

            return "Wrong Example";
        }
        public static string Brakets(this string value)
        {
           while (value.Contains('(') && value.Contains(')'))
            {
                int indexOp = 0;
                int indexCl = 0;
                for(int i = 0; i <value.Length; i++)
                {
                    if (value[i] == '(')
                    {
                        indexOp = i;
                    }
                    else if (value[i] == ')')
                    {
                        indexCl = i;

                        value = value.Remove(indexOp, indexCl - indexOp + 1).Insert(indexOp, BraketsSolution(indexOp, indexCl, value));
                        return value.Calculate();
                    }
                }
            }
            string result = "0";
            return result;
        }
        public static string BraketsSolution(int startpoint, int endpoint, string example)
        {
            string ans = Result(example.Substring(startpoint + 1, endpoint - startpoint - 1));
            return ans;
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
                Console.WriteLine("INPUT YOUR EXAMPLE:\n");
                //test
                string[] value = {"5+5-(6+5", "(5+6)*3", "5+5-8*2/0.5", "5+", "5+5-"}; //"(5+6)*3", "5+5-8*2/0.5", "5.5+5.3", "36/2+(9*6)/5", "85/6-(5*5)+(3*7)"
                for (int i = 0; i < value.Length; i++)
                {
      
                    Console.WriteLine(value[i]);
                    Console.WriteLine("Result is {0}\n",value[i].Calculate());

                }
                break;
                /*string value = Console.ReadLine();

                if (value.ToLower() == "exit")
                {
                    break;
                }

                value = value.Calculate();*/

                Console.WriteLine($"\nResult is {value} \n");
            }
           
        }
    }
}
