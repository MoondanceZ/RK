using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Framework.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// 反序列号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string stringValue)
        {
            return JsonConvert.DeserializeObject<T>(stringValue);
        }

        /// <summary>
        /// 是否附加$type附加说明
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringValue"></param>
        /// <param name="withType"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string stringValue, bool withType)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = withType ? TypeNameHandling.All : TypeNameHandling.Auto };
            return JsonConvert.DeserializeObject<T>(stringValue, settings);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringValue"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string stringValue, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(stringValue, settings);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string Serialize(object instance)
        {
            return JsonConvert.SerializeObject(instance);
        }

        public static string Serialize(object instance, string dateTImeFormat)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = String.IsNullOrWhiteSpace(dateTImeFormat) ? "yyyy-MM-dd HH:mm:ss" : dateTImeFormat;
            return JsonConvert.SerializeObject(instance, Formatting.Indented, timeFormat);
        }

        /// <summary>
        /// 序列号
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="withType"></param>
        /// <returns></returns>
        public static string Serialize(object instance, bool withType)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = withType ? TypeNameHandling.All : TypeNameHandling.Auto };
            return JsonConvert.SerializeObject(instance, settings);
        }
    }
}
