using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;

    float hp;
    float maxHP;

    private Transform target;
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHP, Time.deltaTime * 5f); 
    }

    public void OnInit(float maxHp, Transform target)
	{
        this.target = target;
        this.maxHP = maxHp;
        hp = maxHP;
        imageFill.fillAmount = 1; 
	}

    public void SetNewHP(float hp)
	{
        this.hp = hp;
	}
}
