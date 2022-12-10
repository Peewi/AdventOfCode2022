namespace Day10a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			int x = 1;
			int cycle = 0;
			int signalSum = 0;
			foreach (string line in lines)
			{
				cycle++;
				if (Cycle(cycle, x))
				{
					signalSum += cycle * x;
				}
				string[] split = line.Split(' ');
				if (split[0] == "addx")
				{
					cycle++;
					if (Cycle(cycle, x))
					{
						signalSum += cycle * x;
					}
					x += int.Parse(split[1]);
				}
			}
			Console.WriteLine($"Signal sum: {signalSum}");
		}

		static bool Cycle(int cycle, int x)
		{
			if ((cycle + 20) % 40 == 0)
			{
				Console.WriteLine($"Cycle {cycle} strength: {cycle * x}");
				return true;
			}
			return false;
		}
	}
}