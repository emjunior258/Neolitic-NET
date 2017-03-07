# Neolitic
Framework to build a text commands based back-end employing a high level separation of concerns and abstraction.

## How does Neolitic Help?
When you are developing a back-end that receives service commands in string format and then returns response strings, you are
suscepible to write boilerplate code and end up writing some tricky and unmaintainable code.

Lets think of a banking text command interface that offers two basic services: BALANCE consults and
Money TRANSFERS via the following text commands:

1. BAL <account_number> <pin_code>
a) Success Result: "The balance of the account X is Y";

2. TRANSF <from_account> <amount> <to_account> <pin_code>;
a) Success Result: "You have successfully transfered X from Y to Z";

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

```json
{accountNumber} {pinCode}
```
The advatange of mapping the arguments is that, when we ask the Neolitic Container to execute a balance command
like this one: 

```pre
BAL 1021212 1234
```

It will automatically fetch the parameters using the names with have given them in the mapping string and will store their respective values into
a Key-Value variable that will be accessible to the service method.
*You wont have to SubString the command. The values will already be there for you*.

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
Why you need Parsers? Because when Neolitic reads the arguments of a command to store then into the key-Value pairs
variable, it stores them as string object, which means, if you try to Get an argument parsing it to a type different
from String, an InvalidCastException will be thrown:

```c#
...

	//This will cause an InvalidCastException : you cant cast a String to Int32
	int accountNumber = this.Context.Get<Int32>("accountNumber");
	
...
	



## Message values bindind

## Key-Value pairs

## Capturers

## Formatters

## Parsers

## Thread-bound Context



## Arguments Mapping
Neolitic asks you to map the arguments of each of your services. When you map the arguments you are telling it
the arguments the service expects


## Neolitic Introduction - A Neolitic Bank Application Example