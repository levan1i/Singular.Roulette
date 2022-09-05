using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Hubs
{
    [Authorize]
    public class BetHub : Hub
    {
        public override async Task OnConnectedAsync()
        {



        }
    }
}
