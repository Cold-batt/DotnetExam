using Itis.DotnetExam.Api.Contracts.Enums;

namespace Itis.DotnetExam.Api.Contracts.Requests.Chat;

public class ResponseMessage
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// Чат
    /// </summary>
    public Guid ChatId { get; set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public string UserName { get; set; }

    public MessageTypes MessageType { get; set; }
}