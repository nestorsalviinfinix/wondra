using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameController
{
    public ChessPlayerController playerController;
    public ChessTurnController turnController;
    public ChessBoard Board { get; private set; }

    public ChessGameController()
    {
        playerController = new ChessPlayerController();
        playerController.CreateTestingPlayers();

        turnController = new ChessTurnController();
        turnController.Init();

        Board = new ChessBoard();
        Board.StandardFill(playerController.playerList.ToArray());
    }
    

    public void StartChessGame()
    {
        turnController.NextTurn();
    }

}
