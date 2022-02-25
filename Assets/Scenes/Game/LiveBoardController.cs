using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBoardController : MonoBehaviour
{
    private int boardWidth;
    private int boardHeight;
    public float boxWidth;
    public float boxHeight;
    public LiveBox[,] boxes;
    public ChessBoard chessBoard;

    public Material[] boxesMaterials;
    public LiveBox boxPrefab;
    public NullBox NullBox;
    public NullPiece NullPiece;

    // text reference
    //public event Action<LiveBox> OnLiveBoxSelected;
    public delegate void UpdateSelectedBox(LiveBox box);
    public static event UpdateSelectedBox OnUpdateSelectedBox;

    public delegate void UpdatePossibleMoves(List<ChessBoardBox> movesList);
    public static event UpdatePossibleMoves OnUpdatePossibleMoves;

    public LiveBox SelectedBox { get; private set; }
    public LivePiece SelectedPiece { get; set; }

    public void CreateBoxMatrix()
    {
        boxes = new LiveBox[boardWidth, boardHeight];

        for (int x = 0; x < boardWidth; x++)
            for (int y = 0; y < boardHeight; y++)
            {
                float posX = boxWidth * (2*x - boardWidth)/4;
                float posY = boxHeight * (boardHeight - 2*y)/4;
                LiveBox box = Instantiate(boxPrefab, gameObject.transform);
                box.transform.position = new Vector3(posX, 0.01f, posY);
                box.transform.parent = gameObject.transform;

                MeshRenderer render = box.GetComponentInChildren<MeshRenderer>();
                EChessColor color = chessBoard.boxes[x, y].Color;
                 
                render.material = color == EChessColor.White ? boxesMaterials[0] : boxesMaterials[1];

                box.name = $"{ChessBoard.ToAlgebraic(new Vector2(x, y))} - {chessBoard.boxes[x, y].Color}";

                boxes[x, y] = box;
                box.CoordX = x;
                box.CoordY = y;

                box.Init(this);
            }
    }

    public void Init(ChessBoard data)
    {
        LiveBox.NullBox = this.NullBox;
        this.NullBox.Init(this);
        LivePiece.NullPiece = this.NullPiece;

        SelectedBox = NullBox;
        SelectedPiece = NullPiece;

        chessBoard = data;

        boardWidth = chessBoard.sizeWidth;
        boardHeight = chessBoard.sizeHeight;

        boxWidth = boxPrefab.GetComponent<BoxCollider>().size.x * 2;
        boxHeight = boxPrefab.GetComponent<BoxCollider>().size.z * 2;

    }

    public void SelectBox(LiveBox box)
    {
        SelectedBox = box;

        // show text
        // text.setSelectedBox(LiveBox)
        //OnLiveBoxSelected(box);
        OnUpdateSelectedBox?.Invoke(box);
        List<ChessBoardBox> possibleMoves = box.piece.GetLivePossibleMoves();
        OnUpdatePossibleMoves.Invoke(possibleMoves);
    }

}
