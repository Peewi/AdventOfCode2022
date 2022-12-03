namespace Day03b
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			int sum = 0;
			const int GROUPSIZE = 3;
			for (int i = 0; i < lines.Length / GROUPSIZE; i++)
			{
				Dictionary<char, bool[]> items = new Dictionary<char, bool[]>();
				for (int j = 0; j < GROUPSIZE; j++)
				{
					string line = lines[i * GROUPSIZE + j];
					for (int k = 0; k < line.Length; k++)
					{
						if (!items.ContainsKey(line[k]))
						{
							items.Add(line[k], new bool[GROUPSIZE]);
						}
						items[line[k]][j] = true;
					}
				}
				foreach (KeyValuePair<char, bool[]> item in items)
				{
					bool correctItem = true;
					for (int h = 0; h < GROUPSIZE; h++)
					{
						correctItem = correctItem && item.Value[h];
					}
					if (correctItem)
					{
						Console.WriteLine($"Group {i} item is {item.Key}");
						sum += Priority(item.Key);
						break;
					}
				}
			}
			Console.WriteLine($"Priority sum: {sum}");
		}

		static int Priority(char item)
		{
			bool isupper = char.IsUpper(item);
			int retval = (int)char.ToUpper(item);
			retval -= 64;
			if (isupper)
			{
				retval += 26;
			}
			return retval;
		}
	}
}