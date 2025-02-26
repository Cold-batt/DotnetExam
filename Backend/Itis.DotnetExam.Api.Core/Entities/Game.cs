﻿using Aspose.Html.Rendering.Pdf;
using Itis.DotnetExam.Api.Contracts.Enums;
using Itis.DotnetExam.Api.Core.Exceptions;

namespace Itis.DotnetExam.Api.Core.Entities;

/// <summary>
/// Игровое лобби
/// </summary>
public class Game
{
    private User? _owner;
    private User? _opponent;
    private Chat _chat;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="owner">Хост</param>
    /// <param name="maxRate">Максимальный рейтинг</param>
    /// <param name="gameState">Статус игры</param>
    /// <param name="gameMap">Карта игры</param>
    public Game(
        Guid ownerId,
        int maxRate,
        GameState gameState,
        MapMarkers[] gameMap)
    {
        OwnerId = ownerId;
        MaxRate = maxRate;
        GameState = gameState;
        GameMap = gameMap;
    }

    private Game()
    {
    }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор хоста
    /// </summary>
    public Guid OwnerId { get; private set; }

    /// <summary>
    /// Идентификатор соперника
    /// </summary>
    public Guid? OpponentId { get; set; }

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
	
    /// <summary>
    /// Максимальный возможный рейтинг
    /// </summary>
    public int MaxRate { get; set; }
	
    /// <summary>
    /// Статус игры
    /// </summary>
    public GameState GameState { get; set; }
	
    /// <summary>
    /// Карта игры
    /// </summary>
    public MapMarkers[] GameMap { get; set; }

    #region Navigation properties

    /// <summary>
    /// Хост
    /// </summary>
    public User? Owner
    {
        get => _owner;
        set
        {
            OwnerId = value?.Id 
                ?? throw new RequiredFieldIsEmpty("Идентификатор объекта");
            _owner = value;
        }
    }

    /// <summary>
    /// Соперник
    /// </summary>
    public User? Opponent 
    {         
        get => _opponent;
        set
        {
            OpponentId = value?.Id;
            _opponent = value;
        } 
    }

    #endregion
}