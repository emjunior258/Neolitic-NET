using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace NeoliticTest
{
	public class BankingServiceIdentifier : BaseServiceIdentifier
	{

		private IDictionary<String,IServiceInfo> _services = new Dictionary<String,IServiceInfo>();

		protected override IServiceInfo GetServiceInfo (string serviceId)
		{

			IServiceInfo service = null;

			if (!_services.ContainsKey (serviceId))
				return service;
			else
				_services.TryGetValue (serviceId, out service);

			return service;
			
		}

		public BankingServiceIdentifier(){


			_createTransferServiceInfo();
			_createBalanceServiceInfo ();
			//TODO: More services will come here: BALANCE and others

		}


		private void _createBalanceServiceInfo(){

			ServiceInfo balance = new ServiceInfo ("BALANCE");

			//The ? after "account" means that the "account" argument is optional
			balance.ArgumentsMapping = "{account?} {@pin}";

			balance.SuccessMessage = "Your balance is {balance|fbalance}";
			_services.Add (balance.Id,balance);

		}

		private void _createTransferServiceInfo(){

			//You could be fetching this data from a database or whatever u want
			ServiceInfo transfer = new ServiceInfo ("TRANSFER");
			transfer.Description = "Executes a banking transference";

			//The @ before "pin" means that the pin value must be captured.
			//The : after "amount" means that the Parser with name "decimal" will be used on the amount argument
			transfer.ArgumentsMapping = "{origin} {amount:pdecimal} {destination} {@pin}"; 

			//The | after "amount" means that the amout value must be formatted using the a formatter named "money"
			transfer.SuccessMessage = "You have successfully transfered {amount|money} to {destination}";

			_services.Add (transfer.Id, transfer);

		}



		
	}
}

