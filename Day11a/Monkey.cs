using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11a
{
	enum MonkeyOperator
	{
		Add,
		Multiply
	}
	internal class Monkey
	{
		public List<int> Items = new List<int>();
		public int OperationAmount = 0;
		public MonkeyOperator Operator = MonkeyOperator.Add;
		public int DivisionTest = 0;
		public int TrueTarget = 0;
		public int FalseTarget = 0;
		public int InspectionCount = 0;
	}
}
