using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculatorProgram
{
	class Calculator
	{
		private IStack<char> stackSymbols = new Stack_list<char>();
		private IStack<int> stackDigits = new Stack_list<int>();
		private string dig = "";
		private void Count(char symbol)
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
				{
					stackDigits.Push(digit1 + digit2);
					break;
				}
				case '-':
				{
					stackDigits.Push(digit1 - digit2);
					break;
				}
				case '*':
				{
					stackDigits.Push(digit1 * digit2);
					break;
				}
				case '/':
				{
					stackDigits.Push(digit1 / digit2);
					break;
				}
			}

		}
		private  void addPlusAndMinus()
		{
			if (!stackSymbols.IsEmpty())
			{
				char symbol = stackSymbols.Pop();
				while (symbol != '+' && symbol != '-' || !stackSymbols.IsEmpty())
				{
					if (symbol == '+' || symbol == '-' || symbol == '(')
					{
						stackSymbols.Push(symbol);
					}
					else
					{
						Count(symbol);
						if (!stackSymbols.IsEmpty())
						{
							symbol = stackSymbols.Pop();
						}
					}
				}
			}	
		}
		private void addBracket()
		{
			char symbol = stackSymbols.Pop();
			if (stackSymbols.IsEmpty())
			{
				throw new Exception("Error!");
			}
			while (symbol != '(')
			{
				Count(symbol);
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
					if (ArExp[i] >= '(' && ArExp[i] <= '/' && ArExp[i] != '.' || ArExp[i] == ' ')
					{
						char symbol = ArExp[i];
						switch(symbol)
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
								addPlusAndMinus();
								stackSymbols.Push('+');
								break;
							case '-':
								addPlusAndMinus();
								stackSymbols.Push('-');
								break;
							case ')':
								addBracket();
								break;
						}

					}
					else
					{
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
					Count(stackSymbols.Pop());
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
