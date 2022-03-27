using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public BoardBox boxModel;
    public int width = 8;
    public int hight = 8;
    public float widthDistance = .5f;
    public float hightDistance = .5f;
    public Material groundWhite;
    public Material groundBlack;

    [Header("Arena de otro costal")]
    public BoardBox[,] boardMatrix;
    public PieceGame pieceModel;


    void Start()
    {
        CreateBoard();
        CreateInitPieces();
    }
    public void CreateBoard()
    {
        float midCenterW = widthDistance * (float)(width / 2);
        float midCenterH = hightDistance * (float)(hight / 2);
        boardMatrix = new BoardBox[width, hight];

        for (int y = 0; y < hight; y++)
            for (int x = 0; x < width; x++)
            {
                float posX = -midCenterW + (x * widthDistance);
                float posY = -midCenterH + (y * hightDistance);
                BoardBox box = Instantiate(boxModel, gameObject.transform);
                box.transform.localPosition = new Vector3(posX, 0.01f, posY);

                box.SetPosition(y, x);

                MeshRenderer render = box.GetComponentInChildren<MeshRenderer>();
                render.material = box.color == EChessColor.White ? groundWhite : groundBlack;

                boardMatrix[y, x] = box;
            }

        FindObjectOfType<SelectBoard>().board = boardMatrix;
    }
    public void CreateInitPieces()
    {
        //Test
        //CreatePiece(4, 4, EChessPieceType.BISHOP, EChessColor.White);
        //Piezas Blancas
        CreatePiece(0, 0, EChessPieceType.TOWER, EChessColor.White);
        CreatePiece(0, 1, EChessPieceType.KNIGHT, EChessColor.White);
        CreatePiece(0, 2, EChessPieceType.BISHOP, EChessColor.White);
        CreatePiece(0, 3, EChessPieceType.QUEEN, EChessColor.White);
        CreatePiece(0, 4, EChessPieceType.KING, EChessColor.White);
        CreatePiece(0, 5, EChessPieceType.BISHOP, EChessColor.White);
        CreatePiece(0, 6, EChessPieceType.KNIGHT, EChessColor.White);
        CreatePiece(0, 7, EChessPieceType.TOWER, EChessColor.White);

        CreatePiece(1, 0, EChessPieceType.PAWN, EChessColor.White);
        CreatePiece(1, 1, EChessPieceType.PAWN, EChessColor.White);
        CreatePiece(1, 2, EChessPieceType.PAWN, EChessColor.White);
        CreatePiece(1, 3, EChessPieceType.PAWN, EChessColor.White);
        CreatePiece(1, 4, EChessPieceType.PAWN, EChessColor.White);
        CreatePiece(1, 5, EChessPieceType.PAWN, EChessColor.White);
        CreatePiece(1, 6, EChessPieceType.PAWN, EChessColor.White);
        CreatePiece(1, 7, EChessPieceType.PAWN, EChessColor.White);
        //Piezas Negras
        CreatePiece(7, 0, EChessPieceType.TOWER, EChessColor.Black);
        CreatePiece(7, 1, EChessPieceType.KNIGHT, EChessColor.Black);
        CreatePiece(7, 2, EChessPieceType.BISHOP, EChessColor.Black);
        CreatePiece(7, 3, EChessPieceType.QUEEN, EChessColor.Black);
        CreatePiece(7, 4, EChessPieceType.KING, EChessColor.Black);
        CreatePiece(7, 5, EChessPieceType.BISHOP, EChessColor.Black);
        CreatePiece(7, 6, EChessPieceType.KNIGHT, EChessColor.Black);
        CreatePiece(7, 7, EChessPieceType.TOWER, EChessColor.Black);

        CreatePiece(6, 0, EChessPieceType.PAWN, EChessColor.Black);
        CreatePiece(6, 1, EChessPieceType.PAWN, EChessColor.Black);
        CreatePiece(6, 2, EChessPieceType.PAWN, EChessColor.Black);
        CreatePiece(6, 3, EChessPieceType.PAWN, EChessColor.Black);
        CreatePiece(6, 4, EChessPieceType.PAWN, EChessColor.Black);
        CreatePiece(6, 5, EChessPieceType.PAWN, EChessColor.Black);
        CreatePiece(6, 6, EChessPieceType.PAWN, EChessColor.Black);
        CreatePiece(6, 7, EChessPieceType.PAWN, EChessColor.Black);
    }
    private void CreatePiece(int x, int y, EChessPieceType t, EChessColor c)
    {
        PieceGame piece = Instantiate(pieceModel, boardMatrix[x, y].pieceSpot.transform);
        boardMatrix[x, y].SetPiece(piece);
        piece.transform.localPosition = Vector3.zero;
        piece.Creation(t, c);
    }
}
