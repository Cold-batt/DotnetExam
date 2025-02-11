using Itis.DotnetExam.Api.Core.Exceptions;

namespace Itis.DotnetExam.Api.Core.Entities;

/// <summary>
/// Сообщение
/// </summary>
public class Message : EntityBase
{
    private Chat _chat;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="text">Текст сообщения</param>
    /// <param name="createdAt">Дата и время создания</param>
    /// <param name="chat">Чат</param>
    public Message(string text, DateTime createdAt, Chat chat)
    {
        Text = text;
        Chat = chat;
        CreatedAt = createdAt;
    }

    private Message()
    {
        
    }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; }

    public string UserName { get; set; }

    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Чат
    /// </summary>
    public Guid ChatId { get; set; }

    /// <summary>
    /// Чат
    /// </summary>
    public Chat Chat
    {
        get => _chat;
        set
        {
            ChatId = value?.Id
                ?? throw new RequiredFieldIsEmpty("Чат");
            _chat = value;
        }
    }
}