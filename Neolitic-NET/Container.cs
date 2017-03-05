using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Neolitic
{
    public class Container : IContainer
    {
        private IContextFactory _contextFactory;
        private IServiceIdentifier _serviceIdentifier;
        private IErrorMessageResolver _errMessageResolver;
        private IDictionary<String, Object> _featureObjects = new Dictionary<String, Object>();
        private IDictionary<String, MethodInfo> _mappedServices = new Dictionary<String, MethodInfo>();
        private IList<IValueFormatter> _formatters = new List<IValueFormatter>();
        private IList<IValueParser> _parsers = new List<IValueParser>();
        private IList<IValuesInterpreter> _interpreters = new List<IValuesInterpreter>();

        public void AddFeature(IFeature feature)
        {
            //TODO: Scan mappend service methods
        }

        public void AddFormatter(IValueFormatter formatter)
        {
            _formatters.Add(formatter);
        }

        public void AddParser(IValueParser parser)
        {
            _parsers.Add(parser);
        }

        public void AddValuesInterpreter(IValuesInterpreter interpreter)
        {
            _interpreters.Add(interpreter);
        }

        private MethodInfo GetServiceMethodInfo(String code, out Object featureObject)
        {
            //TODO: Implement
        }

        public ExecutionResult ExecuteCommand(string command)
        {
            try
            {
                String arguments = null;
                ServiceInfo info = _serviceIdentifier.IdentifyService(command, out arguments);
                IExecutionContext context = _contextFactory.CreateContext(command, info, arguments);
                context.Service = info;
                context.Arguments = arguments;

                //TODO Check if service code was mapped : throw UnsuportedServiceException if not mapped
                MethodInfo serviceMethodInfo = null;
                Object featureObject = null;
                serviceMethodInfo = GetServiceMethodInfo(info.Code, out featureObject);
                bool stopExecution = false;
                try
                {
                    //Command execution start hook
                    stopExecution = context.OnExecutionStart(); 
                    context.ExecutionFailed = false;
                }
                catch (Exception ex)
                {
                    HandleException(ex, context);
                }

                //There is no wish to stop the command execution and the execution didn't fail earlier
                if ((!stopExecution)&&(!context.ExecutionFailed))
                {                  
                    try
                    {
                        //Invoke the mapped service method
                        serviceMethodInfo.Invoke(featureObject, new Object[] { });
                  
                    }catch(Exception ex)
                    {
                        context.FailureCause = ex;
                        HandleException(ex, context);
                    }
                }

                try
                {
                    //Command execution end hook
                    context.OnExecutionEnd();         
                           
                }catch(Exception ex)
                {
                    context.FailureCause = ex;
                    HandleException(ex, context);
                }

                //TODO: Compose the result message : invoke formatters
                //TODO: Send the result message
                //TODO: Create ExecutionResult instance

            }
            catch (Exception ex)
            {
                throw new ContainerException("An Unexpected Exception was thrown during the command execution", ex,command);
            }
        }


        public void Start(IContextFactory contextFactory, IServiceIdentifier serviceIdentifier, IErrorMessageResolver errMessageResolver)
        {
            throw new NotImplementedException();
        }

        private String ComponseResultMessage(IExecutionContext context, String template)
        {
            //Read keywords present in template and run the respective formatters while ignoring the non registered formatters
        }

        private List<KeywordToken> FindTokens(String template)
        {
            "{{}}"
        }

        private void SetTokenValues(List<KeywordToken> tokens)
        {

        }

        private void HandleException(Exception e, IExecutionContext context)
        {
            if (e is CommandExecutionFailException)
            {
                CommandExecutionFailException ex = (CommandExecutionFailException) e;

                //Fail method was explicitly invoked: use the error code set on exception object
                context.ErrorCode = ex.ErrorCode;
                context.ExecutionFailed = true;

            }else
            {

                //Implicit invocation of Fail method: use the default error code
                context.ErrorCode = CommandExecutionException.DEFAULT_ERROR_CODE;
                context.ExecutionFailed = true;

            }    

        }

        private class KeywordToken
        {
            public String[] Formatters { get; set; }

            public String Key { get; set; }

            public String Value { get; set; }
        }

    }
}
