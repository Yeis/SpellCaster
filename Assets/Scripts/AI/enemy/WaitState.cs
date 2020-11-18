using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : EnemyState
{
    public WaitState(Enemy enemy) : base(enemy)
    {
    }

    public override IEnumerator Start()
    {
        Enemy.print("WaitState");
        Enemy.animator.SetFloat("Horizontal", Enemy.currDirectiion.x);
        Enemy.animator.SetFloat("Vertical", Enemy.currDirectiion.y);
        //Me espero a que la barra se llene
        yield return new WaitForSeconds(.2f);
        if (InAttackRange())
        {
            Enemy.SetState(new AttackState(Enemy));
        }
        else
        {
            Enemy.SetState(new FollowState(Enemy));
        }

    }

    private bool InAttackRange()
    {
        Vector2Int gridPlayerPos = (Vector2Int)Enemy.battleFieldReference.walkableTileMap.WorldToCell(Enemy.playerReference.transform.position);
        Vector2Int gridEnemyPos = (Vector2Int)Enemy.battleFieldReference.walkableTileMap.WorldToCell(Enemy.transform.position);
        Spell spell = Enemy.spells[0].GetComponent<Spell>();

        //Check Right
        if (gridPlayerPos.y == gridEnemyPos.y && Mathf.Abs(gridEnemyPos.x - gridPlayerPos.x) <= spell.maxDistance && gridEnemyPos.x < gridPlayerPos.x)
        {
            Enemy.print("Attacking Right");
            Enemy.currDirectiion = Direction.Right;
            return true;
        }
        //Check Left
        else if (gridPlayerPos.y == gridEnemyPos.y && Mathf.Abs(gridEnemyPos.x - gridPlayerPos.x) <= spell.maxDistance && gridEnemyPos.x > gridPlayerPos.x)
        {
            Enemy.print("Attacking Left");
            Enemy.currDirectiion = Direction.Left;
            return true;
        }
        //Check Down
        else if (gridPlayerPos.x == gridEnemyPos.x && Mathf.Abs(gridEnemyPos.y - gridPlayerPos.y) <= spell.maxDistance && gridEnemyPos.y > gridPlayerPos.y)
        {
            Enemy.print("Attacking Down");

            Enemy.currDirectiion = Direction.Down;
            return true;
        }
        //Check Up
        else if (gridPlayerPos.x == gridEnemyPos.x && Mathf.Abs(gridEnemyPos.y - gridPlayerPos.y) <= spell.maxDistance && gridEnemyPos.y < gridPlayerPos.y)
        {
            Enemy.print("Attacking Up");

            Enemy.currDirectiion = Direction.Up;
            return true;
        }
        return false;
    }
}