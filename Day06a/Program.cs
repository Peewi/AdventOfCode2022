namespace Day06a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// set to 4 for part 1, 14 for part 2
			const int MARKERSIZE = 14;
			string[] lines = File.ReadAllLines("input.txt");
			for (int i = MARKERSIZE; i < lines[0].Length; i++)
			{
				string marker = lines[0][(i - MARKERSIZE)..i];
				HashSet<char> chars = new HashSet<char>();
				foreach (char item in marker)
				{
					chars.Add(item);
				}
				if (chars.Count == MARKERSIZE)
				{
					Console.WriteLine($"Marker {marker} found at position {i}");
					break;
				}
			}
			Console.WriteLine("Hello, World!");
		}
	}
}