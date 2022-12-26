namespace Day20b
{
	internal class Program
	{
		static void Main(string[] args)
		{
			const long DECRYPTION = 811589153;
			const int MIXCOUNT = 10;
			string[] input = File.ReadAllLines("input.txt");
			List<long> numbers = new List<long>(input.Length);
			List<int> locations = new List<int>(input.Length);
			for (int i = 0; i < input.Length; i++)
			{
				string line = input[i];
				numbers.Add(int.Parse(line) * DECRYPTION);
				locations.Add(i);
			}

			for (int time = 0; time < MIXCOUNT; time++)
			{
				for (int i = 0; i < locations.Count; i++)
				{
					int index = locations[i];
					long moveNumber = numbers[index];
					long realNumber = numbers[index];
					moveNumber = Math.Sign(moveNumber) * ((Math.Abs(moveNumber) % (numbers.Count - 1)));
					//while (Math.Abs(moveNumber) >= numbers.Count)
					//{
					//	moveNumber = Math.Sign(moveNumber) * (Math.Abs(moveNumber) - (numbers.Count - 1));
					//}
					int newIndex = index + (int)moveNumber;
					newIndex = ((newIndex % numbers.Count) + numbers.Count) % numbers.Count;

					if (Math.Sign(moveNumber) != Math.Sign(newIndex - index))
					{
						newIndex -= Math.Sign(newIndex - index);
					}
					numbers.RemoveAt(index);
					numbers.Insert(newIndex, realNumber);
					// readjust locations
					//locations[i] = newIndex;
					for (int j = 0; j < locations.Count; j++)
					{
						if (i == j)
						{
							locations[i] = newIndex;
						}
						else if (newIndex - index > 0 && locations[j] <= newIndex && locations[j] > index)
						{
							locations[j]--;
						}
						else if (newIndex - index < 0 && locations[j] >= newIndex && locations[j] < index)
						{
							locations[j]++;
						}
						locations[j] = ((locations[j] % locations.Count) + locations.Count) % locations.Count;
					}
				}
				Console.WriteLine($"List mixed {time + 1} time(s)");
			}
			int zeroPos = numbers.IndexOf(0);
			int[] important = new int[]
			{
				(zeroPos + 1000) % numbers.Count,
				(zeroPos + 2000) % numbers.Count,
				(zeroPos + 3000) % numbers.Count,
			};
			long importantSum = 0;
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