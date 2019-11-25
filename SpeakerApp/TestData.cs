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
			//DataRepository.Add("m0001", 1);
			//DataRepository.Add("m1001", 1);
			//DataRepository.Add("m1002", 6);
			//DataRepository.Add("m1003", 100);
			//DataRepository.Add("m1004", 70);
			//DataRepository.Add("m1005", 23);
			//DataRepository.Add("m1006", 24);
			//DataRepository.Add("m1007", 1);
			//DataRepository.Add("m1008", 0);
			//DataRepository.Add("m1009", 7);
			//DataRepository.Add("m1010", 0);
			//DataRepository.Add("m1011", 277);

			DataRepository.Add("m0301", 2200);
			DataRepository.Add("m0302", 6);
			DataRepository.Add("m0303", 12);
			DataRepository.Add("m0304", 34);

			// --------------------------------------------
			//  not Any(T01, m0505> 2 and t0102> 0 and t0107 = 0) &
			//	not Any(T01, m0505 = 1, 2 and t0102 > 0 and t0107 > 0)
			DataRepository.Add("m0505", 2);
			DataRepository.Add("t0102", 2);
			DataRepository.Add("t0107", 2);
			DataRepository.Add("m23", 1);
			DataRepository.Add("m2301", 1);

		}
	}
}
