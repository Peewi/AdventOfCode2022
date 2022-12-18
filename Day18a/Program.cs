namespace Day18a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines("input.txt");
			HashSet<(int x, int y, int z)> lavadrops = new HashSet<(int x, int y, int z)>();
			int loX = int.MaxValue;
			int hiX = int.MinValue;
			int loY = int.MaxValue;
			int hiY = int.MinValue;
			int loZ = int.MaxValue;
			int hiZ = int.MinValue;
			foreach (string line in input)
			{
				string[] split = line.Split(',');
				(int x, int y, int z) dropCoord = (int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
				loX = Math.Min(loX, dropCoord.x);
				hiX = Math.Max(hiX, dropCoord.x);
				loY = Math.Min(loY, dropCoord.y);
				hiY = Math.Max(hiY, dropCoord.y);
				loZ = Math.Min(loZ, dropCoord.z);
				hiZ = Math.Max(hiZ, dropCoord.z);
				lavadrops.Add(dropCoord);
			}
			Console.WriteLine($"Lava droplet bounds: X: {loX}-{hiY}, Y:{loY}-{hiY}, Z:{loZ}-{hiZ}");
			int totalSurfaceArea = 0;
			foreach ((int x, int y, int z) in lavadrops)
			{
				(int x, int y, int z)[] directions = new (int x, int y, int z)[]
				{
					(x + 1, y, z),
					(x - 1, y, z),
					(x, y + 1, z),
					(x, y - 1, z),
					(x, y, z + 1),
					(x, y, z - 1),
				};
				foreach (var item in directions)
				{
					if (!lavadrops.Contains(item))
					{
						totalSurfaceArea++;
					}
				}
			}
			Console.WriteLine($"Lava droplet surface area: {totalSurfaceArea}");
			Console.WriteLine($"~~Part 2~~");
			int outerSurface = 0;
			foreach ((int x, int y, int z) in lavadrops)
			{
				(int x, int y, int z)[] directions = new (int x, int y, int z)[]
				{
					(x + 1, y, z),
					(x - 1, y, z),
					(x, y + 1, z),
					(x, y - 1, z),
					(x, y, z + 1),
					(x, y, z - 1),
				};
				foreach (var item in directions)
				{
					if (!lavadrops.Contains(item))
					{
						HashSet<(int x, int y, int z)> surfaceFill = new HashSet<(int x, int y, int z)>
						{
							item
						};
						HashSet<(int x, int y, int z)> shell = new HashSet<(int x, int y, int z)>();
						int oldSize = 0;
						while (surfaceFill.Count != oldSize)
						{
							bool breaktime = false;
							foreach ((int x, int y, int z) s in surfaceFill)
							{
								(int x, int y, int z)[] d = new (int x, int y, int z)[]
								{
								(s.x + 1, s.y, s.z),
								(s.x - 1, s.y, s.z),
								(s.x, s.y + 1, s.z),
								(s.x, s.y - 1, s.z),
								(s.x, s.y, s.z + 1),
								(s.x, s.y, s.z - 1),
								};
								foreach ((int x, int y, int z) surfacePotential in d)
								{
									if (!lavadrops.Contains(surfacePotential))
									{
										shell.Add(surfacePotential);
										if (s.x <= loX
											|| s.x >= hiX
											|| s.y <= loY
											|| s.y >= hiY
											|| s.z <= loZ
											|| s.z >= hiZ)
										{
											outerSurface++;
											breaktime = true;
											break;
										}
									}
								}
								if (breaktime)
								{
									break;
								}
							}
							if (breaktime)
							{
								break;
							}
							oldSize = surfaceFill.Count;
							foreach (var s in shell)
							{
								surfaceFill.Add(s);
							}
						}
					}
				}
			}
			Console.WriteLine($"Outer lava droplet surface area: {outerSurface}");
		}
	}
}