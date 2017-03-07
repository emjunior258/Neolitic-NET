# Neolitic
Framework to build a text commands based back-end employing a high level separation of concerns and abstraction.

## How does Neolitic Help?
When you are developing a back-end that receives service commands in string format and then returns response strings, you are
suscepible to write boilerplate code and end up writing some tricky and unmaintainable code.

## A Banking text commands interface example
Lets think of a banking text commands interface that offers two basic services: __BALANCE checks__ and
__Money TRANSFERS__ via the following text commands:

### Balance check command

__Syntax__
```pre
BAL <account_number> <pin_code>
```
__Expected Outcome__
```pre
"The balance of the account X is Y"
```

### Money transfer command

__Syntax__
```pre
TRANSF <from_account> <amount> <to_account> <pin_code>
```
__Expected Outcome__: 
```pre
"You have successfully transfered X from Y to Z"
```

These two services have a few things in common:
* They both receive arguments and need to access them in order to proceed.
* They both receive the pin_code as an argument and they must validate it and return immediatelly if the validation fails.
* They both need to use some of the input arguments to compose the result message.

Neolitic understands that and brings the power you need to ease your development process. In order to do so,
it comes with a small set of concepts:

## Service arguments mapping
Each service must tell the arguments it expects (giving names), the order it expects them and how the service wants them to be 
parserd. 

The arguments mapping for the Balances services would be the following:

```pre
{accountNumber} {pinCode}
```
The advatange of mapping the arguments is that, when we ask the Neolitic Container to execute a balance command
like this one: 

```pre
BAL 1021212 1234
```

It will automatically fetch the parameters using the names you have given them in the mapping string and will store their respective values in
a Key-Value variable that will be accessible to the service method.
__The values will already be there for you.__

You will be able to retrive them simply by doint this:

```c#
...
public void CheckBalance(){

	String accountNumber = this.Context.Get<String>("accountNumber");
	String pinCode = this.Context.Get<String>("pinCode");

	//Consult the balance here

}
...

```

## Parsers

In the Neolitic world, a Parser is an object that receives a string object and parses it to a different format.
Why you need Parsers? Because when Neolitic reads the arguments of a command to store then in the key-Value pairs
variable, it stores them as string object, which means, *if you try to Get an argument parsing it to a type different
from String, an InvalidCastException will be thrown*:

```c#
...

	//This will cause an InvalidCastException : you cant cast a String to Int32
	int accountNumber = this.Context.Get<Int32>("accountNumber");
	
...
```

Parsers allow you to tell Neolitic to parse the value of the argument and then take the parse result and 
store in the Key-Values variable. 

Parsers are define per argument. Let us show you an example:

```pre
//The "accountNumber" argument will be parsed using a built-in parser named "pint32"
{accountNumber:pint32} {pinCode}
```

After doing this, you can safelly cast the *accountNumber* value

```c#
...

	//Now it will work
	int accountNumber = this.Context.Get<Int32>("accountNumber");
	
...

Neolitic comes with built-in parsers (pint32, pint16, pdecimal, pdouble, pint64, pbool) and of course we allow you to create your own.

## Capturation

Both services need to validate the *pinCode* and they must return with an error message if the pin code
is not valid. Neolitic offers you an elegant way to handling this situation: Capturation.
Capturation is the process of taking an argument's value and hand it to a Capturer in order to perform some validation
before the service method is invoked. A Capturer is some sort of argument interceptor.

Why are Capturers great? Because they allow you to decouple validation code from operation code.
Lets solve the pin validation problem with a Capturer:

```c#

...

[Captures("pinCode")]
	public class BankingPinCapturer : BaseCapturer
	{


		//This method will receive the captured value
		public override void Captured (object value)
		{

			//If the pin is not 1234 then
			//The command execution will be stopped with the INVALID_PIN error code
			if(!value.ToString()=="1234")
				this.Context.Exit("INVALID_PIN"); 
			
		}
		
		
	}


...

```

If we want the __pinCode__ argument present in the check balance command to be captured, we must
prepend a "@" caracter:

```pre
//Every argument preceded by the "@" character, will be captured
{accountNumber:pint32} {@pinCode}
```


## Error Messages resolution

## Message values bindind

## Formatters

## Thread-bound Context
