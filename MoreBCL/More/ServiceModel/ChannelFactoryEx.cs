namespace More.ServiceModel
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography.X509Certificates;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Configuration;
    using System.ServiceModel.Description;

    /// <summary>
    /// A custom channel factory that can use arbitrary config files 
    /// other than app.config and web.config.
    /// </summary>
    /// <typeparam name="T">Interface of service.</typeparam>
    public class ChannelFactoryEx<T> : ChannelFactory<T>
    {
        private const string ConfigFileExtension = ".config";
        private readonly string configurationPath;
        private readonly string endpointConfigurationName;
        private readonly EndpointAddress remoteAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelFactoryEx{T}"/> class.
        /// </summary>
        /// <param name="configurationPath">The configuration path.</param>
        public ChannelFactoryEx(string configurationPath)
            : base(typeof(T))
        {
            this.configurationPath = configurationPath;
            base.InitializeEndpoint((string)null, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelFactoryEx{T}"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="configurationPath">The configuration path.</param>
        public ChannelFactoryEx(string endpointConfigurationName, string configurationPath)
            : base(typeof(T))
        {
            this.configurationPath = configurationPath;
            this.endpointConfigurationName = endpointConfigurationName;
            base.InitializeEndpoint((string)null, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelFactoryEx{T}"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        /// <param name="configurationPath">The configuration path.</param>
        public ChannelFactoryEx(
            string endpointConfigurationName,
            EndpointAddress remoteAddress,
            string configurationPath)
            : base(typeof(T))
        {
            this.configurationPath = configurationPath;
            this.endpointConfigurationName = endpointConfigurationName;
            this.remoteAddress = remoteAddress;
            base.InitializeEndpoint((string)null, null);
        }

        /// <summary>
        /// Creates the channel from assembly config.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static ChannelFactory<T> CreateChannelFromAssemblyConfig(
            Assembly assembly)
        {
            return new ChannelFactoryEx<T>(
                assembly.Location + ConfigFileExtension);
        }

        /// <summary>
        /// Creates the channel from assembly config.
        /// </summary>
        /// <param name="configurationPath">The configuration path.</param>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <returns></returns>
        public static ChannelFactory<T> CreateChannelFromConfig(
            string configurationPath,
            string endpointConfigurationName)
        {
            return new ChannelFactoryEx<T>(
                endpointConfigurationName,
                configurationPath);
        }

        /// <summary>
        /// Creates the channel from assembly config.
        /// </summary>
        /// <param name="configurationPath">The configuration path.</param>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        /// <returns></returns>
        public static ChannelFactory<T> CreateChannelFromConfig(
            string configurationPath,
            string endpointConfigurationName,
            EndpointAddress remoteAddress)
        {
            return new ChannelFactoryEx<T>(
                endpointConfigurationName,
                remoteAddress,
                configurationPath);
        }

        /// <summary>
        /// Creates the channel from assembly config.
        /// </summary>
        /// <param name="configurationPath">The configuration path.</param>
        /// <returns></returns>
        public static ChannelFactory<T> CreateChannelFromConfig(
            string configurationPath)
        {
            return new ChannelFactoryEx<T>(
                configurationPath);
        }

        /// <summary>
        /// Creates the channel from assembly config.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <returns></returns>
        public static ChannelFactory<T> CreateChannelFromAssemblyConfig(
            Assembly assembly,
            string endpointConfigurationName)
        {
            return new ChannelFactoryEx<T>(
                endpointConfigurationName,
                assembly.Location + ConfigFileExtension);
        }

        /// <summary>
        /// Creates the channel from assembly config.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        /// <returns></returns>
        public static ChannelFactory<T> CreateChannelFromAssemblyConfig(
            Assembly assembly,
            string endpointConfigurationName,
            EndpointAddress remoteAddress)
        {
            return new ChannelFactoryEx<T>(
                endpointConfigurationName,
                remoteAddress,
                assembly.Location + ConfigFileExtension);
        }

        /// <summary>
        /// overrides the CreateDescription() method of the channel factory
        /// to apply a new configuration file
        /// </summary>
        /// <returns></returns>
        protected override ServiceEndpoint CreateDescription()
        {
            ServiceEndpoint serviceEndpoint = base.CreateDescription();

            var executionFileMap = new ExeConfigurationFileMap();
            executionFileMap.ExeConfigFilename = this.configurationPath;

            System.Configuration.Configuration config =
                ConfigurationManager.OpenMappedExeConfiguration(executionFileMap, ConfigurationUserLevel.None);

            ServiceModelSectionGroup serviceModeGroup =
                ServiceModelSectionGroup.GetSectionGroup(config);

            ChannelEndpointElement selectedEndpoint = null;

            if (this.endpointConfigurationName != null)
            {
                foreach (ChannelEndpointElement endpoint in serviceModeGroup.Client.Endpoints.Cast<ChannelEndpointElement>()
                    .Where(endpoint => endpoint.Name == this.endpointConfigurationName))
                {
                    selectedEndpoint = endpoint;
                    break;
                }
            }

            if (selectedEndpoint == null)
            {
                foreach (ChannelEndpointElement endpoint in serviceModeGroup.Client.Endpoints.Cast<ChannelEndpointElement>()
                    .Where(endpoint => endpoint.Contract == serviceEndpoint.Contract.ConfigurationName))
                {
                    selectedEndpoint = endpoint;
                    break;
                }
            }

            if (selectedEndpoint != null)
            {
                if (serviceEndpoint.Binding == null)
                {
                    serviceEndpoint.Binding = CreateBinding(selectedEndpoint.Binding, serviceModeGroup);
                }

                if (this.remoteAddress == null)
                {
                    if (serviceEndpoint.Address == null)
                    {
                        serviceEndpoint.Address = new EndpointAddress(
                            selectedEndpoint.Address,
                            GetIdentity(selectedEndpoint.Identity),
                            selectedEndpoint.Headers.Headers);
                    }
                }
                else
                {
                    serviceEndpoint.Address = this.remoteAddress;
                }

                if (serviceEndpoint.Behaviors.Count == 0 &&
                    !string.IsNullOrEmpty(selectedEndpoint.BehaviorConfiguration))
                {
                    AddBehaviors(selectedEndpoint.BehaviorConfiguration, serviceEndpoint, serviceModeGroup);
                }

                serviceEndpoint.Name = selectedEndpoint.Contract;
            }

            return serviceEndpoint;
        }

        /// <summary>
        /// Configures the binding for the selected endpoint
        /// </summary>
        /// <param name="bindingName">Name of the binding.</param>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        private Binding CreateBinding(string bindingName, ServiceModelSectionGroup group)
        {
            BindingCollectionElement bindingElementCollection = group.Bindings[bindingName];

            if (bindingElementCollection.ConfiguredBindings.Count > 0)
            {
                IBindingConfigurationElement be = bindingElementCollection.ConfiguredBindings[0];

                Binding binding = GetBinding(be);

                if (be != null)
                {
                    be.ApplyConfiguration(binding);
                }

                return binding;
            }
            else
            {
                return Activator.CreateInstance(bindingElementCollection.BindingType) as Binding;
            }
        }

        /// <summary>
        /// Adds the configured behavior to the selected endpoint
        /// </summary>
        /// <param name="behaviorConfiguration">The behavior configuration.</param>
        /// <param name="serviceEndpoint">The service endpoint.</param>
        /// <param name="group">The group.</param>
        private void AddBehaviors(
            string behaviorConfiguration,
            ServiceEndpoint serviceEndpoint,
            ServiceModelSectionGroup group)
        {
            EndpointBehaviorElement behaviorElement =
                group.Behaviors.EndpointBehaviors[behaviorConfiguration];

            for (int i = 0; i < behaviorElement.Count; i++)
            {
                BehaviorExtensionElement behaviorExtension = behaviorElement[i];

                object extension = behaviorExtension.GetType().InvokeMember(
                    "CreateBehavior",
                    BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    behaviorExtension,
                    null);

                if (extension != null)
                {
                    serviceEndpoint.Behaviors.Add((IEndpointBehavior)extension);
                }
            }
        }

        /// <summary>
        /// Gets the endpoint identity from the configuration file
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private EndpointIdentity GetIdentity(IdentityElement element)
        {
            EndpointIdentity identity = null;
            PropertyInformationCollection properties = element.ElementInformation.Properties;

            if (properties["userPrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateUpnIdentity(element.UserPrincipalName.Value);
            }

            if (properties["servicePrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateSpnIdentity(element.ServicePrincipalName.Value);
            }

            if (properties["dns"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateDnsIdentity(element.Dns.Value);
            }

            if (properties["rsa"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateRsaIdentity(element.Rsa.Value);
            }

            if (properties["certificate"].ValueOrigin != PropertyValueOrigin.Default)
            {
                X509Certificate2Collection supportingCertificates = new X509Certificate2Collection();
                supportingCertificates.Import(Convert.FromBase64String(element.Certificate.EncodedValue));

                if (supportingCertificates.Count == 0)
                {
                    throw new InvalidOperationException("UnableToLoadCertificateIdentity");
                }

                X509Certificate2 primaryCertificate = supportingCertificates[0];
                supportingCertificates.RemoveAt(0);
                return EndpointIdentity.CreateX509CertificateIdentity(primaryCertificate, supportingCertificates);
            }

            return identity;
        }

        /// <summary>
        /// Helper method to create the right binding depending on the configuration element
        /// </summary>
        /// <param name="configurationElement">The configuration element.</param>
        /// <returns></returns>
        private Binding GetBinding(IBindingConfigurationElement configurationElement)
        {
            if (configurationElement is CustomBindingElement)
            {
                return new CustomBinding();
            }
            else if (configurationElement is BasicHttpBindingElement)
            {
                return new BasicHttpBinding();
            }
            else if (configurationElement is NetMsmqBindingElement)
            {
                return new NetMsmqBinding();
            }
            else if (configurationElement is NetNamedPipeBindingElement)
            {
                return new NetNamedPipeBinding();
            }
            else if (configurationElement is NetPeerTcpBindingElement)
            {
                return new NetPeerTcpBinding();
            }
            else if (configurationElement is NetTcpBindingElement)
            {
                return new NetTcpBinding();
            }
            else if (configurationElement is WSDualHttpBindingElement)
            {
                return new WSDualHttpBinding();
            }
            else if (configurationElement is WSHttpBindingElement)
            {
                return new WSHttpBinding();
            }
            else if (configurationElement is WSFederationHttpBindingElement)
            {
                return new WSFederationHttpBinding();
            }

            return null;
        }
    }
}