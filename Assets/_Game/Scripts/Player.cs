using UnityEngine;

public class Player : Character
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private float speed = 250;
	[SerializeField] private float jumpForce = 400;

	[SerializeField] private Kunai kunaiPrefab;
	[SerializeField] private Transform throwPoint;
	[SerializeField] private GameObject attackArea;

	private bool isGrounded = false;
	private bool isJumping = false;
	private bool isAttack = false;
	private int gamePoint = 0;
	private float horizontal;
	private Vector3 savePoint;

	private void Awake()
	{
		gamePoint = PlayerPrefs.GetInt("gamePoint", 0);//Lưu data trong storage như trong local storage
	}

	// Update is called once per frame -- bug dùng fixedUpdate gây delay ở hàm input
	void Update()
	{
		isGrounded = CheckGrounded();
		//Nam trong khoang -1 -> 1 va = 0 neu ko bam j
		horizontal = Input.GetAxisRaw("Horizontal");

		//Debug.Log(horizontal);

		// check if on ground so that player could jump
		if (IsDead)
		{
			return;
		}
		if (isGrounded)
		{
			if (isJumping)
			{
				//Debug.Log("Jumping when in ground!");
				return;
			}

			//Jumping
			if (Input.GetKeyDown(KeyCode.X))
			{
				Jump();
			}

			//change anim to run
			if (Mathf.Abs(horizontal) > 0.1f && !isAttack)
			{
				changeAnim("Run");
			}

			//Throw
			if (Input.GetKeyDown(KeyCode.C))
			{
				//Debug.Log("Throw!!");
				Throw();
			}

			//Attack                        
			if (Input.GetKeyDown(KeyCode.Z))
			{
				//Debug.Log("Attack!!");
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
		if (Mathf.Abs(horizontal) > 0.1f && !isAttack)
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
		gamePoint = 0;
		transform.position = savePoint;
		changeAnim("Idle");
		DeactiveAttack();
		SavePoint();
		UIManager.instance.setCoin(gamePoint);
	}

	protected override void OnDeath()
	{
		base.OnDeath();
		
	}

	public override void OnDespawn()
	{
		base.OnDespawn();
		OnInit();
	}

	private bool CheckGrounded()
	{
		//Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

		return hit.collider != null;

	}

	public void Attack()
	{
		if (isGrounded && !isAttack) {
			rb.velocity = Vector2.zero;
			changeAnim("Attack");
			isAttack = true;
			Invoke("ResetAttack", 0.5f);
			ActiveAttack();
			Invoke("DeactiveAttack", 0.5f);
		}
		else
		{
			return;
		}
		
	}

	public void Jump()
	{
		isJumping = true;
		changeAnim("Jump");
		rb.AddForce(jumpForce * Vector2.up);
	}

	public void Throw()
	{
		if(isGrounded && !isAttack)
		{
			rb.velocity = Vector2.zero;
			changeAnim("Throw");
			isAttack = true;
			Invoke("ResetAttack", 0.5f);
			Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation); //Create kunai prefab at the throw point 
		}
		else
		{
			return;
		}
		
	}

	private void ResetAttack()
	{
		changeAnim("idle");
		isAttack = false;
	}

	private void moving()
	{
		rb.velocity = new Vector3(horizontal* speed, rb.velocity.y);
		transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
	}


	internal void SavePoint()
	{
		savePoint = transform.position;
	}

	private void ActiveAttack()
	{
		attackArea.SetActive(true);
	}

	private void DeactiveAttack()
	{
		attackArea.SetActive(false);
	}

	public void setMove(float horizonal)
	{
		this.horizontal = horizonal;
	}

	//Xu ly va cham
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Khi roi xuong vuc
		if (collision.CompareTag("DeadZone"))
		{
			//Destroy(collision.gameObject);
			OnHit(999f);
			Debug.Log("Game over! Total Coin: " + gamePoint);
			//Invoke(nameof(OnInit), 1f);
		}

		//Khi nhat coin
		if (collision.CompareTag("Coin"))
		{
			Destroy(collision.gameObject);
			gamePoint++;
			PlayerPrefs.SetInt("gamePoint", gamePoint);
			UIManager.instance.setCoin(gamePoint);
			//Debug.Log("Total Coin: " + gamePoint);
		}

	}

}
