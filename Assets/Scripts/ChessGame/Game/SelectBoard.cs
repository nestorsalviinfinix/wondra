using UnityEngine;
using System.Collections.Generic;

public class SelectBoard : MonoBehaviour
{
    public BoardBox boxSelect;
    public BoardBox[,] board;
    private List<BoardBox> indicators = new List<BoardBox>();

    private actualStatus status = actualStatus.InTurn;

    public Camera boardCamera, battleCamera;
    private enum actualStatus {Select, Waiting, InTurn};

    private IPieceMove[] attackStrategies = {new PawnMovesAttack(EChessColor.White),
                                             new PawnMovesAttack(EChessColor.Black),
                                             new KingMoves(),
                                             new KnightMoves(),
                                             new BishopMoves(),
                                             new QueenMoves(),
                                             new TowerMoves()
                                            };

    private void Start()
    {
        InitBattle(false);
    }
    public void Select(BoardBox box)
    {
        if (box == boxSelect || status == actualStatus.Waiting)
            return;
        if(status == actualStatus.InTurn)
        {
            StatusInTurn(box);
        }else if(status == actualStatus.Select)
        {
            StatusSelect(box);
        }
    }
    private void StatusInTurn(BoardBox box)
    {
        if (box.pieceContaining == null)
        {
            CleanIndicators();
            return;
        }
        boxSelect = box;
        boxSelect.MoveIndicator(true, new Color(0.6545857f, 0.7372549f, 0.00392159f));

        if (boxSelect.pieceContaining != null)
        {
            PossibleMoves(boxSelect.positionInMatrix, boxSelect.pieceContaining.canMoves.PossibleMovements(), boxSelect.pieceContaining.canAttack.PossibleMovements());
            status = actualStatus.Select;
        }
    }
    private void PossibleMoves(Vector2Int initPosition, MoveRule[] possibleMoves, MoveRule[] possibleAttacks)
    {
        indicators.Clear();
        foreach (var m in possibleMoves)
        {
            ContinueMove(m, initPosition);
        }
        foreach (var a in possibleAttacks)
        {
            ContinueAttack(a, initPosition);
        }
    }
    private void ContinueMove(MoveRule rule, Vector2Int initPosition)
    {
        if(rule.especialCondition) // Caso muy especifico para el peon. Pero en el futuro extender para aceptar toda clase de condiciones que seran una serie de bools que todos deben estar en true.
        {
            int conditionX = initPosition.x + rule.needEmptySlots.x;
            int conditionY = initPosition.y + rule.needEmptySlots.y;
            if (board[conditionX, conditionY].pieceContaining != null)
                return;
        }
        if(rule.useInTurn >= 0)
            if(board[initPosition.x,initPosition.y].pieceContaining.moveCount != rule.useInTurn)
                return;
        int moveX = initPosition.x + rule.move.x;
        int moveY = initPosition.y + rule.move.y;
        if (moveX < 0 || moveX >= board.GetLength(0) || moveY < 0 || moveY >= board.GetLength(1))
            return;
        BoardBox s = board[moveX,moveY];
        if (s.pieceContaining)
            return;
        s.MoveIndicator(true, new Color(0.003470994f, 0.735849f, 0.1269133f));
        indicators.Add(s);
        if (rule.oneTime)
            return;
        else
            ContinueMove(rule, s.positionInMatrix);
    }
    private void ContinueAttack(MoveRule rule, Vector2Int initPosition)
    {
        if (rule.useInTurn >= 0)
            if (board[initPosition.x, initPosition.y].pieceContaining.moveCount != rule.useInTurn)
                return;
        int moveX = initPosition.x + rule.move.x;
        int moveY = initPosition.y + rule.move.y;
        if (moveX < 0 || moveX >= board.GetLength(0) || moveY < 0 || moveY >= board.GetLength(1))
            return;
        BoardBox s = board[moveX, moveY];
        if (s.pieceContaining)
        {
            if (s.pieceContaining.color == boxSelect.pieceContaining.color)
                return;
            s.MoveIndicator(true, new Color(0.7372549f, 0.00392159f, 0.01141315f));
            indicators.Add(s);
        }
        else if (rule.oneTime)
            return;
        else
            ContinueAttack(rule, s.positionInMatrix);
    }
    private void StatusSelect(BoardBox box)
    {
        if (!indicators.Contains(box))
        {
            status = actualStatus.InTurn;
            CleanIndicators();
            boxSelect = null;
            return;
        }
        if(box.pieceContaining == null)
        {
            box.SetPiece(boxSelect.pieceContaining);  ///// FIJARSE SI ESTA OCUPADO POR UN RIVAL Y COMENZAR UNA BATALLA
            boxSelect.SetPiece(null);
            box.pieceContaining.Movement(box.pieceSpot);
            status = actualStatus.InTurn;
        }
        else
        {
            bool whiteAttack = boxSelect.pieceContaining.color == EChessColor.White;
            List<EChessPieceType> white = new List<EChessPieceType>();
            List<EChessPieceType> black = new List<EChessPieceType>();
            if (whiteAttack)
            {
                white.Add(boxSelect.pieceContaining.type);
                black.Add(box.pieceContaining.type);
            }
            else
            {
                black.Add(boxSelect.pieceContaining.type);
                white.Add(box.pieceContaining.type);
            }
            PartnerInBoard(white, EChessColor.White, box.positionInMatrix);
            PartnerInBoard(black, EChessColor.Black, box.positionInMatrix);

            FindObjectOfType<BattleController>().StartBattle(white,black,boxSelect,box, whiteAttack);
            status = actualStatus.Waiting;
            InitBattle(true);
        }
        CleanIndicators();
    }

    private void PartnerInBoard(List<EChessPieceType> pieces,EChessColor partnerColor, Vector2Int initPosition)
    {
        foreach (var stra in attackStrategies)
        {
            foreach(var r in stra.PossibleMovements())
            {
                int moveX = (initPosition.x - r.move.x);
                int moveY = (initPosition.y - r.move.y);
                do
                {
                    if (moveX < 0 || moveX >= board.GetLength(0) || moveY < 0 || moveY >= board.GetLength(1))
                        break;
                    BoardBox s = board[moveX, moveY];
                    Debug.Log("Buscnado partners: " + s.name);
                    if (s.pieceContaining != null && s.pieceContaining.color == partnerColor && s.pieceContaining.canAttack.GetType() == stra.GetType())
                    {
                        if(s.pieceContaining != boxSelect.pieceContaining)
                            pieces.Add(s.pieceContaining.type);
                        break;
                    }
                    if (s.pieceContaining != null)
                        break;

                    moveX -= r.move.x;
                    moveY -= r.move.y;
                } while (!r.oneTime);
            }
        }
    }

    private void CleanIndicators()
    {
        if (boxSelect == null)
            return;
        boxSelect.MoveIndicator(false);
        foreach (var ind in indicators)
        {
            ind.MoveIndicator(false);
        }
        indicators.Clear();
    }
    public void InitBattle(bool b)
    {
        if (b)
        {
            boardCamera.gameObject.SetActive(false);
            battleCamera.gameObject.SetActive(true);
        }
        else
        {
            boardCamera.gameObject.SetActive(true);
            battleCamera.gameObject.SetActive(false);
            status = actualStatus.InTurn;
        }
    }

}
