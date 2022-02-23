using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullPiece : LivePiece
{
    public override EChessColor PieceColor { get => EChessColor.Black; }
    public override EChessPieceType PieceType { get => EChessPieceType.PAWN; }
    public NullBox nullBox;
    private void Start()
    {
        SetBox(new ChessBoardBox(-1, -1, EChessColor.Black));
    }

    private void Update()
    {
        return;
    }
}
