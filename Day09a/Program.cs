using System.Numerics;

namespace Day09a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			Vector2 head = new Vector2(0, 0);
			Vector2 tail = new Vector2(0, 0);
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
					head += dir;
					float xDist = Math.Abs(head.X - tail.X);
					float yDist = Math.Abs(head.Y - tail.Y);
					if (xDist > 1 || yDist > 1)
					{
						Vector2 tailDir = new Vector2(MathF.Sign(head.X - tail.X), MathF.Sign(head.Y - tail.Y));
						tail += tailDir;
					}
					visited.Add(tail);
				}

			}
			Console.WriteLine($"The tail visited {visited.Count} positions");
		}
	}
}