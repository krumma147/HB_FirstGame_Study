using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject hitVFX;
    public Rigidbody2D rb;
    public GameObject spawnKunai;
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
		if (hitVFX != null)
		{
			Destroy(spawnKunai);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
            collision.GetComponent<Character>().OnHit(30f);
            spawnKunai = Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();      
        }
	}
}
