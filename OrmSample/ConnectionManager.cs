using System;
using System.Collections.Generic;
using ECode.Data;
using ECode.Utility;

namespace Sample1
{
    public class ConnectionManager : IConnectionManager
    {
        private Dictionary<string, string>      connectionStrings   = null;


        public ConnectionManager(Dictionary<string, string> connectionStrings)
        {
            AssertUtil.ArgumentNotNull(connectionStrings, nameof(connectionStrings));

            if (connectionStrings.Count == 0)
            { throw new ArgumentException($"Argument '{nameof(connectionStrings)}' cannot be empty."); }

            this.connectionStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string shardNo in connectionStrings.Keys)
            {
                this.connectionStrings[shardNo?.Trim()] = connectionStrings[shardNo];
            }
        }


        public string GetConnectionString(string shardNo = null, bool writable = true)
        {
            shardNo = shardNo?.Trim() ?? string.Empty;

            if (connectionStrings.ContainsKey(shardNo))
            {
                return connectionStrings[shardNo];
            }

            throw new KeyNotFoundException($"Shard connection '{shardNo}' cannot be found.");
        }
    }
}
