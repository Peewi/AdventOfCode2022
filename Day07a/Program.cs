namespace Day07a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			Directory root = new Directory();
			Directory active = root;
			List<Directory> dirs = new List<Directory>();
			int fileCount = 0;
			foreach (string line in lines)
			{
				string[] split = line.Split(' ');
				if (split[0] == "$" && split[1] == "cd" && split[2] != "/")
				{
					if (split[2] == "..")
					{
						active = active?.Parent;
					}
					else
					{
						active = new Directory(active);
						dirs.Add(active);
					}
				}
				else if (int.TryParse(split[0], out int filesize))
				{
					active?.IncreaseSize(filesize);
					fileCount++;
				}
			}
			Console.WriteLine($"A total of {fileCount} files");
			const int SMALLFOLDERTHRESHOLD = 100000;
			int smallFolderTotal = 0;
			foreach (Directory dir in dirs)
			{
				if (dir.Size <= SMALLFOLDERTHRESHOLD)
				{
					smallFolderTotal += dir.Size;
				}
			}
			Console.WriteLine($"Part 1 solution: {smallFolderTotal}");
			// part 2
			Console.WriteLine("~~~~~~~");
			const int TOTALSIZE = 70000000;
			const int NEEDED = 30000000;
			int unused = TOTALSIZE - root.Size;
			int missing = NEEDED - unused;
			Console.WriteLine($"Unused space: {unused}, missing for update {missing}");
			int smallestBigEnough = int.MaxValue;
			foreach (Directory dir in dirs)
			{
				if (dir.Size >= missing && dir.Size < smallestBigEnough)
				{
					smallestBigEnough = dir.Size;
				}
			}
			Console.WriteLine($"Part 2 solution: Smallest dir that is big enough: {smallestBigEnough}");
		}
	}
}