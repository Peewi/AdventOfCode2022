namespace Day08a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			int[,] trees = new int[lines.Length, lines[0].Length];
			// parse
			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					trees[j, i] = int.Parse(lines[i][j].ToString());
				}
			}
			// check visibility
			int width = trees.GetLength(0);
			int height = trees.GetLength(1);
			int visibleCount = width * 2 + height * 2 - 4;
			int peakScenic = 0;
			for (int x = 1; x < width - 1; x++)
			{
				for (int y = 1; y < height - 1; y++)
				{
					int thisTree = trees[x, y];
					bool visibleN = true;
					bool visibleS = true;
					bool visibleW = true;
					bool visibleE = true;
					int[] scenic = new int[4];
					for (int i = x + 1; i < width; i++)
					{
						visibleE = visibleE && trees[i, y] < thisTree;
						scenic[0]++;
						if (!visibleE)
						{
							break;
						}
					}
					for (int i = x - 1; i >= 0; i--)
					{
						visibleW = visibleW && trees[i, y] < thisTree;
						scenic[1]++;
						if (!visibleW)
						{
							break;
						}
					}
					for (int i = y + 1; i < height; i++)
					{
						visibleN = visibleN && trees[x, i] < thisTree;
						scenic[2]++;
						if (!visibleN)
						{
							break;
						}
					}
					for (int i = y - 1; i >= 0; i--)
					{
						visibleS = visibleS && trees[x, i] < thisTree;
						scenic[3]++;
						if (!visibleS)
						{
							break;
						}
					}
					peakScenic = Math.Max(peakScenic, scenic[0] * scenic[1] * scenic[2] * scenic[3]);
					if (visibleN || visibleS || visibleW || visibleE)
					{
						visibleCount++;
					}
				}
			}
			Console.WriteLine($"{visibleCount} visible trees");
			Console.WriteLine($"Highest scenic score: {peakScenic}");
		}
	}
}