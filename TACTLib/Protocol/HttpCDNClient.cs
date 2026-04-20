using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using TACTLib.Client;

namespace TACTLib.Protocol {
    public class HttpCDNClient : ICDNClient
    {
        private static readonly HttpClientHandler s_httpClientHandler = new HttpClientHandler
        {
            // unlikely to be supported by cdn but...
            AutomaticDecompression = DecompressionMethods.All
        };

        private readonly HttpClient m_httpClient;
        private string[] m_cdnHosts = [];
        private string m_cdnPath = "";
        
        private bool m_log = true;

        public HttpCDNClient(HttpClient? httpClient)
        {
            m_httpClient = httpClient ?? new HttpClient(s_httpClientHandler);
        }

        public virtual void SetClientHandler(ClientHandler handler)
        {
            var hosts = handler.InstallationInfo.Values["CDNHosts"].Split(' ');
            var path = handler.InstallationInfo.Values["CDNPath"];
            
            Configure(hosts, path);
        }

        public void Configure(IEnumerable<string> cdnHosts, string cdnPath)
        {
            m_cdnHosts = cdnHosts.Select(host =>
            {
                if (host.StartsWith("http", StringComparison.InvariantCultureIgnoreCase)) return host;
                return $"http://{host}";
            }).ToArray();

            m_cdnPath = cdnPath;
        }

        public void ConfigureLogging(bool log)
        {
            m_log = log;
        }
        
        public virtual byte[]? Fetch(string type, string key, Range? range=null, string? suffix=null)
        {
            key = key.ToLowerInvariant();
            
            foreach (var host in m_cdnHosts)
            {
                var url = $"{host}/{m_cdnPath}/{type}/{key.AsSpan(0, 2)}/{key.AsSpan(2, 2)}/{key}{suffix}";
                if (m_log) Logger.Info("CDN", $"Fetching file {url}");

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                if (range != null)
                {
                    requestMessage.Headers.Range = new RangeHeaderValue(range.Value.Start.Value, range.Value.End.Value);
                }
                
                try 
                {
                    using var response = m_httpClient.Send(requestMessage, HttpCompletionOption.ResponseHeadersRead);
                    if (!response.IsSuccessStatusCode)
                    {
                        continue;
                    }
                    
                    var result = response.Content.ReadAsByteArrayAsync().Result; // todo: async over sync
                    return result;
                } catch (Exception e) {
                    // ignored
                    Logger.Debug("CDN", $"Error fetching {url}: {e}");
                }
            }

            return null;
        }
    }
}
