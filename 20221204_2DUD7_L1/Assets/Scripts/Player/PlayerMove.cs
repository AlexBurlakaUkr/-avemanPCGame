using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rB2D;
    private Transform flippingSprite;
    private Animator playerAnimator;
    private GroundCheck check;
    private GameObject[] fly;
    private float playerVariableSpeed = 0.1f;
    private float horizontalMove;

    [SerializeField] private float playerSpeed = 0.2f;
    [SerializeField] private float playerRunSpeed = 0.8f;
    [SerializeField] private float playerSideImpulse = 10f;

    private void Start()
    {
        fly = GameObject.FindGameObjectsWithTag("Fly");
        check = GetComponentInChildren<GroundCheck>();
        rB2D = GetComponent<Rigidbody2D>();
        flippingSprite = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        GetPlayerVariableSpeed();
        GetMoveWithoutInertion();
        GetSideImpulse(Vector2.right, check.onGroundCheck);

    }
    private void Update()
    {
        GetPlayerSpriteDirection();
        PlayerRunAnimation();
    }

    private void GetPlayerVariableSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift)) playerVariableSpeed = playerRunSpeed;
        else playerVariableSpeed = playerSpeed;
    }

    public void GetDirectionImpulse(Vector2 vector, float impulsMagnitude)
    {
        rB2D.AddForce(vector * impulsMagnitude, ForceMode2D.Impulse);
    }

    private void GetPlayerSpriteDirection()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            flippingSprite.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
            foreach (var item in fly)
            {
                item.transform.localScale = new Vector3(0.1f, item.transform.localScale.y, item.transform.localScale.z);

            }
        }

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            flippingSprite.localScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);
            foreach (var item in fly)
            {
                item.transform.localScale = new Vector3(-0.1f, item.transform.localScale.y, item.transform.localScale.z);

            }
        }

    }
    public void GetSideImpulse(Vector2 vector, bool onGroundCheck)
    {
        if (Input.GetKey(KeyCode.E) && onGroundCheck)
        {
            if (transform.localScale.x > 0) GetDirectionImpulse(vector, playerSideImpulse);
            else if (transform.localScale.x < 0) GetDirectionImpulse(-vector, playerSideImpulse);
        }
    }
  
    private void GetMoveWithoutInertion()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        Vector3 velocity = new Vector3(horizontalMove, 0, 0) * playerVariableSpeed;
        velocity.y = rB2D.velocity.y;
        rB2D.velocity = velocity;
        //rB2D.AddForce(Vector3.right * Input.GetAxis("Horizontal") * playerVariableSpeed); - enother move logic
    }
    private void PlayerRunAnimation()
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    public void PlayerJumpAnimation()
    {
        playerAnimator.SetBool("IsJumping", true);
    }
    public void OnLanding()
    {
        playerAnimator.SetBool("IsJumping", false);
    }
}
