using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
	[SerializeField] private float attackRange;
	[SerializeField] private float moveSpeed;
	[SerializeField] private Rigidbody2D rb;
	private bool isRight = true;
	private IState currentState;
	private Character target;
	public Character Target => target; // quy tac dong goi cua huong doi tuong

	void Update()
	{
		if(currentState != null)
		{
			currentState.OnExecute(this);   
		}
	}
	public override void OnInit()
	{
		base.OnInit();
		ChangeState(new IdleState());
	}

	public override void OnDespawn()
	{
		base.OnDespawn();
	}

	protected override void OnDeath()
	{
		base.OnDeath();
	}

	

	public void ChangeState(IState newState)
	{
		if ( currentState != null)
		{
			currentState.OnExit(this);
		}
		currentState = newState;
		if (currentState != null)
		{
			currentState.OnEnter(this);
		}
	}

	internal void SetTarget(Character character)
	{
		this.target = character;
		if (IsTargetInRange())
		{
			ChangeState(new AttackState());
		}else if(Target != null)
		{
			ChangeState(new PatrolState());
		}
		else
		{
			ChangeState(new IdleState());
		}
	}


	public void Moving()
	{
		changeAnim("Run");
		rb.velocity = transform.right * moveSpeed;
	}

	public void StopMoving()
	{
		changeAnim("Idle");
		rb.velocity = Vector3.zero;
	}

	public void Attack()
	{
		changeAnim("Attack");
	}

	public bool IsTargetInRange()
	{
		if(target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "EnemyWall")
		{
			ChangeDirection(!isRight);
		}
	}

	public void ChangeDirection(bool isRight)
	{
		this.isRight = isRight;
		transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
	}
}