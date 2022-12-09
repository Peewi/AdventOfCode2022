using System.Numerics;

namespace Day09b
{
	internal class Program
	{
		static void Main(string[] args)
		{
			const int ROPELENGTH = 10;
			string[] lines = File.ReadAllLines("input.txt");
			Vector2[] rope = new Vector2[ROPELENGTH];
			HashSet<Vector2> visited = new HashSet<Vector2>();
			visited.Add(new Vector2(0, 0));
			foreach (string line in lines)
			{
				string[] split = line.Split(' ');
				Vector2 dir;
				switch (split[0])
				{
					case "R":
						dir = new Vector2(1, 0);
						break;
					case "L":
						dir = new Vector2(-1, 0);
						break;
					case "U":
						dir = new Vector2(0, 1);
						break;
					case "D":
						dir = new Vector2(0, -1);
						break;
					default:
						throw new Exception();
				}
				int count = int.Parse(split[1]);
				for (int i = 0; i < count; i++)
				{
					rope[0] += dir;
					for (int j = 1; j < ROPELENGTH; j++)
					{
						float xDist = Math.Abs(rope[j-1].X - rope[j].X);
						float yDist = Math.Abs(rope[j-1].Y - rope[j].Y);
						if (xDist > 1 || yDist > 1)
						{
							Vector2 tailDir = new Vector2(MathF.Sign(rope[j - 1].X - rope[j].X), MathF.Sign(rope[j - 1].Y - rope[j].Y));
							rope[j] += tailDir;
						}
					}
					visited.Add(rope[ROPELENGTH - 1]);
				}

			}
			Console.WriteLine($"The tail visited {visited.Count} positions");
		}
	}
}