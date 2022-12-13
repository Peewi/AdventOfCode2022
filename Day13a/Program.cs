namespace Day13a
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines("input.txt");
			Console.WriteLine($"~Part 1~");
			List<Packet> packetPairs = new List<Packet>();
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i].Length > 0)
				{
					List<Packet> parentStack = new List<Packet>() { new Packet() };
					packetPairs.Add(parentStack[0]);
					for (int j = 1; j < input[i].Length; j++)
					{
						if (input[i][j] == '[')
						{
							Packet newPacket = new Packet();
							parentStack[^1].Children.Add(newPacket);
							parentStack.Add(newPacket);
						}
						else if (input[i][j] == ']')
						{
							parentStack.RemoveAt(parentStack.Count - 1);
						}
						else if (input[i][j] == ',')
						{

						}
						else
						{
							int numEnd = 0;
							for (int k = j+1; k < input[i].Length; k++)
							{
								if (input[i][k] == ']' || input[i][k] == ',')
								{
									numEnd = k;
									break;
								}
							}
							int n = int.Parse(input[i][j..numEnd]);
							parentStack[^1].Children.Add(new Packet(n));
						}
					}
				}
				string[] brack = input[i].Split('[');
			}
			Console.WriteLine($"Parsed {packetPairs.Count} packets");
			int score = 0;
			int pairNum = 1;
			for (int i = 0; i < packetPairs.Count; i+=2)
			{
				int comp = Packet.Compare(packetPairs[i], packetPairs[i + 1]);
				if (comp == 1)
				{
					Console.WriteLine($"Pair {pairNum} is NOT in the right order");
				}
				else if (comp == -1)
				{
					Console.WriteLine($"Pair {pairNum} is in right order");
					score += pairNum;
				}
				else
				{
					Console.WriteLine($"Pair {pairNum} is ???");
				}
				pairNum++;
			}
			Console.WriteLine($"Sum of correct indices: {score}");
			Console.WriteLine($"~Part 2~");
			Packet[] specialPackets = new Packet[]
			{
				packetPairs[^1],
				packetPairs[^2]
			};
			Console.WriteLine($"Sorting");
			packetPairs.Sort((a, b) => Packet.Compare(a, b));
			int decoder = 1;
			for (int i = 0; i < packetPairs.Count; i++)
			{
				for (int j = 0; j < specialPackets.Length; j++)
				{
					if (packetPairs[i] == specialPackets[j])
					{
						Console.WriteLine($"Special packet found at index {i + 1}");
						decoder *= i + 1;
					}
				}
			}
			Console.WriteLine($"Decoder key: {decoder}");
		}
	}

	class Packet
	{
		public int Value = -1;
		public List<Packet>? Children;

		public Packet()
		{
			Children = new List<Packet>();
		}

		public Packet(int val)
		{
			Value = val;
			Children = null;
		}

		public static int Compare(Packet left, Packet right)
		{
			if (left.Value >= 0 && right.Value >= 0)
			{
				return Math.Sign(left.Value - right.Value);
			}
			if (right.Value >= 0)
			{
				Packet packagedRight = new Packet();
				packagedRight.Children?.Add(right);
				return Compare(left, packagedRight);
			}
			if (left.Value >= 0)
			{
				Packet packagedLeft = new Packet();
				packagedLeft.Children?.Add(left);
				return Compare(packagedLeft, right);
			}
			if (left.Children is List<Packet> lC && right.Children is List<Packet> rC)
			{
				int lowerCount = Math.Min(lC.Count, rC.Count);
				for (int i = 0; i < lowerCount; i++)
				{
					int comp = Compare(lC[i], rC[i]);
					if (comp != 0)
					{
						return comp;
					}
				}
				if (lC.Count < rC.Count)
				{
					return -1;
				}
				if (lC.Count > rC.Count)
				{
					return 1;
				}
			}
			return 0;
		}
	}
}