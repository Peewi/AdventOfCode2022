using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11b
{
	enum MonkeyOperator
	{
		Add,
		Multiply
	}
	internal class Monkey
	{
		public List<long> Items = new List<long>();
		public int OperationAmount = 0;
		public MonkeyOperator Operator = MonkeyOperator.Add;
		public int DivisionTest = 0;
		public int TrueTarget = 0;
		public int FalseTarget = 0;
		public int InspectionCount = 0;
	}
}
