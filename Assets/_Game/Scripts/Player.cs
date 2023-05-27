using UnityEngine;

public class Player : Character
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private float speed = 250;
	[SerializeField] private float jumpForce = 400;

	[SerializeField] private Kunai kunaiPrefab;
	[SerializeField] private Transform throwPoint;

	private bool isGrounded = false;
	private bool isJumping = false;
	private bool isAttack = false;
	private bool isDead = false;
	private int gamePoint = 0;
	private float horizontal;
	private Vector3 savePoint;

	// Start is called before the first frame update
	void Start()
	{
		savePoint = transform.position;
	}

	private void FixedUpdate()
	{

	}
	// Bug jumping and moving sometime cause character to freeze in falling
	// Update is called once per frame -- bug dùng fixedUpdate gây delay ở hàm input
	void Update()
	{
		isGrounded = CheckGrounded();
		//Nam trong khoang -1 -> 1 va = 0 neu ko bam j
		horizontal = Input.GetAxisRaw("Horizontal");

		Debug.Log(horizontal);

		// check if on ground so that player could jump
		if (isDead)
		{
			return;
		}
		if (isGrounded)
		{
			if (isJumping)
			{
				return;
			}

			//Jumping
			if (Input.GetKeyDown(KeyCode.X) && isGrounded)
			{
				Jump();
			}

			//change anim to run
			if (Mathf.Abs(horizontal) > 0.1f && !isAttack)
			{
				changeAnim("Run");
			}

			//Throw
			if (Input.GetKeyDown(KeyCode.C) && isGrounded)
			{
				Debug.Log("Throw!!");
				Throw();
			}

			//Attack                        
			if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
			{
				Debug.Log("Attack!!");
				Attack();
			}
		}

		//check falling
		if (!isGrounded && rb.velocity.y < 0)
		{
			changeAnim("Fall");
			isJumping = false;
		}

		//Moving
		if (Mathf.Abs(horizontal) > 0.1f && !isAttack) //&& isGrounded 
		{
			moving();
		}
		else if (isGrounded)
		{
			//Set anim idle when do nothing
			changeAnim("Idle");
			rb.velocity = Vector3.zero;
		}


	}

	public override void OnInit()
	{
		base.OnInit();
		isJumping = false;
		isAttack = false;
		isDead = false;
		gamePoint = 0;
		transform.position = savePoint;
		changeAnim("Idle");
	}

	protected override void OnDeath()
	{
		base.OnDeath();
	}

	public override void OnDespawn()
	{
		base.OnDespawn();
	}

	private bool CheckGrounded()
	{
		isJumping = false;
		Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

		return hit.collider != null;

	}

	private void Attack()
	{
		rb.velocity = Vector2.zero;
		changeAnim("Attack");
		isAttack = true;
		Invoke("ResetAttack", 0.5f);
	}

	private void Jump()
	{
		isJumping = true;
		changeAnim("Jump");
		rb.AddForce(jumpForce * Vector2.up);
	}

	private void Throw()
	{
		rb.velocity = Vector2.zero;
		changeAnim("Throw");
		isAttack = true;
		Invoke("ResetAttack", 0.5f);
		Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
	}

	private void ResetAttack()
	{
		changeAnim("idle");
		isAttack = false;
	}

	private void moving()
	{
		rb.velocity = new Vector3(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
		transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
	}

	//Xu ly va cham
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Khi roi xuong vuc
		if (collision.CompareTag("DeadZone"))
		{
			//Destroy(collision.gameObject);
			isDead = true;
			changeAnim("Die");
			Debug.Log("Game over! Total Coin: " + gamePoint);
			Invoke(nameof(OnInit), 1f);
		}

		//Khi nhat coin
		if (collision.CompareTag("Coin"))
		{
			Destroy(collision.gameObject);
			gamePoint++;
			Debug.Log("Total Coin: " + gamePoint);
		}

	}

	internal void SavePoint()
	{
		savePoint = transform.position;
	}
}
