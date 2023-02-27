using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    [SerializeField] private float impulseYrampline = 5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * impulseYrampline, ForceMode2D.Impulse);
    }
}
