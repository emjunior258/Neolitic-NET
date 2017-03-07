using System;
using Neolitic;
using Neolitic.Attributes;
using Neolitic.Abstracts;
using System.Collections;
using System.Collections.Generic;


namespace NeoliticTest
{
	public class BankingServices : BaseContextualized
	{


		[Service("BALANCE")]
		public void CheckBalance(){

			BankingContext bankingContext = GetContext<BankingContext>();
			String account = bankingContext.GetOptional<String> ("account");

			//Check balance of all the accounts
			if (account == null) {

				IList<BalanceInfo> balances = new List<BalanceInfo> ();
				BalanceInfo balance1 = new BalanceInfo {
					Balance = bankingContext.GetBalance ("100021"),
					Account = "100021"
				};

				BalanceInfo balance2 = new BalanceInfo {
					Balance = bankingContext.GetBalance ("100023"),
					Account = "100023"
				};

				balances.Add (balance1);
				balances.Add (balance2);
				bankingContext.Set ("balance", balances);
				return;
			}

			//Balance of one single account

			if(!bankingContext.AccountExists(account))
				bankingContext.Fail("INEXISTENT_ACCOUNT"); //Exit with error

			bankingContext.Set ("balance", bankingContext.GetBalance (account));

		}


		[Service("TRANSFER")]
		public void DoTransference(){

			BankingContext bankingContext = GetContext<BankingContext>();

			String destination = bankingContext.Get<String> ("destination");
			String origin = bankingContext.Get<String> ("origin");
			decimal amount = bankingContext.Get <Decimal> ("amount");

			//We assume the origin account exists - we only check the destination
			if (!bankingContext.AccountExists (destination)){
				bankingContext.Set ("account", origin);
				bankingContext.Fail("INEXISTENT_ACCOUNT"); //Exit with error
			}
			
			decimal balance = bankingContext.GetBalance (origin);
			if (balance < amount)
				this.Context.Fail("INSUFFICIENT_FUNDS"); //Exit with error

			//TODO: Do the transference here
		}
		
	}
}

