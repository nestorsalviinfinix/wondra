using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveGameController : MonoBehaviour
{
    private ChessGameController gameController;
    public LiveBoardController liveBoard;
    public LivePieceController pieceController;

    void Start()
    {
        // creates game
        gameController = new ChessGameController();

        // set board data on live board
        liveBoard.Init(gameController.Board);

        Action.Init();

        // create live board boxes
        liveBoard.CreateBoxMatrix();

        pieceController.Init(gameController);
        pieceController.CreatePieces();
    }

}
