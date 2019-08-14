//-----------------------------------------------------------------------
// <copyright file="Common.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Renci.SshNet;
    using SSHConnector.Model;

    /// <summary>
    /// Common re-usable methods
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Get response, from command line output
        /// </summary>
        /// <param name="input">string which is received from command execution</param>
        /// <returns>returns the array of string</returns>
        public static IEnumerable<string[]> GetResponse(string input)
        {
            var lines = input.Split(new char[] { '\r', '\n' }).Skip(1).ToList();
            return lines.Where(l => l.Trim() != string.Empty).Select(l => l.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// Get response string from based on command
        /// </summary>
        /// <param name="request">will contains host,port,userName,password</param>
        /// <param name="command">command to execute</param>
        /// <returns>command result</returns>
        public static string GetCommandResponse(BaseRequest request, string command)
        {
            var result = string.Empty;

            ////Set up the SSH connection
            using (var client = new SshClient(request.Host, request.Port, request.UserName, request.Password))
            {
                ////Start the connection
                client.Connect();
                var response = client.RunCommand(command);
                client.Disconnect();
                result = response.Result;
            }

            return result;
        }
    }
}
