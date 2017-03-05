using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public interface IExecutionContext
    {

        /// <summary>
        /// This method is invoked when the Execution of a command begins. Right before
        /// the mapped service method get invoked. This is the right moment to interrupt the command execution.
        /// It's also the proper place to implement service deactivation logic.
        /// If the <see cref="IExecutionContext.Fail"/> method is invoked inside this method, then, the mapped service method wont even be executed,
        /// on the other hand, the <see cref="IExecutionContext.OnExecutionEnd"/> will be executed and then the error message will be resolved and sent.
        /// Throwing an <see cref="Exception"/> inside this method has the same effect with invoking the <see cref="IExecutionContext.Fail(string) method."/>
        ///<returns>True if the current command execution must be suspended</returns>
        /// </summary>
        bool OnExecutionStart();


        /// <summary>
        /// <see cref="ServiceInfo"/> instance regarding the current command in execution
        /// </summary>
        ServiceInfo Service { get; set; }

        /// <summary>
        /// Tells if the active command execution failed or succeeded. 
        /// The value for this property turns True by throwing an <see cref="Exception"/> inside the mapped service method
        /// or inside <see cref="IExecutionContext.OnExecutionStart(out bool)"/> or <see cref="IExecutionContext.OnExecutionEnd"/>
        /// or by invoking the <see cref="IExecutionContext.Fail(string)"/> method.
        /// The value of this property is exclusively set by the <see cref="IContainer"/> running the <see cref="IExecutionContext"/>
        /// </summary>
        bool ExecutionFailed { get; set; }


        /// <summary>
        /// Fails the current command execution. Throws an <see cref="CommandExecutionFailException"/>
        /// </summary>
        /// <param name="errorCode">the error code to be resolved into error message</param>
        void Fail(String errorCode);


        /// <summary>
        /// Gets the ICommandKeywords instance for the current command in execution 
        /// </summary>
        ICommandKeywords Keywords { get; }


        /// <summary>
        /// This method gets invoked right before the reply message is sent and right after the mappend service method invocation end.
        /// This methods gets invoked even if the mapped service method is not.
        /// Throwing an <see cref="Exception"/> inside this method will cause an execution failure, similar to the invocation of the <see cref="IExecutionContext.Fail(string)"/> method.
        /// </summary>
        void OnExecutionEnd();

        /// <summary>
        /// Error code of currrent command execution failure.
        /// The value of this property is exclusively set by the <see cref="IContainer"/> instance running the <see cref="IExecutionContext"/>.
        /// </summary>
        String ErrorCode { get; set; }


        /// <summary>
        /// Arguments passed to the command in execution
        /// </summary>
        String Arguments { get; set; }

        /// <summary>
        /// Exception that caused the command execution to fail.
        /// The value of this property is exclusively set by the <see cref="IContainer"/> instance running the <see cref="IExecutionContext"/>.
        /// </summary>
        Exception FailureCause { get; set; }


        IContainer Container { get; set; }


		/// <summary>
		/// Token that represents NULL in the command in execution.
		/// This value is used to determine if an optional token is present or not in the command.
		/// </summary>
		/// <value>The null token.</value>
		String NullToken {get;set;}


    }
}
