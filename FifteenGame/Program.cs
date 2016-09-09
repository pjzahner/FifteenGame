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
			BoardString NewBox = GameBoardBuilder.BoardStringBuilder(5, 3);
			Console.WriteLine(NewBox.GameBoardList.ToArray());
			char[,] TileNums = new char[5, 5];
			for(int i = 0; i < 5; i++)
			{
				for(int j = 0; j < 5; j++)
				{
					TileNums[i, j] = 'X';
				}
			}

			NewBox.PrintNumLocs();
			Console.WriteLine(NewBox.OutputWriter(TileNums));
			Console.ReadLine();
		}

	}

	public static class GameBoardBuilder
	{
		//String creation method
		public static BoardString BoardStringBuilder(int NumBoxes, int BoxSize)
		{
			//Initialize the CharSet objects we'll use to create the char arrays
			CharSet TopSet = new CharSet(BoardCharacters.TopLeftCorner, BoardCharacters.Spanner, BoardCharacters.TopTee, BoardCharacters.TopRightCorner);
			CharSet FillerSet = new CharSet(BoardCharacters.Riser, ' ', BoardCharacters.Riser, BoardCharacters.Riser);
			CharSet MiddleSet = new CharSet(BoardCharacters.LeftTee, BoardCharacters.Spanner, BoardCharacters.Cross, BoardCharacters.RightTee);
			CharSet BottomSet = new CharSet(BoardCharacters.BottomLeftCorner, BoardCharacters.Spanner, BoardCharacters.BottomTee, BoardCharacters.BottomRightCorner);

			//Calling BoardLineBuilder for each of the four char arrays we need
			char[] TopLine = BoardLineBuilder(NumBoxes, BoxSize, TopSet);
			char[] FillerLine = BoardLineBuilder(NumBoxes, BoxSize, FillerSet);
			char[] MiddleLine = BoardLineBuilder(NumBoxes, BoxSize, MiddleSet);
			char[] BottomLine = BoardLineBuilder(NumBoxes, BoxSize, BottomSet);

			//List which will append all the char arrays together and be returned from the function
			List<char> GameBoardString = new List<char>();

			//Put the top line in first of all
			GameBoardString.AddRange(TopLine);
			//Loop which appends the needed lines for as many boxes as are specified by the Dimension variable
			for (int Box = 1; Box <= NumBoxes; Box++)
			{
				//Loop appends as many filler lines as specified by the BoxSize variable
				for (int j = 0; j < BoxSize; j++)
				{
					GameBoardString.AddRange(FillerLine);
				}
				//If we are on the last box, add the bottom line to the end of it
				if (Box == NumBoxes)
				{
					GameBoardString.AddRange(BottomLine);
				}
				//Otherwise, this is not the last box, so add a middle line
				else
				{
					GameBoardString.AddRange(MiddleLine);
				}

			}
			return new BoardString(NumLocBuilder(NumBoxes, BoxSize), GameBoardString, NumBoxes, BoxSize);
		}

		//Line creation method
		private static char[] BoardLineBuilder(int NumBoxes, int BoxSize, CharSet LineCharSet)
		{
			//We start by multiplying BoxSize by two, since risers are twice as long as spanners
			BoxSize = (BoxSize * 2);

			//Int values which define the number of chars in a line and in one box
			int CharsInBox = BoxSize + 1;
			//We add one to make up for the very first character of the line, which is not considered to belong to any single box
			int CharsInLine = (CharsInBox * NumBoxes) + 1;
			//The total number of chars in the final array is 2 greater than the number of chars represented in the box
				//One character is needed for '\n' One is needed for '\0'
			//The LineLength value we pass out in the final structure will be 1 greater, since the '\0' is eliminated when the arrays are appended into one List<char>
			int CharsInArray = CharsInLine + 2;  //Includes '\n' and '\0'
			//Initialize the BoxLine array that will be returned from this function
			char[] NewLine = new char[CharsInArray];

			//Insert the left-hand character right away
			NewLine[0] = LineCharSet.LeftChar;

			//Outer loop tracks number of boxes based on NumBoxes
			for (int BoxNumber = 1; BoxNumber <= NumBoxes; BoxNumber++)
			{
				//Inner loop adds spanner chars based on BoxSize (multiplied by two to compensate for being half length of riser character)
				for (int SpannerNumber = 1; SpannerNumber <= BoxSize; SpannerNumber++)
				{
					NewLine[((BoxNumber - 1) * CharsInBox) + SpannerNumber] = LineCharSet.SpanningChar;
				}
				//Once all the spanner chars have been added, add a middle char
				NewLine[(BoxNumber * CharsInBox)] = LineCharSet.MiddleChar;
			}

			//Add the right-hand character to the end of the last box
			NewLine[(NumBoxes * (BoxSize + 1))] = LineCharSet.RightChar;
			//Add new-line escape character to end of line
			NewLine[CharsInLine + 1] = '\n';

			//ERROR-CHECKING
			Console.WriteLine(NewLine); 

			return NewLine;
		}

		private static int[,] NumLocBuilder(int NumBoxes, int BoxSize)
		{
			//initialize 2D array for number locations
			//arranged as [Column, Row]
			int[,] NumLoc = new int[NumBoxes, NumBoxes];
			//number of chars in each line
			int CharsPerLine = (((BoxSize * 2) + 1) * NumBoxes + 3);

			//EdgeOffset essentially gets you to the space for the number in the top left corner of the board.
			//After edge offset, it's simply a matter of adding on the number of rows and columns times the right number of chars
			int EdgeOffset = (((BoxSize + 1) / 2) * CharsPerLine) + BoxSize;

			//CharsPerRow
			int CharsPerRow = (BoxSize + 1) * CharsPerLine;
			//CharsPerColumn
			int CharsPerColumn = (BoxSize * 2) + 1;

			for (int Row = 0; Row < NumBoxes; Row++)
			{

				for (int Column = 0; Column < NumBoxes; Column++)
				{
					NumLoc[Column, Row] = (Column * CharsPerColumn) + (Row * CharsPerRow) + EdgeOffset;
				}
			}
			return NumLoc;
		}

		private static class BoardCharacters
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

		private class CharSet
		{
			public char LeftChar { get { return LeftCharValue; } }
			public char SpanningChar { get { return SpanningCharValue; } }
			public char MiddleChar { get { return MiddleCharValue; } }
			public char RightChar { get { return RightCharValue; } }
			private char
				LeftCharValue,
				SpanningCharValue,
				MiddleCharValue,
				RightCharValue;

			public CharSet(char LeftIn, char SpanningIn, char MiddleIn, char RightIn)
			{
				LeftCharValue = LeftIn;
				SpanningCharValue = SpanningIn;
				MiddleCharValue = MiddleIn;
				RightCharValue = RightIn;
			}
		}
	}

	public class BoardString
	{
		//List object which contains the box characters for the  board, and the open spots for the numbers to be inserted
		public List<char> GameBoardList;

		//int array which holds the list locations where numbers can be inserted into the boxes
		public int[,] NumLocs;

		//Number of rows and columns
		int NumBoxes;

		//Dimensions of individual boxes
		int BoxSize;

		//Constructor requires int array and the character list
		public BoardString(int[,] InNumLocs, List<char> InGameBoardList, int InNumBoxes, int InBoxSize)
		{
			NumLocs = InNumLocs;
			GameBoardList = InGameBoardList;
			NumBoxes = InNumBoxes;
			BoxSize = InBoxSize;
		}

		//method which takes current board arrangement as input, and returns string with tile characters inserted
		public string OutputWriter(char[,] TileNums)
		{
			char[] OutputArray = (GameBoardList.ToArray());
			for (int Column = 0; Column < NumBoxes; Column++)
			{
				for (int Row = 0; Row < NumBoxes; Row++)
				{
					OutputArray[NumLocs[Column, Row]] = TileNums[Row, Column];
				}
			}
			return new string(OutputArray);
		}

		//Debugging Method
		public void PrintNumLocs()
		{
			for (int i = 0; i < NumBoxes; i++)
			{
				for (int j = 0; j < NumBoxes; j++)
				{
					Console.Write(NumLocs[j, i].ToString() + ' ');
				}
				Console.Write('\n');
			}
		}
	}

}
