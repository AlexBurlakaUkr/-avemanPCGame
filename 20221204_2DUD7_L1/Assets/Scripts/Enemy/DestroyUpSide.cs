using UnityEngine;

public class DestroyUpSide : MonoBehaviour
{
    [SerializeField] private new ParticleSystem particleSystem;
    private float playerLayerNumber = 11f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerConnect(collision);
    }
    private void PlayerConnect(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayerNumber)
        {
            particleSystem.Play();
            Destroy(collision.gameObject);
        }
    }
}
