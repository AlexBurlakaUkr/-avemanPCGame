
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private float enemyHealth = 50f;
    private DemageLevel clubDemage;
    private DemageLevel rockWeaponDemage;
    private Animator enemyAnimator;
    private Color color;
    private float timeToDestroyObject = 1.5f;
    
    private void Start()
    {
        color.a = 0;
        clubDemage = GameObject.FindGameObjectWithTag("Weapon").GetComponent<DemageLevel>();
        rockWeaponDemage = GameObject.FindGameObjectWithTag("RockWeapon").GetComponent<DemageLevel>();
        enemyAnimator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetDemageToEnemyByWeapon(collision,"Weapon", clubDemage.Demage, KeyCode.LeftAlt);
        GetDemageToEnemyByWeapon(collision, "RockWeapon", rockWeaponDemage.Demage);
    }

    private void GetDemageToEnemyByWeapon(Collision2D collision, string weaponTag, float demageLevel)
    {
        if (collision.gameObject.CompareTag(weaponTag))
        {
            enemyAnimator.SetTrigger("HitRock");
            CheckTheEnemyHelthLevel(demageLevel);
        }
    }
    private void GetDemageToEnemyByWeapon(Collision2D collision, string weaponTag, float demageLevel, KeyCode keyCode)
    {
        if (collision.gameObject.CompareTag(weaponTag) && Input.GetKey(keyCode))
        {
            enemyAnimator.SetTrigger("HitRock");
            CheckTheEnemyHelthLevel(demageLevel);
        }
    }
    private void CheckTheEnemyHelthLevel(float demageLevel)
    {
        if (enemyHealth > 0) enemyHealth -= demageLevel;
        else
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            Destroy(gameObject, timeToDestroyObject);
        }
    }
}
