using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveGameController : MonoBehaviour
{
    private ChessGameController gameController;
    public LiveBoardController liveBoard;
    public LivePieceController pieceController;
    public BattleController battleController;

    public Camera battleCamera, boardCamera;


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
        InitBattle(false);
    }


    public void InitBattle(bool b)
    {
        if(b)
        {
            boardCamera.gameObject.SetActive(false);
            battleCamera.gameObject.SetActive(true);
        }
        else
        {
            boardCamera.gameObject.SetActive(true);
            battleCamera.gameObject.SetActive(false);
        }
    }

}
