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
		}
		//system.text.encoding
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

		public static char[] MakeBoxStrings(int NumBoxes, int BoxSize, LineType Type)
		{
			//Unicode decimal values for double-lined box characters to make code more readable
			const char TopLeftCorner = (char)9556;
			const char TopRightCorner = (char)9559;
			const char BottomLeftCorner = (char)9562;
			const char BottomRightCorner = (char)9565;
			const char TopTee = '\u2566';
			const char BottomTee = '\u2569';
			const char LeftTee = '\u2560';
			const char RightTee = '\u2563';
			const char Riser = (char)9553;
			const char Spanner = (char)9552;
			const char Cross = '\u256c';

			//Int values to define width of one box (in characters) and width of one line (in characters)
			int BoxWidth = BoxSize + 1;
			int LineLength = (BoxWidth * NumBoxes) + 1;

			//Character variables to assign to this specific line
			char LeftChar;
			char SpanningChar;
			char MiddleChar;
			char RightChar;

			//Switch to determine which characters to use based on which type of line we are making.
			switch(Type)
			{
				case LineType.TopLine:
					LeftChar = TopLeftCorner;
					SpanningChar = Spanner;
					MiddleChar = TopTee;
					RightChar = TopRightCorner;
					break;
				case LineType.MiddleLine:
					LeftChar = LeftTee;
					SpanningChar = Spanner;
					MiddleChar = Cross;
					RightChar = RightTee;
					break;
				case LineType.FillerLine:
					LeftChar = Riser;
					SpanningChar = ' ';
					MiddleChar = Riser;
					RightChar = Riser;
					break;
				case LineType.BottomLine:
					LeftChar = BottomLeftCorner;
					SpanningChar = Spanner;
					MiddleChar = BottomTee;
					RightChar = BottomRightCorner;
					break;
				default:
					LeftChar = 'x';
					SpanningChar = 'x';
					MiddleChar = 'x';
					RightChar = 'x';
					break;
			}
				
					
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
}
