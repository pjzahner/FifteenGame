using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FifteenGame
{
	
	class Program
	{
		static void Main(string[] args)
		{
			List<char> GameBoardString = AssembleBoard(10, 3);
			Console.WriteLine(GameBoardString.ToArray());
			BoxCharacters.TopLeftCorner = 'f';
		}
		//system.text.encoding
		//penis
		//byteswriter
		public static void DrawBoard()
		{
			
		}
		public enum LineType
		{
			TopLine,
			FillerLine,
			MiddleLine,
			BottomLine
		};
		public static List<char> AssembleBoard(int Dimension, int BoxSize)
		{
			//Initi
			int BoxWidth = (BoxSize * 2);
			char[] Top = MakeBoxStrings(Dimension, BoxWidth, LineType.TopLine);
			char[] Filler = MakeBoxStrings(Dimension, BoxWidth, LineType.FillerLine);
			char[] Middle = MakeBoxStrings(Dimension, BoxWidth, LineType.MiddleLine);
			char[] Bottom = MakeBoxStrings(Dimension, BoxWidth, LineType.BottomLine);

			List<char> GameBoardString = new List<char>();

			GameBoardString.AddRange(Top);
			for (int Box = 1; Box <= Dimension; Box++)
			{
				for (int j = 0; j < BoxSize; j++)
				{
					GameBoardString.AddRange(Filler);
				}
				if(Box == Dimension)
				{
					GameBoardString.AddRange(Bottom);
				}
				else
				{
					GameBoardString.AddRange(Middle);
				}
			}
			return GameBoardString;		
		}

		//Penis penis penis
		//Thoughts on solving problem of passing in character sets
			//Use one static class with multiple CharSet strucs
			//Classes with properties and interfaces
		public static char[] MakeBoxStrings(int NumBoxes, int BoxSize, CharSet LineCharacters)
		{
			//Character variables to assign to this specific line
			char LeftChar = LineCharacters.LeftChar;
			char SpanningChar;
			char MiddleChar;
			char RightChar;
			//Int values to define width of one box (in characters) and width of one line (in characters)
			int BoxWidth = BoxSize + 1;
			int LineLength = (BoxWidth * NumBoxes) + 1;

			
			//initialize char arrays based on board dimensions and box size
			//Initialize top line
			char[] Line = new char[LineLength + 2];

			//Insert the left-hand character
			Line[0] = LeftChar;

			//Outer loop tracks for each box
			//Inner loop tracks over individual characters in each box
			for (int BoxNumber = 0; BoxNumber < NumBoxes; BoxNumber++)
			{
				for (int j = 1; j <= BoxSize; j++)
				{
					Line[(BoxNumber * BoxWidth) + j] = SpanningChar;
				}
				Line[((BoxNumber + 1) * BoxWidth)] = MiddleChar;
				Line[(NumBoxes * (BoxSize + 1))] = RightChar;
			}
			Line[LineLength + 1] = '\n';
			Console.WriteLine(Line);
			return Line;
		}
	}

	//Class which contains the unicode values of every desired box-drawing character
	// '\u' is the escape sequence which specifies the numbers as a unicode character (in hexadecimal)
	public static class BoxCharacters
	{
		public const char TopLeftCorner = (char)9556;
		public const char TopRightCorner = (char)9559;
		public const char BottomLeftCorner = (char)9562;
		public const char BottomRightCorner = (char)9565;
		public const char TopTee = '\u2566';
		public const char BottomTee = '\u2569';
		public const char LeftTee = '\u2560';
		public const char RightTee = '\u2563';
		public const char Riser = (char)9553;
		public const char Spanner = (char)9552;
		public const char Cross = '\u256c';
	}
	//Character set section
	//Abstract class for all character sets to use
	public abstract class CharSet
	{
		public static readonly char LeftChar;
		public static readonly char SpanningChar;
		public static readonly char MiddleChar;
		public static readonly char RightChar;
	}
	//Derivatives of CharSet, used by methods which build strings for drawing boxes for fiteen game
	//Constrctor is private to prevent instantiation of these classes
	//All four variables should be marked 'readonly' to prevent modification
	public class TopLine : CharSet
	{
		public static readonly char
			LeftChar = BoxCharacters.TopRightCorner,
			SpanningChar = BoxCharacters.Spanner,
			MiddleChar = BoxCharacters.TopTee,
			RightChar = BoxCharacters.TopRightCorner;
		private TopLine() { }
	}
	public class FillerLine : CharSet
	{
		public static readonly char
			LeftChar = BoxCharacters.Riser,
			SpanningChar = ' ',
			MiddleChar = BoxCharacters.Riser,
			RightChar = BoxCharacters.Riser;
		private FillerLine() { }
	}
	public class MiddleLine : CharSet
	{
		public static readonly char
			LeftChar = BoxCharacters.LeftTee,
			SpanningChar = BoxCharacters.Spanner,
			MiddleChar = BoxCharacters.Cross,
			RightChar = BoxCharacters.RightTee;
		private MiddleLine() { }
	}
	public class BottomLine : CharSet
	{
		public static readonly char
			LeftChar = BoxCharacters.BottomLeftCorner,
			SpanningChar = BoxCharacters.Spanner,
			MiddleChar = BoxCharacters.BottomTee,
			RightChar = BoxCharacters.BottomRightCorner;
		private BottomLine() { }
	}
}
