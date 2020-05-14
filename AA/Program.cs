using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Redis
    {
        private static readonly string redisConn = ConfigurationManager.ConnectionStrings["redis"].ToString();

        #region 常用的数据库
        public static IDatabase Top { get; } = GetDb(0);
        //public static IDatabase TopOauth { get; } = GetDb(1);
        //public static IDatabase TopService { get; } = GetDb(2);
        #endregion
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="dbnumber"></param>
        /// <returns></returns>
        public static IDatabase GetDatabase(int dbnumber)
        {
            ConnectionMultiplexer server = ConnectionMultiplexer.Connect(redisConn);
            return server.GetDatabase(dbnumber);
        }
    }

    /// <summary>
    /// Redis缓存扩展：保存、获取、删除（不过其实好像没有用到，引用的框架了好像已经带有这个方法了）
    /// </summary>
    public static class RedisExt
    {
        /// <summary>
        /// 保存一个对象（string类型）
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="experation"></param>
        /// <returns></returns>
        public static async Task<bool> SetAsync(this IDatabaseAsync cache, string key, object value, TimeSpan experation)
        {
            return await cache.StringSetAsync(key, JsonConvert.SerializeObject(value), experation);
        }

        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetTAsync<T>(this IDatabaseAsync cache, string key)
        {
            var value = await cache.StringGetAsync(key).ConfigureAwait(false);
            if (!value.IsNull)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<bool> DelAsync(this IDatabaseAsync cache, string key)
        {
            return await cache.KeyDeleteAsync(key).ConfigureAwait(false);
        }
    }
}
