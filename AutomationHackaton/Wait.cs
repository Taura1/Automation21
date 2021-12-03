using System;
using System.Threading;

namespace AutomationHackaton
{
    public static class Wait
    {
        public static void WaitFor(Func<(bool result, Exception exception)> action, double timeoutInSecs)
        {
            var start = DateTime.Now;
            var actionResult = false;
            Exception actionException = null;
            var iteration = 0;

            while ((DateTime.Now - start).TotalSeconds < timeoutInSecs || iteration < 1)
            {
                var outcome = action();
                actionResult = outcome.result;
                actionException = outcome.exception;

                if (actionResult)
                {
                    break;
                }

                Thread.Sleep(500);
                iteration++;
            }

            if (!actionResult)
            {
                string outputMessage = $"Action returned false after timeout (in {iteration} tries)";
                Exception outputException = actionException == null
                    ? new TimeoutException(outputMessage)
                    : new TimeoutException(outputMessage, actionException);
                throw outputException;
            }
        }
    }
}
