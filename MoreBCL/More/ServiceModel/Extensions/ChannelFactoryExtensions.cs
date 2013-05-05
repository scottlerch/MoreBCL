namespace More.ServiceModel.Extensions
{
    using System;
    using System.ServiceModel;

    /// <summary>
    /// ChannelFactory{T} extension methods.
    /// </summary>
    public static class ChannelFactoryExtensions
    {
        /// <summary>
        /// Use channel factory by creating channel and using it within a code block.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="channelFactory">The channel factory.</param>
        /// <param name="codeBlock">The code block.</param>
        public static void Use<TService>(
            this ChannelFactory<TService> channelFactory,
            Action<TService> codeBlock)
        {
            var proxy = (IClientChannel)channelFactory.CreateChannel();
            var success = false;

            try
            {
                codeBlock((TService)proxy);
                proxy.Close();
                success = true;
            }
            finally
            {
                if (!success && proxy != null)
                {
                    try
                    {
                        proxy.Abort();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Use channel factory by creating channel and using it within a code block.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="channelFactory">The channel factory.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="codeBlock">The code block.</param>
        public static void Use<TService>(
            this ChannelFactory<TService> channelFactory,
            EndpointAddress endpoint,
            Action<TService> codeBlock)
        {
            var proxy = (IClientChannel)channelFactory.CreateChannel(endpoint);
            var success = false;

            try
            {
                codeBlock((TService)proxy);
                proxy.Close();
                success = true;
            }
            finally
            {
                if (!success && proxy != null)
                {
                    try
                    {
                        proxy.Abort();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Use channel factory by creating channel and using it within a code block.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="channelFactory">The channel factory.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="codeBlock">The code block.</param>
        public static void Use<TService>(
            this ChannelFactory<TService> channelFactory,
            string uri,
            Action<TService> codeBlock)
        {
            Use(channelFactory, new EndpointAddress(uri), codeBlock);
        }

        /// <summary>
        /// Use channel factory by creating channel and using it within a code block.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="channelFactory">The channel factory.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="codeBlock">The code block.</param>
        /// <returns></returns>
        public static TResult Use<TService, TResult>(
            this ChannelFactory<TService>
            channelFactory,
            EndpointAddress endpoint,
            Func<TService, TResult> codeBlock)
        {
            var proxy = (IClientChannel)channelFactory.CreateChannel(endpoint);
            var success = false;

            try
            {
                TResult result = codeBlock((TService)proxy);
                proxy.Close();
                success = true;
                return result;
            }
            finally
            {
                if (!success && proxy != null)
                {
                    try
                    {
                        proxy.Abort();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Use channel factory by creating channel and using it within a code block.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="channelFactory">The channel factory.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="codeBlock">The code block.</param>
        /// <returns></returns>
        public static TResult Use<TService, TResult>(
            this ChannelFactory<TService> channelFactory,
            string uri,
            Func<TService, TResult> codeBlock)
        {
            return Use(channelFactory, new EndpointAddress(uri), codeBlock);
        }
    }
}
