using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PieceStats : MonoBehaviour
{
    public int life, defense, attack;
    public float atkSpeed, critical, block;

    public PieceStats()
    {

    }
    public PieceStats(EChessPieceType type)
    {
        switch (type)
        {
            case EChessPieceType.PAWN:
                SetStats(3, 1, 1, 1, .1f, .1f);
                break;
            case EChessPieceType.TOWER:
                SetStats(1, 8, 3, .25f, 0, .15f);
                break;
            case EChessPieceType.BISHOP:
                SetStats(4, 1, 2, .65f, .25f, 0);
                break;
            case EChessPieceType.KNIGHT:
                SetStats(3, 4, 5, .33f, .15f, .35f);
                break;
            case EChessPieceType.QUEEN:
                SetStats(7, 0, 1, 1.6f, .35f, .35f);
                break;
            case EChessPieceType.KING:
                SetStats(3, 3, 1, 1, .1f, .65f);
                break;
            default:
                break;
        }
    }
    private void SetStats(int life, int defense, int atk, float atkSpeed, float critical, float block)
    {
        this.life = life;
        this.defense = defense;
        this.attack = atk;
        this.atkSpeed = atkSpeed;
        this.critical = critical;
        this.block = block;
    }
}
