using ECode.Checksums;
using ECode.Core;
using ECode.Data;

namespace Sample1
{
    public class ShardStrategy : IShardStrategy
    {
        public string GetDbShardNo(object shardObject)
        {
            var crc = new Crc32_IEEE();
            crc.Update(shardObject.ToString().Trim().ToLower().ToBytes());

            if (crc.Value % 2 == 0)
            {
                return "shard-1";
            }
            else
            {
                return "shard-2";
            }
        }

        public string GetTablePartitionNo(string tableName, object shardObject, object partitionObject)
        {
            return string.Empty;
        }

        public string GetTableShardNo(string tableName, object shardObject)
        {
            return string.Empty;
        }
    }
}
