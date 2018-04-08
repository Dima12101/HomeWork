using System;
using System.Collections.Generic;

namespace Test_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var queue = new Queue<int>();
			queue.Enqueue(1, 1);
			queue.Enqueue(5, 5);
			queue.Enqueue(4, 4);
			queue.Enqueue(7, 7);
		}
	}
}
