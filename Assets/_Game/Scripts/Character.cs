using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
	[SerializeField] protected HealthBar healthBar;	

	private string currentAnimName;
    private float health;
    public bool IsDead => health <= 0; //Kiem tra trang thai cua doi tuong
    
    //Tao ra mot object moi khi can thiet tai bat cu luc nao
    public virtual void OnInit()
	{
        health = 100;
		healthBar.OnInit(100, transform);
	}

    //Xoa di object bat cu khi nao can thiet
    public virtual void OnDespawn()
	{
		
		

	}

	protected virtual void OnDeath()
	{
		changeAnim("Die");
		Invoke(nameof(OnDespawn), 1f);
	}
	protected void changeAnim(string animName)
	{
		if (currentAnimName != animName)
		{
			anim.ResetTrigger(animName);
			currentAnimName = animName;
			anim.SetTrigger(currentAnimName);
		}
	}

	public void OnHit(float damage)
	{
		Debug.Log("Deals 30 damage!");
        if(!IsDead)
		{
            health -= damage;

            if(IsDead)
			{
				health = 0;

				OnDeath();
			}
			healthBar.SetNewHP(health);
		}
	}

	void Start()
	{
		OnInit();
	}

}
