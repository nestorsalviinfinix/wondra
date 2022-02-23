using Assets.Scenes.Game.Interfaces;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChessPlayerController : IChessPlayerManager
{
    public List<ChessPlayer> playerList = new List<ChessPlayer>();
    private int _currentPlayer = 0;

    public ChessPlayer GetFirstPlayer()
    {
        _currentPlayer++;
        return playerList.ElementAt(0);
    }
    public ChessPlayer GetNextPlayer()
    {
        _currentPlayer++;
        if (_currentPlayer >= playerList.Count - 1) _currentPlayer = 0;

        return playerList.ElementAt(_currentPlayer - 1);
    }

    public void AddPlayer(ChessPlayer player)
    {
        playerList.Add(player);
    }

    public void CreateTestingPlayers()
    {
        AddPlayer(new ChessPlayer("Diana", EChessColor.White));
        AddPlayer(new ChessPlayer("Ronnie", EChessColor.Black));
    }
}
