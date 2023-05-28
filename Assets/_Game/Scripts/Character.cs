using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
		
    private string currentAnimName;
    private float health;
    public bool IsDead => health <= 0; //Kiem tra trang thai cua doi tuong
    
    //Tao ra mot object moi khi can thiet tai bat cu luc nao
    public virtual void OnInit()
	{
        health = 100;

	}

    //Xoa di object bat cu khi nao can thiet
    public virtual void OnDespawn()
	{
		
		

	}

	protected virtual void OnDeath()
	{
		changeAnim("Die");
		Invoke(nameof(OnDespawn), 1.5f);
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
                OnDeath();
			}
		}
	}

	

	

	void Start()
	{
		OnInit();
	}

}
