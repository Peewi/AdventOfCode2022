namespace AdventOfCode2022
{
	internal class Program
	{
		static void Main(string[] args)
		{
			IEnumerable<string> lines = File.ReadLines("input.txt");
			int highest = 0;
			int[] top3 = { 0, 0, 0 };
			int current = 0;
			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					highest = Math.Max(highest, current);
					for (int i = 0; i < top3.Length; i++)
					{
						if (current > top3[i])
						{
							for (int j = top3.Length - 1; j > i; j--)
							{
								top3[j] = top3[j - 1];
							}
							top3[i] = current;
							break;
						}
					}
					current = 0;
				}
				else
				{
					current += int.Parse(line);
				}
			}
			Console.WriteLine($"Most carried by a single elf: {highest}");
			Console.WriteLine($"Carried by top 3 elves: {top3[0]} + {top3[1]} + {top3[2]}");
			Console.WriteLine($"Total of top 3 elves: {top3[0] + top3[1] + top3[2]}");
		}
	}
}