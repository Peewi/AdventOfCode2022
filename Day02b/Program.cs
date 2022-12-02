namespace Day2
{
	internal class Program
	{
		private const int DRAWPOINTS = 3;
		private const int WINPOINTS = 6;

		enum RPS
		{
			Rock = 1,
			Paper = 2,
			Scissors = 3,
		}
		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("input.txt");
			int totalScore = 0;
			foreach (string line in lines)
			{
				RPS opp;
				RPS me;
				switch (line[0])
				{
					case 'A':
						opp = RPS.Rock;
						break;
					case 'B':
						opp = RPS.Paper;
						break;
					case 'C':
						opp = RPS.Scissors;
						break;
					default:
						throw new Exception();
				}
				switch (line[2])
				{
					case 'X':
						me = opp switch
						{
							RPS.Rock => RPS.Scissors,
							RPS.Paper => RPS.Rock,
							RPS.Scissors => RPS.Paper,
							_ => throw new Exception(),
						};
						break;
					case 'Y':
						me = opp;
						break;
					case 'Z':
						me = opp switch
						{
							RPS.Rock => RPS.Paper,
							RPS.Paper => RPS.Scissors,
							RPS.Scissors => RPS.Rock,
							_ => throw new Exception(),
						};
						break;
					default:
						throw new Exception();
				}
				totalScore += (int)me;
				if (opp == me)
				{
					totalScore += DRAWPOINTS;
					Console.WriteLine($"Draw with {me}");
				}
				else if ((opp == RPS.Rock && me == RPS.Paper)
					|| (opp == RPS.Paper && me == RPS.Scissors)
					|| (opp == RPS.Scissors && me == RPS.Rock))
				{
					totalScore += WINPOINTS;
					Console.WriteLine($"Win with {me}");
				}
				else
				{
					Console.WriteLine($"Loss with {me}");
				}
			}
			Console.WriteLine($"Total score: {totalScore}");
		}
	}
}