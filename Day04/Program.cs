namespace Day04
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			int fullOverlaps = 0;
			int partialOverlaps = 0;
			foreach (string line in lines)
			{
				string[] ranges = line.Split(',','-');
				int[] numbers = new int[ranges.Length];
				for (int i = 0; i < ranges.Length; i++)
				{
					numbers[i] = int.Parse(ranges[i]);
				}
				Console.Write($"ranges {numbers[0]}-{numbers[1]} and {numbers[2]}-{numbers[3]}: ");
				
				if (numbers[0] <= numbers[3] && numbers[1] >= numbers[2])
				{
					Console.Write($"partial overlap");
					partialOverlaps++;
				}
				if ((numbers[0] <= numbers[2] && numbers[1] >= numbers[3])
					|| (numbers[2] <= numbers[0] && numbers[3] >= numbers[1]))
				{
					Console.Write($" and full overlap");
					fullOverlaps++;
				}
				Console.Write("\n");
			}
			Console.WriteLine($"Full overlaps: {fullOverlaps} (Part 1 solution)");
			Console.WriteLine($"Partial overlaps: {partialOverlaps} (Part 2 solution)");
		}
	}
}