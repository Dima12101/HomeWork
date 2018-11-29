using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ControlWork
{
	class Program
	{
		public static object locker = new object();

		static void PrintList(List<string> list)
		{
			foreach (string str in list)
			{
				Console.WriteLine(str);
			}
		}

		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			Hasher hasher = new Hasher();
			Double time;
			var resultHash = new List<string>();

			//Usen't threads
			Console.WriteLine("Usen't threads. Begin");
			sw.Start();
			resultHash = hasher.GetHash(@"H:\Proga\EvolutionGame", false);
			sw.Stop();
			time = (sw.Elapsed.Milliseconds + 0.0) / 1000;
			Console.WriteLine("Time: " + time.ToString() + " seconds\n");
			Console.ReadKey();
			Console.WriteLine("ResultHash: ");
			PrintList(resultHash);
			Console.WriteLine("End.\n");
			//___________
			Console.ReadKey();

			//Use threads
			Console.WriteLine("Use threads. Begin");
			sw.Start();
			resultHash = hasher.GetHash(@"H:\Proga\EvolutionGame", true);
			sw.Stop();
			time = (sw.Elapsed.Milliseconds + 0.0) / 1000;
			Console.WriteLine("Time: " + time.ToString() + " seconds");
			Console.ReadKey();
			Console.WriteLine("ResultHash: ");
			PrintList(resultHash);
			Console.WriteLine("End.\n");
			//___________
			Console.ReadKey();
		}
	}
}
