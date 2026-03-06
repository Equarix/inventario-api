using System;
using System.Security.Claims;
using invetario_api.database;
using invetario_api.Modules.chat.entity;
using invetario_api.Websocket.chat.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace invetario_api.Websocket.chat;

[Authorize]
public class ChatWS : Hub
{
    private int? GetUserId() =>
    int.TryParse(
        Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        out var id
    ) ? id : null;

    private Database _db;

    public ChatWS(Database db)
    {
        _db = db;
    }

    public async Task JoinRoom(string room)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, room);
    }

    public async Task LeaveRoom(string room)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
    }

    public async Task SendMessage(MessageDto dto)
    {
        var userId = GetUserId();
        if (userId == null) return;

        var findUser = await _db.users.Where((u) => u.userId == userId).FirstOrDefaultAsync();

        if (findUser == null) return;

        var newMessage = new ChatMessage
        {
            message = dto.content,
            userId = userId.Value,
            storeId = int.Parse(dto.room),
            user = findUser
        };

        _db.chatMessages.Add(newMessage);
        await _db.SaveChangesAsync();

        await Clients.OthersInGroup(dto.room).SendAsync("ReceiveMessage", newMessage);
    }

}
