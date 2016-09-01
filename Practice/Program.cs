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
			WriteInput(TestClass);
		}

		static void WriteInput(ITestInterface Input)
		{
			Console.WriteLine(Input.Character);
		}
	}

	//Declaring interface
	public interface ITestInterface
	{
		char Character {get;}
	}

	//Noww the class which works with the interface
	public class TestClass : ITestInterface
	{
		private static char CharacterValue = 'z';
		public char Character
		{
			get
			{
				return CharacterValue;
			}
		}
	}
	
}
