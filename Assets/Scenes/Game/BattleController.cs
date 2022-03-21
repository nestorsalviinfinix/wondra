using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public BattlePiece pieceModel;
    public Transform player1Slots, player2Slots;
    public Camera battleCamera;

    public List<BattlePiece> player1Pieces = new List<BattlePiece>();
    public List<BattlePiece> player2Pieces = new List<BattlePiece>();

    public Image barWhite, barBlack;

    private bool _inBattle = false;

    public LivePiece attacker, defender;

    public bool whiteAttack;
    public LiveBox box;
    private bool _winAttacker;


    void Start()
    {
        player1Pieces = CreatePieces(player1Slots, EChessColor.White);
        player2Pieces = CreatePieces(player2Slots, EChessColor.Black);

        //List<EChessPieceType> test1 = new List<EChessPieceType>() { EChessPieceType.TOWER,EChessPieceType.BISHOP, EChessPieceType.KING };
        //List<EChessPieceType> test2 = new List<EChessPieceType>() { EChessPieceType.KING, EChessPieceType.KNIGHT, EChessPieceType.KNIGHT };

        //StartBattle(test1, test2);
    }


    public void StartBattle(List<EChessPieceType> player1Soldiers, List<EChessPieceType> player2Soldiers,LivePiece attacker, LivePiece defender, bool whiteAttack,LiveBox box)
    {
        SuitPieces(player1Pieces, player1Soldiers);
        SuitPieces(player2Pieces, player2Soldiers);

        barWhite.fillAmount = barBlack.fillAmount = 1;
        _inBattle = true;
        this.attacker = attacker;
        this.defender = defender;
        this.whiteAttack = whiteAttack;
        this.box = box;
    }
    public void EndBattle()
    {
        _inBattle = false;

        EActionType currentActionType = EActionType.Move;
        IAction action = Action.actions[currentActionType];
        if (_winAttacker)
            attacker.ExecuteActionTo(action, box);
        else
        {
            LivePiece piesaNull = new LivePiece();
            ChessPiece chessNull = new ChessPiece(EChessPieceType.NULL,new ChessPlayer("null",EChessColor.White));
            box.piece = piesaNull;
            attacker.Box.SetPiece(chessNull);
        }

        Invoke(nameof(BackToBoard), 4f);
    }
    private void BackToBoard()
    {
        FindObjectOfType<LiveGameController>().InitBattle(false);
    }
    private void Update()
    {
        if(_inBattle)
        {
            foreach (var p in player1Pieces)
                p.BattleUpdate();
            foreach (var p in player2Pieces)
                p.BattleUpdate();
        }
    }
    private void SuitPieces(List<BattlePiece> pieces,List<EChessPieceType> soldiers)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (i < soldiers.Count)
            {
                pieces[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(
                    "Animation/PiecesAnims/BattleAnims/Battle" + (soldiers[i].ToString())
                    );
                pieces[i].GetComponent<BattlePiece>().stats = new PieceStats(soldiers[i]);
                pieces[i].GetComponent<BattlePiece>().StartBattle();
            }
            else
            {
                pieces[i].GetComponent<Animator>().runtimeAnimatorController = null;
                pieces[i].GetComponent<SpriteRenderer>().sprite = null;
                pieces[i].GetComponent<BattlePiece>().amAlive = false;
            }
        }
    }

    public void BarDamage(EChessColor team, int currentLife, int maxLife)
    {
        if (team == EChessColor.White)
        {
            barWhite.fillAmount = (float)currentLife / (float)maxLife;
            if(barWhite.fillAmount <=0)
            {
                if(whiteAttack)
                {
                    attacker.Capture();
                    _winAttacker = false;
                }
                else
                {
                  defender.Capture();
                    _winAttacker = true;
                }
            }

        }
        else
        {
            barBlack.fillAmount = (float)currentLife / (float)maxLife;
            if(barBlack.fillAmount<=0)
            {
                if (whiteAttack)
                {
                    defender.Capture();
                    _winAttacker = true;
                }    
                else
                {
                    attacker.Capture();
                    _winAttacker = false;
                }
            }
        }
    }

    private List<BattlePiece> CreatePieces(Transform father, EChessColor color)
    {
        Transform[] chills = father.GetComponentsInChildren<Transform>().Where(c=>c != father).ToArray();
        List<BattlePiece> pieces = new List<BattlePiece>();

        foreach (var c in chills)
        {
            pieces.Add(CreateEmptyPiece(c,color));
        }
        pieces[0].amPrincipal = true;

        //TestMode
        if(color == EChessColor.Black)
        {
            foreach (var p in pieces)
            {
                p.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }

        return pieces;
    }

    private BattlePiece CreateEmptyPiece(Transform myFather, EChessColor c)
    {
        var p = Instantiate(pieceModel, myFather);
        p.transform.localPosition = Vector2.zero;
        p.transform.localScale = Vector3.one;
        p.GetComponent<LookAtCamera>().SetCamera(battleCamera);
        BattlePiece bp = p.GetComponent<BattlePiece>();
        bp.controller = this;
        bp.team = c;
        return bp;
    }
    public BattlePiece FindEnemy(EChessColor color)
    {
        if(color == EChessColor.White)
        {
            BattlePiece[] array = player2Pieces.Where(p => p.amAlive).ToArray();
            return array[Random.Range(0, array.Length)];
        }else
        {
            BattlePiece[] array = player1Pieces.Where(p => p.amAlive).ToArray();
            return array[Random.Range(0, array.Length)];
        }
    }

}
