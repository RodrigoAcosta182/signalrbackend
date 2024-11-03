using Microsoft.AspNetCore.SignalR;

public class AuthHub : Hub
{
    private static HashSet<string> ConnectedUsers = new HashSet<string>();

    public async Task LoginUser(string username)
    {
        ConnectedUsers.Add(username);
        await Clients.All.SendAsync("UpdateUserList", ConnectedUsers);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = Context.User?.Identity?.Name;
        if (user != null)
        {
            ConnectedUsers.Remove(user);
            await Clients.All.SendAsync("UpdateUserList", ConnectedUsers);
        }
        await base.OnDisconnectedAsync(exception);
    }
    public async Task GetConnectedUsers()
    {
        await Clients.Caller.SendAsync("UpdateUserList", ConnectedUsers);
    }

    public async Task LogoutUser(string username)
    {
        if (ConnectedUsers.Contains(username))
        {
            ConnectedUsers.Remove(username);
            await Clients.All.SendAsync("UpdateUserList", ConnectedUsers);
        }
    }

}
