namespace Day03a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			int sum = 0;
			foreach (string line in lines)
			{
				string first = line[0..(line.Length / 2)];
				string second = line[(line.Length / 2)..^0];
				bool breaktime = false;
				for (int i = 0; i < first.Length; i++)
				{
					for (int j = 0; j < second.Length; j++)
					{
						if (first[i] == second[j])
						{
							Console.WriteLine($"{first[i]} shared");
							sum += Priority(first[i]);
							breaktime = true;
							break;
						}
					}
					if (breaktime) { break; }
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