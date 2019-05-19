using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace СomputationMath
{
	public struct Pair<Tfirst, Tsecond>
	{
		public Tfirst FirstElement { get; set; }

		public Tsecond SecondElement { get; set; }
	}

	public delegate double ScalarFunk1_N(double[] X);

	public delegate double ScalarFunk1(double X);

	public delegate double ScalarFunk2(double X, double Y);

	public delegate double[] VectorFunk(double X, double[] Y);
}