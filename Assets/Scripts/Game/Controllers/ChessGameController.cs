using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameController
{
    public ChessPlayerController playerController;
    public TurnController turnController;
    public ChessBoard Board { get; private set; }

    public ChessGameController()
    {
        playerController = new ChessPlayerController();
        CreatePlayers();

        Board = new ChessBoard();
        Board.StandardFill(playerController.playerList.ToArray());
    }
    

    public void StartChessGame()
    {
        turnController.NextTurn();
    }

    public void CreatePlayers()
    {
        playerController.AddPlayer(new ChessPlayer("Diana", EChessColor.White));
        playerController.AddPlayer(new ChessPlayer("Ronnie", EChessColor.Black));
    }

}
