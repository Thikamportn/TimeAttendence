using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeAttendence.Utilities
{
    public static class TokenHelper
    {
        public static List<String> Tokens = new List<String>();

        public static void GenToken()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            Random rand = new Random();
            var resultToken = new String(Enumerable.Repeat(chars, 12)
                .Select(s => s[rand.Next(s.Length)])
                .ToArray());
            lock (Tokens)
            {
                Tokens.Add(resultToken);
            }
        }

        public static string GetLastToken()
        {
            if (Tokens.Count == 0) GenToken();
            return Tokens.Last();
        }

        public static void RemoveOldToken()
        {
            if(Tokens.Count > 1)
            {
                System.Threading.Thread.Sleep(10);
                lock (Tokens){
                    Tokens.RemoveAt(0);
                }
            }
        }
    }

    
  
}