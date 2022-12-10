namespace Day10b
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			int x = 1;
			int cycle = 0;
			foreach (string line in lines)
			{
				cycle++;
				Cycle(cycle, x);
				string[] split = line.Split(' ');
				if (split[0] == "addx")
				{
					cycle++;
					Cycle(cycle, x);
					x += int.Parse(split[1]);
				}
			}
		}

		static void Cycle(int cycle, int x)
		{
			int pos = (cycle - 1) % 40;
			if (Math.Abs(pos - x) <= 1)
			{
				Console.Write("█");
			}
			else
			{
				Console.Write(" ");
			}
			if (cycle % 40 == 0)
			{
				Console.Write("\n");
			}
		}
	}
}