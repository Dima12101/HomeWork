using System;

namespace StackCalculatorProgram
{
	public class Calculator
	{
		private IStack<int> stackDigits;
		private IStack<char> stackSymbols;

		public Calculator(IStack<int> stackDigits, IStack<char> stackSymbols)
		{
			this.stackDigits = stackDigits;
			this.stackSymbols = stackSymbols;
		}

		private void Calculate(char symbol)
		{
			if (stackDigits.GetSize() < 2)
			{
				throw new Exception("Error!");
			}
			int digit2 = stackDigits.Pop();
			int digit1 = stackDigits.Pop();
			switch (symbol)
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

		private void CheckPriority()
		{
			if (!stackSymbols.IsEmpty())
			{
				bool priorityIsValid = false;
				do
				{
					char symbol = stackSymbols.Pop();
					switch (symbol)
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
				if (!stackSymbols.IsEmpty())
				{
					symbol = stackSymbols.Pop();
				}
				else
				{
					throw new Exception("Error!");
				}
			}
		}

		public int Result(string arExp)
		{
			string dig = "";
			for (int i = 0; i < arExp.Length; i++)
			{
				if (arExp[i] >= '0' && arExp[i] <= '9')
				{
					dig += arExp[i];
				}
				else
				{
					if (dig != "")
					{
						stackDigits.Push(Int32.Parse(dig));
						dig = "";
					}
					switch (arExp[i])
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
			if (!stackSymbols.IsEmpty())
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
