
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float playerJumpImpulse = 5f;
    private PlayerMove player;

    public bool onGroundCheck = true;

    private void Start()
    {
        player = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        GetPlayerJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGroundCheck = true;
            player.OnLanding();
        }
    }

    public void GetPlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGroundCheck)
        {
            player.GetDirectionImpulse(Vector2.up, playerJumpImpulse);
            onGroundCheck = false;
            player.PlayerJumpAnimation();
        }
    }
}
