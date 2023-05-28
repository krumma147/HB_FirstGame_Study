using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit()
	{
        rb.velocity = transform.right * 5f;
		Invoke(nameof(OnDespawn), 1.5f);
	}

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
            collision.GetComponent<Character>().OnHit(30f);
            OnDespawn();
		}
	}
}
