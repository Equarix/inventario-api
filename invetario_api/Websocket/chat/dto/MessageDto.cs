using System;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Websocket.chat.dto;

public class MessageDto
{
    [Required]
    public string content { get; set; }

    [Required]
    public string room { get; set; }
}
