using Interface.Services;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace SignalrHubs
{
    //public interface IConnectionHub
    //{
    //    Task FinishedBooking();
    //}
    //public class ConnectionHub : Hub, IConnectionHub
    //{
    //    private IUserService _userService;
    //    private readonly IHubContext<ConnectionHub> _hubContext;
    //    private static readonly Dictionary<string, HubConnection> Connections = new Dictionary<string, HubConnection>();

    //    public ConnectionHub(
    //        IHubContext<ConnectionHub> hubContext
    //        , IUserService userService
    //        )
    //    {
    //        _hubContext = hubContext;
    //        _userService = userService;
    //    }

    //    public Task FinishedBooking()
    //    {
    //        throw new NotImplementedException();
    //    }
    //    public async Task Connect(string userId)
    //    {
    //        var connection = new HubConnectionBuilder().Build();

    //        Connections.Add(userId, connection);

    //        connection.Closed += async (error) =>
    //        {
    //            Connections.Remove(userId);
    //            Console.WriteLine($"User {userId} disconnected.");

    //            /// udpate lại user
    //        };

    //        await connection.StartAsync();
    //    }
    //    }
    }
