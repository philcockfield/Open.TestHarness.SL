using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using Open.Core.Common;

namespace Open.Core.Web.Config
{
    /// <summary>Defines the settings for a server-proxy (used for passing through web requests from a browser client to another server).</summary>
    public class ServerProxyElement : ConfigurationElement
    {
        #region Head
        private const string PropId = "id";
        private const string PropUrl = "url";
        private const string PropLocalHost = "localHost";
        #endregion

        #region Properties
        /// <summary>Gets the unique identifier of the server proxy.</summary>
        [ConfigurationProperty(PropId, IsRequired = true, IsKey = true)]
        public string Id
        {
            get { return (string)base[PropId]; }
            set { base[PropId] = value; }
        }

        /// <summary>Gets the URL to route requests to.</summary>
        [ConfigurationProperty(PropUrl, IsRequired = false)]
        public string Url
        {
            get { return (string)base[PropUrl]; }
            set { base[PropUrl] = value; }
        }

        /// <summary>Gets or sets the local-host development server to use when in development.</summary>
        [ConfigurationProperty(PropLocalHost, IsRequired = false)]
        public string LocalHost
        {
            get { return (string)base[PropLocalHost]; }
            set { base[PropLocalHost] = value; }
        }
        #endregion

        #region Methods
        /// <summary>Converts the config element to a server-proxy object.</summary>
        public ServerProxy ToProxy()
        {
            return new ServerProxy(Id, Url, LocalHost);
        }
        #endregion
    }

    /// <summary>The configuration collection of [ServerProxyElement]'s.</summary>
    public class ServerProxyCollection : ConfigurationElementCollection
    {
        #region Head
        private static List<ServerProxy> singletons;
        #endregion

        #region Properties
        public ServerProxyElement this[int pos] { get { return (ServerProxyElement)BaseGet(pos); } }
        #endregion

        #region Methods
        protected override ConfigurationElement CreateNewElement() { return new ServerProxyElement(); }
        protected override Object GetElementKey(ConfigurationElement element) { return ((ServerProxyElement)element).Id; }

        public void Add(ServerProxyElement element) { BaseAdd(element); }
        public void Add(int index, ServerProxyElement element) { BaseAdd(index, element); }
        #endregion

        #region Methods : Static
        /// <summary>Retrieves a singleton instance of the server defined in Web.Config with the specified id.</summary>
        /// <param name="id">The unique identifier of the server entry within the Web.Config.</param>
        /// <returns>The server proxy singleton, or null if the server is not declared.</returns>
        public ServerProxy GetProxyAsSingleton(string id)
        {
            // Setup initial conditions.
            if (id.IsNullOrEmpty(true)) return null;
            if (singletons == null) singletons = new List<ServerProxy>();

            // Look for an existing object.
            var proxy = singletons.FirstOrDefault(m => m.Id == id);
            if (proxy != null) return proxy;

            // Retrieve the config-element.
            ServerProxyElement configElement = this.Cast<ServerProxyElement>().FirstOrDefault(item => item.Id == id);
            if (configElement == null) return null;

            // Create and store the proxy.
            proxy = configElement.ToProxy();
            singletons.Add(proxy);

            // Finish up.
            return proxy;
        }
        #endregion
    }
}
