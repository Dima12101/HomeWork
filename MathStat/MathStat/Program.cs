using System;
using System.Collections.Generic;
using System.IO;

namespace MathStat
{
	class Program
	{
		static void Main(string[] args)
		{
			var sample = new List<double>();
			using (var reader = new StreamReader("Data.txt", System.Text.Encoding.Default))
			{
				while(!reader.EndOfStream)
				{
					var sampleValue = Convert.ToDouble(reader.ReadLine().Replace('.', ','));
					sample.Add(sampleValue);
				}
			}
			var randomVariable = new RandomVariable(sample);
			randomVariable.Print();
			//randomVariable.ConfidenceInterval_print(0.95);
			//randomVariable.ConfidenceInterval_print(0.99);

			var accept1 = randomVariable.ChekHypothesis_NormalDistribution(0.05);
			if (accept1)
			{
				Console.WriteLine("Hypothesis is OK");
			}
			else
			{
				Console.WriteLine("Hypothesis is not OK");
			}
			var accept2 = randomVariable.ChekHypothesis_NormalDistribution(0.01);
			if (accept2)
			{
				Console.WriteLine("Hypothesis is OK");
			}
			else
			{
				Console.WriteLine("Hypothesis is not OK");
			}
		}
	}
}
