using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakerApp
{
	/// <summary>
	/// Класс представляющий множетство значений и диапазонов, которые используются для написания логических выражений 
	/// в SRZ вида m0001 = 1..5, 7,9,12
	/// </summary>
	public class SrzSet
	{
		private List<SrzRange<decimal>> RangeList;
		private List<decimal> ValueList;

		public SrzSet()
		{
			RangeList = new List<SrzRange<decimal>>();
			ValueList = new List<decimal>();
		}

		public void Add(decimal value)
		{
			ValueList.Add(value);
		}

		public void Add(SrzRange<decimal> range)
		{
			RangeList.Add(range);
		}

		public void Clear()
		{
			RangeList.Clear();
			ValueList.Clear();
		}

		/// <summary>
		/// Проверка вхождения значения во множество
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool In(decimal value)
		{
			return ValueList.Any(v => v == value) || RangeList.Any(r => r.ContainsValue(value));
		}
	}
}
