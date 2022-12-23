namespace Day20a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines("input.txt");
			List<int> numbers = new List<int>(input.Length);
			List<int> locations = new List<int>(input.Length);
			for (int i = 0; i < input.Length; i++)
			{
				string line = input[i];
				numbers.Add(int.Parse(line));
				locations.Add(i);
			}

			for (int i = 0; i < locations.Count; i++)
			{
				int index = locations[i];
				int moveNumber = numbers[index];
				int realNumber = numbers[index];
				while (Math.Abs(moveNumber) >= numbers.Count)
				{
					moveNumber = Math.Sign(moveNumber) * (Math.Abs(moveNumber) - (numbers.Count - 1));
				}
				int newIndex = index + moveNumber;
				newIndex = ((newIndex % numbers.Count) + numbers.Count) % numbers.Count;
				
				if (Math.Sign(moveNumber) != Math.Sign(newIndex - index))
				{
					newIndex -= Math.Sign(newIndex - index);
				}
				numbers.RemoveAt(index);
				numbers.Insert(newIndex, realNumber);
				// readjust locations
				for (int j = i + 1; j < locations.Count; j++)
				{

					if (newIndex - index > 0 && locations[j] <= newIndex && locations[j] > index)
					{
						locations[j]--;
					}
					if (newIndex - index < 0 && locations[j] >= newIndex && locations[j] < index)
					{
						locations[j]++;
					}

					while (locations[j] < 0)
					{
						locations[j] += locations.Count;
					}
					locations[j] %= locations.Count;
				}
			}
			Console.WriteLine("List mixed");
			int zeroPos = numbers.IndexOf(0);
			int[] important = new int[]
			{
				(zeroPos + 1000) % numbers.Count,
				(zeroPos + 2000) % numbers.Count,
				(zeroPos + 3000) % numbers.Count,
			};
			int importantSum = 0;
			Console.WriteLine("Important numbers:");
			foreach (int item in important)
			{
				Console.WriteLine(numbers[item]);
				importantSum += numbers[item];
			}
			Console.WriteLine($"sum: {importantSum}");
		}
	}
}