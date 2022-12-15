using System.Drawing;

namespace Day15a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines("input.txt");
			HashSet<Point> beacons = new HashSet<Point>();
			HashSet<(Point, int)> sensors = new HashSet<(Point, int)>();
			foreach (string line in input)
			{
				string[] split = line.Split(' ');
				int sX = int.Parse(split[2][2..^1]);
				int sY = int.Parse(split[3][2..^1]);
				int bX = int.Parse(split[8][2..^1]);
				int bY = int.Parse(split[9][2..^0]);
				int dist = Math.Abs(sX - bX) + Math.Abs(sY - bY);
				sensors.Add((new Point(sX,sY), dist));
				beacons.Add(new Point(bX, bY));
			}
			const int IMPORTANTROW = 2000000;
			HashSet<int> noBeacon = new HashSet<int>();

			foreach (var item in sensors)
			{
				int distToRow = Math.Abs(item.Item1.Y - IMPORTANTROW);
				int leftOverDist = item.Item2 - distToRow;
				for (int i = item.Item1.X - leftOverDist; i <= item.Item1.X + leftOverDist; i++)
				{
					Point pos = new Point(i, IMPORTANTROW);
					if (!beacons.Contains(pos))
					{
						noBeacon.Add(i);
					}
					else
					{

					}
				}
			}
			Console.WriteLine($"{noBeacon.Count} positions with no beacon in row {IMPORTANTROW}");
			Console.WriteLine($"~Part 2~");

			const long TUNINGXMULTI = 4000000;
			const int SEARCHMAX = 4000000;

			for (int row = 0; row <= SEARCHMAX; row++)
			{
				List<(int, int)> rangeThing = new List<(int, int)>()
				{
					(0, SEARCHMAX)
				};
				// split on beacons. Turns out it's unnecessary because all the beacons are covered by a sensor range.
				//foreach (Point beacon in beacons)
				//{
				//	if (beacon.Y == row)
				//	{
				//		for (int i = 0; i < rangeThing.Count; i++)
				//		{
				//			if (rangeThing[i].Item1 == beacon.X && rangeThing[i].Item2 == beacon.X)
				//			{
				//				rangeThing.RemoveAt(i);
				//				i--;
				//			}
				//			else if (rangeThing[i].Item1 == beacon.X)
				//			{
				//				rangeThing[i] = (rangeThing[i].Item1 + 1, rangeThing[i].Item2);
				//			}
				//			else if (rangeThing[i].Item2 == beacon.X)
				//			{
				//				rangeThing[i] = (rangeThing[i].Item1, rangeThing[i].Item2 - 1);
				//			}
				//			else if (rangeThing[i].Item1 < beacon.X && rangeThing[i].Item2 > beacon.X)
				//			{
				//				rangeThing.Add((rangeThing[i].Item1, beacon.X - 1));
				//				rangeThing.Add((beacon.X + 1, rangeThing[i].Item2));
				//				rangeThing.RemoveAt(i);
				//				i--;
				//			}
				//		}
				//	}
				//}
				// subtract sensor ranges
				foreach (var item in sensors)
				{
					int distToRow = Math.Abs(item.Item1.Y - row);
					int leftOverDist = item.Item2 - distToRow;
					if (leftOverDist >= 0)
					{
						int left = Math.Max(0, item.Item1.X - leftOverDist);
						int right = Math.Min(SEARCHMAX, item.Item1.X + leftOverDist);
						for (int i = 0; i < rangeThing.Count; i++)
						{
							if (left <= rangeThing[i].Item1 && right >= rangeThing[i].Item2)
							{
								rangeThing.RemoveAt(i);
								i--;
							}
							else if (rangeThing[i].Item1 < left && rangeThing[i].Item2 > right)
							{
								rangeThing.Add((rangeThing[i].Item1, left - 1));
								rangeThing.Add((right + 1, rangeThing[i].Item2));
								rangeThing.RemoveAt(i);
								i--;
							}
							else if (right >= rangeThing[i].Item1 && right < rangeThing[i].Item2)
							{
								rangeThing[i] = (right + 1, rangeThing[i].Item2);
							}
							else if (left <= rangeThing[i].Item2 && left > rangeThing[i].Item1)
							{
								rangeThing[i] = (rangeThing[i].Item1, left - 1);
							}
						}
					}

				}
				if (rangeThing.Count == 1 && rangeThing[0].Item1 == rangeThing[0].Item2)
				{
					Point distressPos = new Point(rangeThing[0].Item1, row);
					Console.WriteLine($"Distress beacon found at: {distressPos}");
					Console.WriteLine($"Tuning frequency: {distressPos.X * TUNINGXMULTI + distressPos.Y}");
				}
				else if (rangeThing.Count > 0)
				{
					Console.WriteLine($"something's wrong");
				}
			}
			Console.WriteLine($"butt");
		}
	}
}