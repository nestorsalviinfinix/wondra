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
        Board = new ChessBoard();
        Board.StandardFill();

        playerController = new ChessPlayerController();
        CreatePlayers();
        Debug.Log("GAME CONTROLLER CREATED");
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