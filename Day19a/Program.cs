using System.ComponentModel;
using System.Diagnostics;

namespace Day19a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines("input.txt");
			int[] blueprintScores = new int[input.Length];
			List<Blueprint> blueprints = new List<Blueprint>();
			foreach (string line in input)
			{
				string[] split = line.Split(' ');
				List<int> inputNumbers = new List<int>();
				foreach (string substr in split)
				{
					if (int.TryParse(substr, out int parseResult))
					{
						inputNumbers.Add(parseResult);
					}
				}
				Blueprint bp = new Blueprint()
				{
					OreRobotCost = new Resource(inputNumbers[0],0,0,0),
					ClayRobotCost = new Resource(inputNumbers[1], 0, 0, 0),
					ObsidianRobotCost = new Resource(inputNumbers[2], inputNumbers[3], 0, 0),
					GeodeRobotCost = new Resource(inputNumbers[4], 0, inputNumbers[5], 0),
				};
				blueprints.Add(bp);
			}
			const int MAXTIME = 24;
			Resource OneOre = new Resource(1, 0, 0, 0);
			Resource OneClay = new Resource(0, 1, 0, 0);
			Resource OneObs = new Resource(0, 0, 1, 0);
			Resource OneGeode = new Resource(0, 0, 0, 1);
			for (int i = 0; i < blueprints.Count; i++)
			{
				Stopwatch bpTimer = new Stopwatch();
				bpTimer.Start();
				Blueprint bp = blueprints[i];
				blueprintScores[i] = 0;
				int bpPeakOre = Math.Max(bp.OreRobotCost.Ore, bp.ClayRobotCost.Ore);
				bpPeakOre = Math.Max(bpPeakOre, bp.ObsidianRobotCost.Ore);
				bpPeakOre = Math.Max(bpPeakOre, bp.GeodeRobotCost.Ore);
				List<(Resource rsc, Resource robotCount)> state = new(1000000)
				{
					(new Resource(), new Resource(1, 0, 0, 0))
				};
				HashSet<(Resource rsc, Resource robotCount)> unique = new(state);
				for (int time = 0; time < MAXTIME; time++)
				{
					Console.WriteLine($"Blueprint {i + 1} starting minute {time + 1}, has been running for {bpTimer.Elapsed}");
					for (int j = state.Count - 1; j >= 0; j--)
					{
						bool madeGeo = false;
						if (time < MAXTIME - 1)
						{
							if (state[j].rsc >= bp.GeodeRobotCost)
							{
								state.Add((state[j].rsc + state[j].robotCount - bp.GeodeRobotCost, state[j].robotCount + OneGeode));
								madeGeo = true;
							}
							else
							{
								if (state[j].rsc >= bp.OreRobotCost && state[j].robotCount.Ore < bpPeakOre)
								{
									state.Add((state[j].rsc + state[j].robotCount - bp.OreRobotCost, state[j].robotCount + OneOre));
								}
								if (state[j].rsc >= bp.ClayRobotCost && state[j].robotCount.Clay < bp.ObsidianRobotCost.Clay)
								{
									state.Add((state[j].rsc + state[j].robotCount - bp.ClayRobotCost, state[j].robotCount + OneClay));
								}
								if (state[j].rsc >= bp.ObsidianRobotCost && state[j].robotCount.Obsidian < bp.GeodeRobotCost.Obsidian)
								{
									state.Add((state[j].rsc + state[j].robotCount - bp.ObsidianRobotCost, state[j].robotCount + OneObs));
								}
							}
						}
						state[j] = (state[j].rsc + state[j].robotCount, state[j].robotCount);
						//if (!madeGeo)
						//{
						//	unique.Add(state[j]);
						//}
						blueprintScores[i] = Math.Max(blueprintScores[i], state[j].rsc.Geode);
					}
					//state.Clear();
					//state.AddRange(unique);
				}
				bpTimer.Stop();
				Console.WriteLine($"Blueprint {i + 1} score: {blueprintScores[i]}, ran for {bpTimer.Elapsed}");
			}
			int scoreTotal = 0;
			for (int i = 0; i < blueprintScores.Length; i++)
			{
				scoreTotal += blueprintScores[i] * (i + 1);
			}
			Console.WriteLine($"Total blueprint score: {scoreTotal}");
		}
	}

	struct Blueprint
	{
		public Resource OreRobotCost;
		public Resource ClayRobotCost;
		public Resource ObsidianRobotCost;
		public Resource GeodeRobotCost;
	}

	struct Resource
	{
		public int Ore;
		public int Clay;
		public int Obsidian;
		public int Geode;

		public Resource(int ore, int clay, int obsidian, int geode)
		{
			Ore = ore;
			Clay = clay;
			Obsidian = obsidian;
			Geode = geode;
		}

		public static Resource operator +(Resource a, Resource b) => new Resource(a.Ore + b.Ore, a.Clay + b.Clay, a.Obsidian + b.Obsidian, a.Geode + b.Geode);
		public static Resource operator -(Resource a, Resource b) => new Resource(a.Ore - b.Ore, a.Clay - b.Clay, a.Obsidian - b.Obsidian, a.Geode - b.Geode);
		public static bool operator <(Resource a, Resource b) => a.Ore < b.Ore && a.Clay < b.Clay && a.Obsidian < b.Obsidian && a.Geode < b.Geode;
		public static bool operator >(Resource a, Resource b) => a.Ore > b.Ore && a.Clay > b.Clay && a.Obsidian > b.Obsidian && a.Geode > b.Geode;
		public static bool operator <=(Resource a, Resource b) => a.Ore <= b.Ore && a.Clay <= b.Clay && a.Obsidian <= b.Obsidian && a.Geode <= b.Geode;
		public static bool operator >=(Resource a, Resource b) => a.Ore >= b.Ore && a.Clay >= b.Clay && a.Obsidian >= b.Obsidian && a.Geode >= b.Geode;
	}
}