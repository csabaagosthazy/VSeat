using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Console_DataAccess
{
    class Program
    {
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
