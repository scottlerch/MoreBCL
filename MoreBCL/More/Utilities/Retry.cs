namespace More.Utilities
{
    using System;
    using System.Threading;

    /// <summary>
    /// Helper methods to retry failed actions multiple times.
    /// </summary>
    public static class Retry
    {
        /// <summary>
        /// Retry on exception.
        /// </summary>
        /// <param name="numberOfRetries"></param>
        /// <param name="waitBetweenRetriesMsec"></param>
        /// <param name="doubleWaitOnRetry"></param>
        /// <param name="rethrowFinalException"></param>
        /// <param name="run"></param>
        /// <param name="onException"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T OnException<T>(
            int numberOfRetries = 5,
            int waitBetweenRetriesMsec = 1000,
            bool doubleWaitOnRetry = false,
            bool rethrowFinalException = true,
            Func<T> run = null,
            Action<Exception> onException = null)
        {
            if (run == null)
            {
                return default(T);
            }

            int currentNumberOfRetries = 0;

            for (; ; )
            {
                try
                {
                    return run();
                }
                catch (Exception ex)
                {
                    if (onException != null)
                    {
                        onException(ex);
                    }

                    currentNumberOfRetries++;

                    if (currentNumberOfRetries >= numberOfRetries)
                    {
                        if (rethrowFinalException)
                        {
                            throw;
                        }

                        return default(T);
                    }

                    Thread.Sleep(doubleWaitOnRetry ? waitBetweenRetriesMsec * currentNumberOfRetries : waitBetweenRetriesMsec);
                }
            }
        }

        /// <summary>
        /// Retry on exception.
        /// </summary>
        /// <param name="numberOfRetries"></param>
        /// <param name="waitBetweenRetriesMsec"></param>
        /// <param name="doubleWaitOnRetry"></param>
        /// <param name="rethrowFinalException"></param>
        /// <param name="run"></param>
        /// <param name="onException"></param>
        public static void OnException(
            int numberOfRetries = 5,
            int waitBetweenRetriesMsec = 1000,
            bool doubleWaitOnRetry = false,
            bool rethrowFinalException = true,
            Action run = null,
            Action<Exception> onException = null)
        {
            if (run == null)
            {
                return;
            }

            var currentNumberOfRetries = 0;

            for (; ; )
            {
                try
                {
                    run();
                    return;
                }
                catch (Exception ex)
                {
                    if (onException != null)
                    {
                        onException(ex);
                    }

                    currentNumberOfRetries++;

                    if (currentNumberOfRetries >= numberOfRetries)
                    {
                        if (rethrowFinalException)
                        {
                            throw;
                        }

                        return;
                    }

                    Thread.Sleep(doubleWaitOnRetry ? waitBetweenRetriesMsec * currentNumberOfRetries : waitBetweenRetriesMsec);
                }
            }
        }

        /// <summary>
        /// Retry on false returned.
        /// </summary>
        /// <param name="numberOfRetries"></param>
        /// <param name="waitBetweenRetriesMsec"></param>
        /// <param name="doubleWaitOnRetry"></param>
        /// <param name="run"></param>
        /// <param name="onFalse"></param>
        /// <returns></returns>
        public static bool OnFalse(
            int numberOfRetries = 5,
            int waitBetweenRetriesMsec = 1000,
            bool doubleWaitOnRetry = false,
            Func<bool> run = null,
            Action onFalse = null)
        {
            if (run == null)
            {
                return false;
            }

            var currentNumberOfRetries = 0;

            for (; ; )
            {
                if (run())
                {
                    return true;
                }

                if (onFalse != null)
                {
                    onFalse();
                }

                currentNumberOfRetries++;

                if (currentNumberOfRetries >= numberOfRetries)
                {
                    return false;
                }

                Thread.Sleep(
                    doubleWaitOnRetry ? waitBetweenRetriesMsec * currentNumberOfRetries : waitBetweenRetriesMsec);
            }
        }
    }
}
