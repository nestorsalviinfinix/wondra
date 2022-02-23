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
    private ChessBoard chessBoard;

    public Material[] boxesMaterials;
    public LiveBox boxPrefab;

    public LiveBox SelectedBox { get; set; }

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
                box.Init();
            }
    }

    public void Init(ChessBoard data)
    {
        chessBoard = data;

        boardWidth = chessBoard.sizeWidth;
        boardHeight = chessBoard.sizeHeight;

        boxWidth = boxPrefab.GetComponent<BoxCollider>().size.x * 2;
        boxHeight = boxPrefab.GetComponent<BoxCollider>().size.z * 2;
    }
}
