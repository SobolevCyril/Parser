using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakerApp
{
	public class TestData
	{
		public static void Fill(Dictionary<string, object> DataRepository)
		{
			DataRepository.Add("m0001", 1);
			DataRepository.Add("m1001", 1);
			DataRepository.Add("m1002", 6);
			DataRepository.Add("m1003", 100);
			DataRepository.Add("m1004", 70);
			DataRepository.Add("m1005", 23);
			DataRepository.Add("m1006", 24);
			DataRepository.Add("m1007", 1);
			DataRepository.Add("m1008", 0);
			DataRepository.Add("m1009", 7);
			DataRepository.Add("m1010", 0);
			DataRepository.Add("m1011", 277);
		}
	}
}
