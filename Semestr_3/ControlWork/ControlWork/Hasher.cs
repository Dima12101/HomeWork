using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Security.Cryptography;

namespace ControlWork
{
	class Hasher
	{
		public static object locker = new object();

		private static List<string> resultHash = new List<string>();

		private static void GetHashFile(object obj)
		{
			FileInfo file = (FileInfo)obj;
			//Console.WriteLine(file.Name);
			MD5 md5Hasher = MD5.Create();
			FileStream fileStream = file.OpenRead();
			var hashByte = md5Hasher.ComputeHash(fileStream);
			fileStream.Close();
			StringBuilder sBuilder = new StringBuilder();
			// Преобразуем каждый байт хэша в шестнадцатеричную строку
			for (int i = 0; i < hashByte.Length; i++)
			{
				//указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
				sBuilder.Append(hashByte[i].ToString("x2"));
			}
			lock (locker)
			{
				resultHash.Add(sBuilder.ToString());
			}
			//Console.WriteLine(sBuilder.ToString());
		}

		private void GetHashDir(DirectoryInfo dir, bool flag)
		{
			//Console.WriteLine("DirName: " + dir.Name);
			lock(locker)
			{
				resultHash.Add("DirName: " + dir.Name);
			}
			var arrFile = dir.GetFiles();
			foreach (FileInfo file in arrFile)
			{
				var resultHashFile = new List<string>();
				if (flag)
				{
					Thread myThread = new Thread(new ParameterizedThreadStart(GetHashFile));
					myThread.Start(file);
				}
				else
				{
					GetHashFile(file);
				}
			}
			var arrDir = dir.GetDirectories();
			foreach (DirectoryInfo _dir in arrDir)
			{
				GetHashDir(_dir, flag);
			}
		}

		public List<string> GetHash(String dirName, bool flag)
		{
			DirectoryInfo dirInfo = new DirectoryInfo(dirName);
			GetHashDir(dirInfo, flag);
			return resultHash;
		}
	}
}
