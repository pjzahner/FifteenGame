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

		public static List<char> AssembleBoard(int Dimension, int BoxSize)
		{
			//Initialize the CharSet objects we'll use to create the char arrays
			CharSet TopSet = new CharSet(BoxCharacters.TopLeftCorner, BoxCharacters.Spanner, BoxCharacters.TopTee, BoxCharacters.TopRightCorner);
			CharSet FillerSet = new CharSet(BoxCharacters.Riser, ' ', BoxCharacters.Riser, BoxCharacters.Riser);
			CharSet MiddleSet = new CharSet(BoxCharacters.LeftTee, BoxCharacters.Spanner, BoxCharacters.Cross, BoxCharacters.RightTee);
			CharSet BottomSet = new CharSet(BoxCharacters.BottomLeftCorner, BoxCharacters.Spanner, BoxCharacters.BottomTee, BoxCharacters.BottomRightCorner);
			
			//Calling MakeBoxLine for each of the four char arrays we need
			char[] TopLine = MakeBoxLine(Dimension, BoxSize, TopSet);
			char[] FillerLine = MakeBoxLine(Dimension, BoxSize, FillerSet);
			char[] MiddleLine = MakeBoxLine(Dimension, BoxSize, MiddleSet);
			char[] BottomLine = MakeBoxLine(Dimension, BoxSize, BottomSet);
			
			//List which will append all the char arrays together and be returned from the function
			List<char> GameBoardString = new List<char>();

			//Put the top line in first of all
			GameBoardString.AddRange(TopLine);
			//Loop which appends the needed lines for as many boxes as are specified by the Dimension variable
			for (int Box = 1; Box <= Dimension; Box++)
			{
				//Loop appends as many filler lines as specified by the BoxSize variable
				for (int j = 0; j < BoxSize; j++)
				{
					GameBoardString.AddRange(FillerLine);
				}
				//If we are on the last box, add the bottom line to the end of it
				if(Box == Dimension)
				{
					GameBoardString.AddRange(BottomLine);
				}
				//Otherwise, this is not the last box, so add a middle line
				else
				{
					GameBoardString.AddRange(MiddleLine);
				}
			}
			return GameBoardString;		
		}

		//Thoughts on solving problem of passing in character sets
			//Use one static class with multiple CharSet strucs
			//Classes with properties and interfaces
		public static char[] MakeBoxLine(int NumBoxes, int BoxSize, CharSet LineChars)
		{
			//We start by multiplying BoxSize by two, since risers are twice as long as spanners
			BoxSize = (BoxSize * 2);

			//Int values which define the number of chars in a line and in one box
			int CharsInBox = BoxSize + 1;
			//We add one to make up for the very first character of the line, which is not considered to belong to any single box
			int CharsInLine = (CharsInBox * NumBoxes) + 1; 

			
			//initialize char arrays based on CharsInLine and space needed for escape characters
			char[] Line = new char[CharsInLine + 2];

			//Insert the left-hand character right away
			Line[0] = LineChars.LeftChar;

			//Outer loop tracks number of boxes based on NumBoxes
			for (int BoxNumber = 1; BoxNumber <= NumBoxes; BoxNumber++)
			{
				//Inner loop adds spanner chars based on BoxSize (multiplied by two to compensate for being half length of riser character)
				for (int SpannerNumber = 1; SpannerNumber <= BoxSize; SpannerNumber++)
				{
					Line[((BoxNumber - 1) * CharsInBox) + SpannerNumber] = LineChars.SpanningChar;
				}
				//Once all the spanner chars have been added, add a middle char
				Line[(BoxNumber * CharsInBox)] = LineChars.MiddleChar;
			}

			//Add the right-hand character to the end of the last box
			Line[(NumBoxes * (BoxSize + 1))] = LineChars.RightChar;
			//Add new-line escape character to end of line
			Line[CharsInLine + 1] = '\n';

			//ERROR-CHECKING
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
	//Creates a collection of private char values accessible by the corresponding properties
	//CharSets for different lines must be constructed fully
	public class CharSet
	{
		public char LeftChar { get {return LeftCharValue; } }
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
