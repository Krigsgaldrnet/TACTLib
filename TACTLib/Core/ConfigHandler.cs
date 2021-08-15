﻿using System;
using System.IO;
using TACTLib.Client;
using TACTLib.Config;

namespace TACTLib.Core {
    public class ConfigHandler {
        /// <summary>Build config</summary>
        public readonly BuildConfig BuildConfig;

        /// <summary>CDN config</summary>
        public readonly CDNConfig CDNConfig;

        /// <summary>Keyring config</summary>
        public readonly Keyring Keyring;

        public ConfigHandler(ClientHandler client) {
            LoadFromInstallationInfo(client, "BuildKey", out BuildConfig);
            LoadFromInstallationInfo(client, "CDNKey", out CDNConfig);
            LoadFromInstallationInfo(client, "Keyring", out Keyring);

            if (Keyring == null) {
                Keyring = new Keyring(client, null);
            }
        }

        private void LoadFromInstallationInfo<T>(ClientHandler client, string name, out T @out) where T : Config.Config {
            if (client.InstallationInfo.Values.TryGetValue(name, out string key) && !string.IsNullOrWhiteSpace(key)) {
                using (Stream stream = client.OpenConfigKey(key)) {
                    LoadConfig(client, stream, out @out);
                }
            } else {
                @out = null;
            }
        }

        private static void LoadConfig<T>(ClientHandler client, Stream stream, out T @out) {
            // hmm
            @out = (T) Activator.CreateInstance(typeof(T), client, stream);
        }
    }
}
