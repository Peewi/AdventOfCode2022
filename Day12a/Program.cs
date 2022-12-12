namespace Day12a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines("input.txt");
			int[,] map = new int[input[0].Length, input.Length];
			int[,] dist = new int[input[0].Length, input.Length];
			int targetX = 0, targetY = 0;
			for (int i = 0; i < input.Length; i++)
			{
				for (int j = 0; j < input[i].Length; j++)
				{
					dist[j, i] = int.MaxValue;
					if (input[i][j] == 'S')
					{
						map[j, i] = 'a';
						dist[j, i] = 0;
					}
					else if (input[i][j] == 'E')
					{
						map[j, i] = 'z';
						targetX = j;
						targetY = i;
					}
					else
					{
						map[j, i] = input[i][j];
					}
				}
			}
			const int MAXCLIMB = 1;
			while (dist[targetX, targetY] == int.MaxValue)
			{
				for (int x = 0; x < map.GetLength(0); x++)
				{
					for (int y = 0; y < map.GetLength(1); y++)
					{
						if (dist[x,y] < int.MaxValue)
						{
							int thisHeight = map[x, y];
							int thisDist = dist[x, y];

							if (x - 1 >= 0 && map[x - 1, y] <= thisHeight + MAXCLIMB)
							{
								dist[x - 1, y] = Math.Min(dist[x - 1, y], thisDist + 1);
							}
							if (x + 1 < map.GetLength(0) && map[x + 1, y] <= thisHeight + MAXCLIMB)
							{
								dist[x + 1, y] = Math.Min(dist[x + 1, y], thisDist + 1);
							}
							if (y - 1 >= 0 && map[x, y - 1] <= thisHeight + MAXCLIMB)
							{
								dist[x, y - 1] = Math.Min(dist[x, y - 1], thisDist + 1);
							}
							if (y + 1 < map.GetLength(1) && map[x, y + 1] <= thisHeight + MAXCLIMB)
							{
								dist[x, y + 1] = Math.Min(dist[x, y + 1], thisDist + 1);
							}
						}
					}
				}
			}
			Console.WriteLine($"Distance to target: {dist[targetX, targetY]}");
		}
	}
}