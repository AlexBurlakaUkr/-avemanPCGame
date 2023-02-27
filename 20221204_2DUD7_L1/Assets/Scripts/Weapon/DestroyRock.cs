
using UnityEngine;

public class DestroyRock : MonoBehaviour
{
    enum ChangeCollisionGameObject { Player, Enemy };
    [SerializeField] private ChangeCollisionGameObject changeCollisionGameObject;
    private Color color;
    private float timeToDestroyObject = 1f;
    void Start()
    {
        color.a = 0;
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(changeCollisionGameObject.ToString()))
        {
            CheckTheEnemyHelthLevel();
        }
    }
    private void CheckTheEnemyHelthLevel()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        Destroy(gameObject, timeToDestroyObject);
    }
}
