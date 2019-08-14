//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Main Program 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args">array of arguments</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create web host builder
        /// </summary>
        /// <param name="args">array of arguments</param>
        /// <returns>Web host builder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
