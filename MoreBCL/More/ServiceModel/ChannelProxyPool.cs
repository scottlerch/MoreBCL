namespace More.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// WCF channel proxy pool that manages multiple channel proxy
    /// connections per URI.  It will auto-recreate a proxy channel
    /// when a Faulted event occurs.
    /// </summary>
    /// <typeparam name="T">Proxy channel interface.</typeparam>
    public class ChannelProxyPool<T> : IDisposable where T : class
    {
        private readonly object syncRoot = new object();

        private readonly int maximumProxiesPerUri;

        private ChannelFactory<T> channelFactory;

        private TimeSpan recreateProxyWaitTimeout;

        private Dictionary<string, List<T>> proxyCache = new Dictionary<string, List<T>>();
        private Dictionary<string, int> cacheIndex = new Dictionary<string, int>();

        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelProxyPool{T}"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="maximumProxiesPerUri">The maximum proxies per URI.</param>
        /// <param name="recreateProxyWaitTimeout">The recreate proxy wait timeout.</param>
        public ChannelProxyPool(string endpointConfigurationName, int maximumProxiesPerUri, TimeSpan recreateProxyWaitTimeout)
        {
            this.recreateProxyWaitTimeout = recreateProxyWaitTimeout;
            this.maximumProxiesPerUri = maximumProxiesPerUri;
            this.channelFactory = new ChannelFactory<T>(endpointConfigurationName);
        }

        /// <summary>
        /// Gets the maximum proxies per URI.
        /// </summary>
        public int MaximumProxiesPerUri
        {
            get { return this.maximumProxiesPerUri; }
        }

        /// <summary>
        /// Gets or sets the recreate proxy wait timeout.
        /// </summary>
        public TimeSpan RecreateProxyWaitTimeout
        {
            get
            {
                lock (this.syncRoot)
                {
                    return this.recreateProxyWaitTimeout;
                }
            }

            set
            {
                lock (this.syncRoot)
                {
                    this.recreateProxyWaitTimeout = value;
                }
            }
        }

        /// <summary>
        /// Adds the specified URI.
        /// This will open the maximum number of channel proxies per URI
        /// for the specified URI.  The open is done asynchronously
        /// so this will return immediately.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <exception>
        /// Argument cannot be null.
        ///     <cref>ArugmentNullException</cref>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Object has been disposed.
        /// </exception>
        public void Add(string uri)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            lock (this.syncRoot)
            {
                if (!this.proxyCache.ContainsKey(uri))
                {
                    var proxyList = new List<T>(this.maximumProxiesPerUri);

                    this.proxyCache.Add(uri, proxyList);
                    this.cacheIndex.Add(uri, 0);

                    for (var i = 0; i < this.maximumProxiesPerUri; i++)
                    {
                        var proxy = this.channelFactory.CreateChannel(new EndpointAddress(uri));
                        var proxyClient = proxy as IClientChannel;

                        proxyClient.Faulted += this.OnChannelFaulted;

                        proxyList.Add(proxy);

                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                proxyClient.Open();
                            }
                            catch (Exception)
                            {
                                proxyClient.Abort();
                            }
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Removes the pool for the specified URI.
        /// This will close any channel proxies asynchronously,
        /// so this call will return immediately.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <exception>
        /// Argument cannot be null.
        ///     <cref>ArugmentNullException</cref>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Object has been disposed.
        /// </exception>
        public void Remove(string uri)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            lock (this.syncRoot)
            {
                if (this.proxyCache.ContainsKey(uri))
                {
                    List<T> proxyList = this.proxyCache[uri];

                    this.proxyCache.Remove(uri);
                    this.cacheIndex.Remove(uri);

                    foreach (IClientChannel proxy in proxyList.Cast<IClientChannel>())
                    {
                        IClientChannel currentProxy = proxy;
                        currentProxy.Faulted -= this.OnChannelFaulted;

                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                currentProxy.Close();
                            }
                            catch (Exception)
                            {
                                currentProxy.Abort();
                            }
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Gets a channel proxy from the pool for the specified URI.
        /// There is no guarantee what state the channel will be in,
        /// it's a best effort.  If the specified URI does not exist
        /// in the pool this will return null.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>Channel proxy instance.</returns>
        /// <exception>
        /// Argument cannot be null.
        ///     <cref>ArugmentNullException</cref>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Object has been disposed.
        /// </exception>
        public T GetProxy(string uri)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            lock (this.syncRoot)
            {
                if (this.proxyCache.ContainsKey(uri))
                {
                    // Get current cache index
                    int index = this.cacheIndex[uri];

                    // Increment modulo max so we get round-robin
                    this.cacheIndex[uri] = (this.cacheIndex[uri] + 1) % this.maximumProxiesPerUri;

                    return this.proxyCache[uri][index];
                }

                return null;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release 
        /// both managed and unmanaged resources; <c>false</c> to
        /// release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            lock (this.syncRoot)
            {
                if (!this.disposed)
                {
                    foreach (var currentProxy in this.proxyCache.SelectMany(cache => cache.Value.Cast<IClientChannel>()))
                    {
                        currentProxy.Faulted -= this.OnChannelFaulted;

                        var proxy = currentProxy;

                        Task.Factory.StartNew(() =>
                            {
                                try
                                {
                                    proxy.Close();
                                }
                                catch (Exception)
                                {
                                    proxy.Abort();
                                }
                            });
                    }

                    this.proxyCache = null;
                    this.cacheIndex = null;
                    this.channelFactory = null;

                    this.disposed = true;
                }
            }
        }

        /// <summary>
        /// Called when channel faulted.
        /// This will attempt to clean up the faulted channel proxy,
        /// create a new channel proxy, and update the cache.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnChannelFaulted(object sender, EventArgs e)
        {
            var faultedProxy = sender as IClientChannel;
            string uri = faultedProxy.RemoteAddress.Uri.OriginalString;

            faultedProxy.Faulted -= this.OnChannelFaulted;
            faultedProxy.Abort();

            var newProxy = this.channelFactory.CreateChannel(new EndpointAddress(uri));
            var newProxyClient = newProxy as IClientChannel;
            newProxyClient.Faulted += this.OnChannelFaulted;

            lock (this.syncRoot)
            {
                // Find index of faulted proxy in cache list so we can replace it
                var index = this.proxyCache[uri].IndexOf(faultedProxy as T);
                this.proxyCache[uri][index] = newProxy;

                Monitor.Wait(this.syncRoot, this.RecreateProxyWaitTimeout);
            }

            Task.Factory.StartNew(() =>
            {
                try
                {
                    newProxyClient.Open();
                }
                catch (Exception)
                {
                    newProxyClient.Abort();
                }
            });
        }
    }
}
