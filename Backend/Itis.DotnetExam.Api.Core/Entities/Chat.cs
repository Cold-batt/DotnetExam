using Itis.DotnetExam.Api.Core.Exceptions;

namespace Itis.DotnetExam.Api.Core.Entities;

/// <summary>
/// Чат
/// </summary>
public class Chat : EntityBase
{
    private Game _game;

    /// <summary>
    /// Конструктор
    /// </summary>
    public Chat()
    {
        Messages = new List<Message>();
    }

    public Guid GameId { get; set; }

    public Game Game
    {
        get => _game;
        set
        {
            GameId = value?.Id
                ?? throw new RequiredFieldIsEmpty("Игра");
            _game = value;
        }

    }
    
    /// <summary>
    /// Сообщения
    /// </summary>
    public List<Message> Messages { get; set; }
}