using System.Configuration;

namespace Open.Core.Web.Config
{
    /// <summary>The primary configuration section for specifying 'Open.Core.Web' settings.</summary>
    /// <example>
    /// 
    ///   <configSections>
    ///     <section name="Core" type="Open.Core.Web.Config.WebConfig" />
    ///   </configSections>
    /// 
    ///   <Core>
    ///     <ServerProxies>
    ///       <add id="myId" url="http://foo.com" localHost="localhost:1234" />
    ///     </ServerProxies>
    ///   </Core>
    /// 
    /// </example>
    public class WebConfig : ConfigurationSection
    {
        private const string PropServerProxies = "ServerProxies";

        /// <summary>Collection of nodes that represent Server-Proxies (pass through's used to route web-requests to another server for processing).</summary>
        [ConfigurationProperty(PropServerProxies)]
        public ServerProxyCollection ServerProxies { get { return (ServerProxyCollection)base[PropServerProxies]; } }
    }
}
