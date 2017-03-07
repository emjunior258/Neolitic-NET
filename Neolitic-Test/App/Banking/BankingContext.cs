using System;
using Neolitic;
using Neolitic.Abstracts;

namespace NeoliticTest
{
	public class BankingContext : ExecutionContext
	{
		private String _costumerId;

		public BankingContext(String customerId){

			this._costumerId = customerId;

		}
			
		public String CustomerID {get { return _costumerId; }}


		public bool AccountExists(String account){

			return account == "100021" || account == "100023"|| account == "100024";

		}


		public decimal GetBalance(String account){

			//You could be fetching this data from a database
			if (account == "1000023")
				return 23000;
			else if (account == "100024")
				return 45000;

			else if (account == "100021")
				return 1000;
			
			return 0;
		}


		//Other context methods and attributes may come here
	}
}

