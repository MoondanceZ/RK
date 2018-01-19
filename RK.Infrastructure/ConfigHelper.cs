using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RK.Infrastructure
{
    public class ConfigHelper
    {

        public static readonly IConfigurationRoot ConfigurationBuilder = new ConfigurationBuilder()
                    .AddInMemoryCollection()    //将配置文件的数据加载到内存中
                    .SetBasePath(Directory.GetCurrentDirectory())   //指定配置文件所在的目录
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)  //指定加载的配置文件
                    .Build();    //编译成对象  

        public static string GetConnectionString(string connStr)
        {
            return ConfigurationBuilder.GetConnectionString(connStr);
        }
    }
}