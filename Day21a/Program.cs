namespace Day21a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, long> monkeyNumbers = new Dictionary<string, long>();
			Dictionary<string, (string, string, string)> monkeyCalc = new Dictionary<string, (string, string, string)>();
			string[] input = File.ReadAllLines("input.txt");
			foreach (string line in input)
			{
				string[] split = line.Split(' ');
				string name = split[0][0..4];
				if (split.Length == 2)
				{
					monkeyNumbers.Add(name, int.Parse(split[1]));
				}
				else
				{
					monkeyCalc.Add(name, (split[1], split[2], split[3]));
				}
			}
			//
			const string IMPORTANTMONKEY = "root";
			while (monkeyCalc.Count > 0)
			{
				List<string> foundMonkeys = new List<string>();
				foreach (KeyValuePair<string, (string, string, string)> item in monkeyCalc)
				{
					if (monkeyNumbers.ContainsKey(item.Value.Item1) && monkeyNumbers.ContainsKey(item.Value.Item3))
					{
						long num1 = monkeyNumbers[item.Value.Item1];
						long num2 = monkeyNumbers[item.Value.Item3];
						long result = 0;
						switch (item.Value.Item2)
						{
							case "+":
								result = num1 + num2;
								break;
							case "-":
								result = num1 - num2;
								break;
							case "*":
								result = num1 * num2;
								break;
							case "/":
								result = num1 / num2;
								break;
							default:
								throw new Exception();
						}
						monkeyNumbers.Add(item.Key, result);
						foundMonkeys.Add(item.Key);
						if (item.Key == IMPORTANTMONKEY)
						{
							break;
						}
					}
				}
				if (monkeyNumbers.ContainsKey(IMPORTANTMONKEY))
				{
					break;
				}
				foreach (string item in foundMonkeys)
				{
					monkeyCalc.Remove(item);
				}
			}
			Console.WriteLine($"{IMPORTANTMONKEY} will yell {monkeyNumbers[IMPORTANTMONKEY]}");
		}
	}
}