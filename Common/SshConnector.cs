//-----------------------------------------------------------------------
// <copyright file="SshConnector.cs" company="Weir Group PLC">
//    Copyright (c) Weir Group PLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Renci.SshNet;
    using SSHConnector.Model;

    /// <summary>
    /// SshConnector re-usable methods
    /// </summary>
    public static class SshConnector
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
        public static async Task<string> GetCommandResponse(RequestBase request, string command)
        {
            string result = string.Empty;

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
