using System.Drawing;

namespace Day17a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines("input.txt");
			const int ROCKLIMIT = 2022;
			const int WIDTH = 7;
			const int SPAWNX = 2;
			const int SPAWNY = 3;
			Dictionary<Point, bool> map = new Dictionary<Point, bool>();
			List<Point[]> rockShapes = new List<Point[]>()
			{
				new Point[] {new Point(0,0), new Point(1, 0), new Point(2, 0), new Point(3, 0), },
				new Point[] {new Point(1,0), new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(1, 2), },
				new Point[] {new Point(0,0), new Point(1, 0), new Point(2, 0), new Point(2, 1), new Point(2, 2), },
				new Point[] {new Point(0,0), new Point(0, 1), new Point(0, 2), new Point(0, 3),},
				new Point[] {new Point(0,0), new Point(1, 0), new Point(0, 1), new Point(1, 1),},
			};
			int jetCount = 0;
			Point rockPos = new Point(SPAWNX, SPAWNY);
			int rockCount = 0;
			int towerTop = 0;

			while (rockCount < ROCKLIMIT)
			{
				Point[] curRock = rockShapes[rockCount % rockShapes.Count];
				// left/right
				int xMove = 0;
				switch (input[0][jetCount % input[0].Length])
				{
					case '<':
						xMove = -1;
						break;
					case '>':
						xMove = 1;
						break;
				}
				bool canMoveLR = true;
				for (int i = 0; i < curRock.Length; i++)
				{
					int targetX = rockPos.X + curRock[i].X + xMove;
					int targetY = rockPos.Y + curRock[i].Y;
					if (targetX < 0 || targetX >= WIDTH || map.ContainsKey(new Point(targetX, targetY)))
					{
						canMoveLR = false;
						break;
					}
				}
				if (canMoveLR)
				{
					rockPos.X += xMove;
				}
				jetCount++;
				// down
				bool canMoveD = true;
				for (int i = 0; i < curRock.Length; i++)
				{
					int targetX = rockPos.X + curRock[i].X;
					int targetY = rockPos.Y + curRock[i].Y - 1;
					if (targetY < 0 || map.ContainsKey(new Point(targetX, targetY)))
					{
						canMoveD = false;
						break;
					}
				}
				if (!canMoveD)
				{
					for (int i = 0; i < curRock.Length; i++)
					{
						map.Add(new Point(rockPos.X + curRock[i].X, rockPos.Y + curRock[i].Y), true);
						towerTop = Math.Max(towerTop, rockPos.Y + curRock[i].Y + 1);
					}
					rockCount++;
					rockPos = new Point(SPAWNX, towerTop + SPAWNY);
				}
				else
				{
					rockPos.Y--;
				}
			}
			Console.WriteLine($"{rockCount} rocks have fallen and the tower is {towerTop} units tall");
		}
	}
}