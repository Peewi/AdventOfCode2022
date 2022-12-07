using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07a
{
	internal class Directory
	{
		public Directory Parent = null;
		public int Size = 0;

		public Directory(Directory parent = null)
		{
			Parent = parent;
		}

		public void IncreaseSize(int amount)
		{
			Size += amount;
			Parent?.IncreaseSize(amount);
		}
	}
}
