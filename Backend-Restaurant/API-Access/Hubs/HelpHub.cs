using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Access.Hubs
{
    public class HelpHub : Hub
    {
        private static List<HelpRequest> ActiveRequests = new();

        public async Task SendHelpRequest(string tableNumber, string message)
        {
            var helpRequest = new HelpRequest
            {
                Id = System.Guid.NewGuid(),
                TableNumber = tableNumber.ToString(),
                Message = message
            };

            ActiveRequests.Add(helpRequest);
            await Clients.All.SendAsync("HelpRequestReceived", helpRequest);
        }

        public async Task ResolveHelpRequest(System.Guid requestId)
        {
            ActiveRequests.RemoveAll(r => r.Id == requestId);
            await Clients.All.SendAsync("HelpRequestResolved", requestId);
        }

        public async Task GetActiveHelpRequests()
        {
            await Clients.Caller.SendAsync("HelpRequestsUpdated", ActiveRequests);
        }
    }

    public class HelpRequest
    {
        public System.Guid Id { get; set; }
        public string TableNumber { get; set; }
        public string Message { get; set; }
    }
}
