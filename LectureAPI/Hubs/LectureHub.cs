using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureAPI.Hubs
{
    public sealed class LectureHub : Hub
    {
        public async Task Send(string value)
        {
            await Clients.All.SendAsync("NewValue", value);
        }
    }
}
