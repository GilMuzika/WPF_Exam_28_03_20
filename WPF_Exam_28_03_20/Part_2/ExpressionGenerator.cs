using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WPF_Exam_28_03_20
{
    class ExpressionGenerator
    {
        
        private Random _rnd = new Random();

        public void Generate(out double firstExpresionMember, out double secondExpressionMember, out string expressionSymbol, out double correctResult, out double fakeResult1, out double fakeResult2, out double fakeResult3)
        {
            string[] symbolsArr = new string[] {"-", "+", "*", "/" };
            int randomNumber = _rnd.Next(0, symbolsArr.Length);
            
            string fairSymbol = symbolsArr[randomNumber]; //the symbol  of the arhitmetic operation

            double firstNumber = 0; //the first number of the arhitmetic operation
            double secondNumber = 0; //the second number of the arhitmetic operation

            double fairResult = DictionaryGenerateFairNumber(out firstNumber, out secondNumber)[fairSymbol](); //the correct result  of the arhitmetic operation

            List<double> fakeResults = new List<double>();
            for(int i = 0; i < 3; i++)
            {
                double fakeResult = _rnd.Next(Convert.ToInt32(fairResult) - 50, Convert.ToInt32(fairResult) + 50);
                fakeResults.Add(fakeResult);
            }

            firstExpresionMember = Convert.ToInt32(firstNumber);
            secondExpressionMember = Convert.ToInt32(secondNumber);
            expressionSymbol = fairSymbol;
            correctResult = Rounding(fairResult);
            fakeResult1 = Rounding(fakeResults[0]);
            fakeResult2 = Rounding(fakeResults[1]);
            fakeResult3 = Rounding(fakeResults[2]);
        }

        private Dictionary<string, Func<double>> DictionaryGenerateFairNumber(out double firstNum, out double secondNum)
        {
            Dictionary<string, Func<double>> generationDict = new Dictionary<string, Func<double>>();

            double randNum1 = _rnd.Next(1, 100);
            double randNum2 = _rnd.Next(1, 100);
            generationDict.Add("+", () => { return randNum1 + randNum2; });
            generationDict.Add("-", () => { return randNum1 - randNum2; });
            generationDict.Add("*", () => { return randNum1 * randNum2; });
            generationDict.Add("/", () => { return randNum1 / randNum2; });
            firstNum = randNum1;
            secondNum = randNum2;
            return generationDict;
        }

        private double Rounding(double num)
        {
            double numAfter = Convert.ToDouble(Convert.ToInt32(num));
            if (num - numAfter == 0) return numAfter;
            else return Convert.ToDouble(Math.Round(Convert.ToDecimal(num), 2));
                
        }

    }
}
