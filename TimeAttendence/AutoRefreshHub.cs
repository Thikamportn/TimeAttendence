using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendence
{
    public class AutoRefreshHub : Hub
    {
        public void RefreshToken(string token)
        {
            Clients.All.refreshToken(token);
        }
    }
}