using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomTime;

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(4f, 8f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x); // doi huong cho dich nhin sang phai neu player co x > hon va sang trai neu < hon
            if (enemy.IsTargetInRange())
			{
                enemy.ChangeState(new AttackState());
			}
			else
			{
                enemy.Moving();
            }
            
		}
		else
		{
            if (timer < randomTime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }

        

        
    }

    public void OnExit(Enemy enemy)
    {

    }
}
