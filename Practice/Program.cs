using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
	class Program
	{
		static void Main(string[] args)
		{
			WriteInput(ChildClass);
		}
		static void WriteInput(PracticeClass)
		{
			Console.WriteLine(InClass.TestChar);
		}
	}

	public abstract class PracticeClass
	{
		public readonly char TestChar;
	}
	public class ChildClass
	{
		public readonly char TestChar = 'z';
		private ChildClass() { }
	}
}
