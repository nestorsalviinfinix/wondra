using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBoardController : MonoBehaviour
{
    private int boardWidth;
    private int boardHeight;
    public float boxWidth;
    public float boxHeight;
    private ChessBoard dataController;

    public Material[] boxesMaterials;
    public GameObject boxPrefab;

    public void CreateBoxMatrix()
    {
        for (int x = 0; x < boardWidth; x++)
            for (int y = 0; y < boardHeight; y++)
            {
                float posX = boxWidth * (2*x - boardWidth)/4;
                float posY = boxHeight * (boardHeight - 2*y)/4;
                GameObject box = Instantiate(boxPrefab, gameObject.transform);
                box.transform.position = new Vector3(posX, 0.01f, posY);
                box.transform.parent = gameObject.transform;

                MeshRenderer render = box.GetComponentInChildren<MeshRenderer>();
                EChessColor color = dataController.boxes[x, y].Color;

                render.material = color == EChessColor.White ? boxesMaterials[0] : boxesMaterials[1];

                box.name = $"{ChessBoard.ToAlgebraic(new Vector2(x, y))} - {dataController.boxes[x, y].Color}";
            }
    }

    public void SetData(ChessBoard data)
    {
        dataController = data;

        boardWidth = dataController.sizeWidth;
        boardHeight = dataController.sizeHeight;

        boxWidth = boxPrefab.GetComponent<BoxCollider>().size.x * 2;
        boxHeight = boxPrefab.GetComponent<BoxCollider>().size.z * 2;
    }
}
