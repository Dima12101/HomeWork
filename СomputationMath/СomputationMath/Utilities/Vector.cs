using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace СomputationMath
{
	class Vector
	{
		public double[] data { get; set; }

		public Vector(int length)
		{
			data = new double[length];
		}

		public Vector(double[] _data)
		{
			data = new double[_data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = _data[i];
			}
		}

		public void Show()
		{
			for (int i = 0; i < data.Length; i++)
			{
				Console.Write(data[i].ToString("0.0000000000") + ' ');
			}
			Console.Write('\n');
		}
		public void Show1()
		{
			for (int i = 0; i < data.Length; i++)
			{
				Console.Write(data[i].ToString("0.0000000000") + '\n');
			}
			Console.Write('\n');
		}

		public void Copy(Vector vector)
		{
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = vector.data[i];
			}
		}

		public void Generate()
		{
			var rnd = new Random();
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = rnd.Next() % (data.Length * 25);
			}
		}

		public void SetValues(double[] values)
		{
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = values[i];
			}
		}

		public static Vector operator *(double alpha, Vector vector)
		{
			var resultVector = new Vector(vector.data.Length);
			for (int i = 0; i < resultVector.data.Length; i++)
			{
				resultVector.data[i] = vector.data[i] * alpha;
			}
			return resultVector;
		}

		public static Vector operator +(Vector vector1, Vector vector2)
		{
			if (vector1.data.Length != vector2.data.Length)
			{
				return null;
			}
			else
			{
				var resultVector = new Vector(vector1.data.Length);
				for (int i = 0; i < resultVector.data.Length; i++)
				{
					resultVector.data[i] = vector1.data[i] + vector2.data[i];
				}
				return resultVector;
			}
		}

		public static Vector operator -(Vector vector1, Vector vector2)
		{
			if (vector1.data.Length != vector2.data.Length)
			{
				return null;
			}
			else
			{
				var resultVector = new Vector(vector1.data.Length);
				for (int i = 0; i < resultVector.data.Length; i++)
				{
					resultVector.data[i] = vector1.data[i] - vector2.data[i];
				}
				return resultVector;
			}
		}

		public double Norm()
		{
			var maxElement = 0.0;
			for (int i = 0; i < data.Length; i++)
			{
				maxElement = Math.Max(maxElement, Math.Abs(data[i]));
			}
			return maxElement;
		}

		public void SetValueByFunks(ScalarFunk1_N[] vectorFunks, double[] X)
		{
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = vectorFunks[i](X);
			}
		}
	}
}