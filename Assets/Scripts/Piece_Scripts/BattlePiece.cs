using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
[RequireComponent(typeof(Animator))]
public class BattlePiece : MonoBehaviour
{
    public PieceStats stats;
    private int _maxLife = 0;
    public Animator animator;
    public bool amAlive = false;
    public EChessColor team;
    public EChessPieceType type;
    public BattlePiece enemy;
    public BattleController controller;
    public bool amPrincipal = false;

    private float _timeToAttack, _attackRate;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StartBattle()
    {
        amAlive = true;
        _timeToAttack = 1f/stats.atkSpeed;
        _attackRate = _timeToAttack + Random.Range(-.1f,.1f);
        _maxLife = stats.defense + stats.life;
        animator.Rebind();
    }
    public void FirstAttack()
    {
        _attackRate = 0;
    }

    public void BattleUpdate()
    {
        if (!amAlive)
            return;

        _attackRate -= Time.deltaTime;
        if(_attackRate<=0)
        {
            enemy = controller.FindEnemy(team);
            Attack();

            _attackRate = _timeToAttack + Random.Range(-.1f, .1f);
        }
    }
    public void Attack()
    {
        animator.SetTrigger("attack");
        int damage = 0;
        if(stats.critical > Random.value)
        {
            damage = stats.attack * 2;
        }else
        {
            damage = stats.attack;
        }
        enemy.TakeDamage(damage);
    }
    public void TakeDamage(int dmg)
    {
        if(stats.block > Random.value)
        {
            return;
        }

        if(stats.defense>0)
        {
            stats.defense -= dmg;
            if (stats.defense < 0)
                stats.defense = 0;
        }else
        {
            stats.life -= dmg;
        }
        if (amPrincipal)
            controller.BarDamage(team, (stats.defense + stats.life), _maxLife);

        if (stats.life <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        animator.SetTrigger("death");
        amAlive = false;
        if(amPrincipal)
        {
            controller.EndBattle();
        }
    }
}
