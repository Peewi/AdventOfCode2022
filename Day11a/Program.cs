namespace Day11a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Monkey> monkeys = new List<Monkey>();
			string[] input = File.ReadAllLines("input.txt");
			// parse input
			for (int i = 0; i < input.Length; i+=7)
			{
				monkeys.Add(new Monkey());

				string[] itemSplit = input[i + 1].Split(':', ',');
				string[] opSplit = input[i + 2].Split(' ');
				string[] divSplit = input[i + 3].Split(' ');
				string[] trueSplit = input[i + 4].Split(' ');
				string[] falseSplit = input[i + 5].Split(' ');

				for (int j = 1; j < itemSplit.Length; j++)
				{
					monkeys[^1].Items.Add(int.Parse(itemSplit[j]));
				}
				if (opSplit[^2] == "*")
				{
					monkeys[^1].Operator = MonkeyOperator.Multiply;
				}
				if (opSplit[^1] == "old")
				{
					monkeys[^1].OperationAmount = -1;
				}
				else
				{
					monkeys[^1].OperationAmount = int.Parse(opSplit[^1]);
				}
				monkeys[^1].DivisionTest = int.Parse(divSplit[^1]);
				monkeys[^1].TrueTarget = int.Parse(trueSplit[^1]);
				monkeys[^1].FalseTarget = int.Parse(falseSplit[^1]);
			}
			// run program
			const int NUMROUNDS = 20;
			const int RELIEFDIVISION = 3;
			for (int i = 0; i < NUMROUNDS; i++)
			{
				for (int j = 0; j < monkeys.Count; j++)
				{
					while (monkeys[j].Items.Count > 0)
					{
						int valMod = monkeys[j].OperationAmount;
						if (valMod == -1)
						{
							valMod = monkeys[j].Items[0];
						}
						switch (monkeys[j].Operator)
						{
							case MonkeyOperator.Add:
								monkeys[j].Items[0] += valMod;
								break;
							case MonkeyOperator.Multiply:
								monkeys[j].Items[0] *= valMod;
								break;
							default:
								break;
						}
						monkeys[j].Items[0] /= RELIEFDIVISION;
						int target = monkeys[j].Items[0] % monkeys[j].DivisionTest == 0 ? monkeys[j].TrueTarget : monkeys[j].FalseTarget;
						monkeys[target].Items.Add(monkeys[j].Items[0]);
						monkeys[j].Items.RemoveAt(0);
						monkeys[j].InspectionCount++;
					}
				}
				Console.WriteLine($"Monkeys finished round {i+1}");
			}
			long peak = 0;
			long peak2 = 0;
			foreach (Monkey monkey in monkeys)
			{
				if (monkey.InspectionCount > peak)
				{
					peak2 = peak;
					peak = monkey.InspectionCount;
				}
				else if (monkey.InspectionCount > peak2)
				{
					peak2 = monkey.InspectionCount;
				}
			}
			Console.WriteLine($"Peak inspection counts: {peak} and {peak2}");
			Console.WriteLine($"Monkey busines: {peak * peak2}");
		}
	}
}