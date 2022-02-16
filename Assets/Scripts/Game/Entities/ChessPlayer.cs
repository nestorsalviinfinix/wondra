using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPlayer
{
    public ChessPlayer(string playerName, EChessColor color)
    {
        this.PlayerName = playerName;
        this.Color = color;
    }

    public string PlayerName { get; private set; }
    public EChessColor Color { get; private set; }
}
