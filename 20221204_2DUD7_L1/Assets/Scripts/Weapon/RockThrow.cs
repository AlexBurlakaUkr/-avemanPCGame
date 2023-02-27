using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rockPrefab;
    [SerializeField] private GameObject startThrowRockPoint;
    [SerializeField] private float rockSpeed = 1f;

    private Transform player;
    private Rigidbody2D rockRB;
    private bool isThrowRock = false;
    private float throwDeley = 0.6f;
    private float rockThrowDirectionX = 1f;
    private float rockThrowDirectionY = 0.5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isThrowRock)
        {
            StartCoroutine(ThrowTheRock());
            isThrowRock = true;
        }
    }
    IEnumerator ThrowTheRock()
    {
        yield return new WaitForSeconds(throwDeley);
        rockRB = Instantiate(rockPrefab, startThrowRockPoint.transform.position, Quaternion.identity);
        if(player.localScale.x > 0) rockRB.velocity = new Vector2(rockThrowDirectionX, rockThrowDirectionY) * rockSpeed;
        else if(player.localScale.x < 0) rockRB.velocity = new Vector2(-rockThrowDirectionX, rockThrowDirectionY) * rockSpeed;

        isThrowRock = false;
    }
}
