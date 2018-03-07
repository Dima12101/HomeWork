using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculatorProgram
{
	public class Calculator
	{
		private IStack<char> stackSymbols = new Stack_list<char>();
		private IStack<int> stackDigits = new Stack_list<int>();

		private void Calculate(char symbol)
		{
			if (stackDigits.GetSize() < 2)
			{
				throw new Exception("Error!");
			}
			int digit2 = stackDigits.Pop();
			int digit1 = stackDigits.Pop();
			switch(symbol)
			{
				case '+':
					stackDigits.Push(digit1 + digit2);
					break;
				case '-':
					stackDigits.Push(digit1 - digit2);
					break;
				case '*':
					stackDigits.Push(digit1 * digit2);
					break;
				case '/':
					stackDigits.Push(digit1 / digit2);
					break;
				default:
					throw new Exception("Error. Not correct symbol!");
			}
		}

		private  void CheckPriority()
		{
			if (!stackSymbols.IsEmpty())
			{
				bool priorityIsValid = false;
				do
				{
					char symbol = stackSymbols.Pop();
					switch(symbol)
					{
						case '*':
							Calculate(symbol);
							break;
						case '/':
							Calculate(symbol);
							break;
						default:
							stackSymbols.Push(symbol);
							priorityIsValid = true;
							break;
					}
					if (stackSymbols.IsEmpty())
					{
						priorityIsValid = true;
					}
				} while (!priorityIsValid);
			}	
		}

		private void CountingInBrackets()
		{
			char symbol = stackSymbols.Pop();
			if (stackSymbols.IsEmpty())
			{
				throw new Exception("Error!");
			}
			while (symbol != '(')
			{
				Calculate(symbol);
				if(!stackSymbols.IsEmpty())
				{
					symbol = stackSymbols.Pop();
				}
				else
				{
					throw new Exception("Error!");
				}
			}
		}

		public int Result(string ArExp)
		{
			string dig = "";
			for (int i = 0; i < ArExp.Length; i++)
			{
				if(ArExp[i] >= '0' && ArExp[i] <= '9')
				{
					dig += ArExp[i];
				}
				else
				{
					if (dig != "")
					{
						stackDigits.Push(Int32.Parse(dig));
						dig = "";
					}
					switch (ArExp[i])
					{
						case '(':
							stackSymbols.Push('(');
							break;
						case '*':
							stackSymbols.Push('*');
							break;
						case '/':
							stackSymbols.Push('/');
							break;
						case '+':
							CheckPriority();
							stackSymbols.Push('+');
							break;
						case '-':
							CheckPriority();
							stackSymbols.Push('-');
							break;
						case ')':
							CountingInBrackets();
							break;
						case ' ':
							break;
						default:
							throw new Exception("Error. Note is not correct!");
					}
				}
			}
			if (dig != "")
			{
				stackDigits.Push(Int32.Parse(dig));
			}
			if(!stackSymbols.IsEmpty())
			{
				do
				{
					Calculate(stackSymbols.Pop());
				} while (!stackSymbols.IsEmpty());
			}
			if (stackDigits.GetSize() > 1)
			{
				throw new Exception("Error!");
			}
			return stackDigits.Pop();
		}
	}
}
