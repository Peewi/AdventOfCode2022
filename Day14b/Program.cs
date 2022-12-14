using System.Drawing;
using System.Xml.Linq;

namespace Day14b
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines("input.txt");
			Dictionary<Point, Space> map = new Dictionary<Point, Space>();
			Point sandStart = new Point(500, 0);
			int lowest = 0;

			for (int i = 0; i < input.Length; i++)
			{
				string[] coords = input[i].Split("->");
				Point[] points = new Point[coords.Length];
				for (int j = 0; j < coords.Length; j++)
				{
					string[] xy = coords[j].Split(",");
					points[j] = new Point(int.Parse(xy[0]), int.Parse(xy[1]));
					lowest = Math.Max(lowest, points[j].Y);
				}
				for (int j = 1; j < points.Length; j++)
				{
					Point pos = points[j - 1];
					int xDif = points[j].X - points[j - 1].X;
					int yDif = points[j].Y - points[j - 1].Y;
					int bigDif = Math.Max(Math.Abs(xDif), Math.Abs(yDif));
					for (int k = 0; k < bigDif; k++)
					{
						map[pos] = Space.Solid;
						pos.X += Math.Sign(xDif);
						pos.Y += Math.Sign(yDif);
					}
					map[pos] = Space.Solid;
				}
			}
			lowest += 2;
			DrawMap(map, sandStart);

			Point sandPos = sandStart;
			int sandResting = 0;
			while (true)
			{
				Point[] moves = new Point[]
				{
					new Point(sandPos.X, sandPos.Y+1),
					new Point(sandPos.X-1, sandPos.Y+1),
					new Point(sandPos.X+1, sandPos.Y+1),
				};
				bool moved = false;
				for (int i = 0; i < moves.Length; i++)
				{
					if (moves[i].Y < lowest && (!map.TryGetValue(moves[i], out Space sp) || sp == Space.Empty))
					{
						sandPos = moves[i];
						moved = true;
						break;
					}
				}
				if (!moved)
				{
					map[sandPos] = Space.Sand;
					sandResting++;
					if (sandPos == sandStart)
					{
						break;
					}
					sandPos = sandStart;
				}
				//DrawMap(map, sandPos);
				//Thread.Sleep(1);
			}
			DrawMap(map, sandPos);
			Console.WriteLine($"{sandResting} sand has come to a rest");
		}

		static void DrawMap(Dictionary<Point, Space> map, Point sand)
		{
			const int LEFT = 440;
			const int RIGHT = 560;
			const int TOP = 0;
			const int BOT = 162;
			Console.Clear();
			string pict = "";
			for (int y = TOP; y <= BOT; y++)
			{
				for (int x = LEFT; x <= RIGHT; x++)
				{
					if (sand.X == x && sand.Y == y)
					{
						pict += "O";
					}
					else if (map.TryGetValue(new Point(x, y), out Space sp))
					{
						if (sp == Space.Solid)
						{
							pict += "█";
						}
						else if (sp == Space.Sand)
						{
							pict += "O";
						}
						else
						{
							pict += " ";
						}
					}
					else
					{
						pict += " ";
					}
				}
				pict += "\n";
			}
			Console.Write(pict);
		}
	}

	enum Space
	{
		Empty,
		Solid,
		Sand
	}
}