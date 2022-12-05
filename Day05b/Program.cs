namespace Day05b
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			int blankLine = 0;
			// find line between starting stacks and move instructions
			for (int i = 0; i < lines.Length; i++)
			{
				if (string.IsNullOrWhiteSpace(lines[i]))
				{
					blankLine = i;
					break;
				}
			}
			// prepare
			int stackcount = lines[blankLine - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Length;
			Stack<char>[] stacks = new Stack<char>[stackcount];
			for (int i = 0; i < stacks.Length; i++)
			{
				stacks[i] = new Stack<char>();
			}
			for (int i = blankLine - 2; i >= 0; i--)
			{
				for (int j = 0; j < stackcount; j++)
				{
					char crate = lines[i][j * 4 + 1];
					if (!char.IsWhiteSpace(crate))
					{
						stacks[j].Push(crate);
					}
				}
			}
			// instructions
			for (int i = blankLine + 1; i < lines.Length; i++)
			{
				string[] splitString = lines[i].Split(' ');
				int moveCount = int.Parse(splitString[1]);
				int src = int.Parse(splitString[3]) - 1;
				int tgt = int.Parse(splitString[5]) - 1;
				string moved = "";
				for (int j = 0; j < moveCount; j++)
				{
					moved += stacks[src].Pop();
				}
				for (int j = moveCount - 1; j >= 0; j--)
				{
					stacks[tgt].Push(moved[j]);
				}
			}
			// print result
			string stackTops = "";
			foreach (var item in stacks)
			{
				stackTops += item.Peek();
			}
			Console.WriteLine($"{stackcount} stacks");
			Console.WriteLine($"Stacktops: {stackTops}");
		}
	}
}