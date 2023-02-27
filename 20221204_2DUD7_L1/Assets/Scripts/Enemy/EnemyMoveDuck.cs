using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveDuck : MonoBehaviour
{
    enum GetDirectionMove { horizontal, vertical };
    enum GetActionVariant { StandartMove, SeePlayer, AttackPlayer };
    enum GetTypeOfAttack { Eggs, Kick };

    [SerializeField] private GetDirectionMove side = GetDirectionMove.horizontal;
    [SerializeField] private GetActionVariant action = GetActionVariant.StandartMove;
    [SerializeField] private GetTypeOfAttack attackType = GetTypeOfAttack.Kick;
    [SerializeField] private Vector3 extrimeRightPoint;
    [SerializeField] private Vector3 extrimeLeftPoint;
    [SerializeField] private float extrimeLeftPointX;
    [SerializeField] private float extrimeRightPointX;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemySpeedSeePlayer;
    [SerializeField] private float enemySpeedAttackPlayer;
    [SerializeField] private float scaleEnemyX = 0.8f;
    [SerializeField] private float distanceToSeePlayer;
    [SerializeField] private float distanceToAttackPlayer;
    [SerializeField] private float attackForce;
    [SerializeField] private float coolDownTime = 0.07f;
    [SerializeField] private float throwEggsForce;
    [SerializeField] private float throwEggsDeleyTime;
    [SerializeField] private Rigidbody2D eggsThrowPrefab;
    [SerializeField] private GameObject startEggsThrowPointRB;

    private Transform player;
    private Vector3 targetPoint;
    private Rigidbody2D enemyRB;
    private Rigidbody2D eggsThrowRB;
    private bool isPlayerContact = false;
    private bool isThrowEggs = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRB = GetComponent<Rigidbody2D>();
        targetPoint = extrimeLeftPoint;
    }
    private void FixedUpdate()
    {
        if (action == GetActionVariant.StandartMove) ChangeEnemyDirectionMove();
        if (action == GetActionVariant.SeePlayer)
        {
            MoveEnenyToTarget(player.position, enemySpeedSeePlayer);
            ChangePlayerScale(player.position.x);
        }
        if (action == GetActionVariant.AttackPlayer)
        {
            if (attackType == GetTypeOfAttack.Kick) if (!isPlayerContact) MoveEnenyToTarget(player.position, enemySpeedAttackPlayer);
            if (attackType == GetTypeOfAttack.Eggs && !isThrowEggs)
            {
                isThrowEggs = true;
                StartCoroutine(ThrowEggs());
            }
        }
    }
    void Update()
    {
        EnemyRestriction();
        var distanceBetweenEnemyAndPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceBetweenEnemyAndPlayer > distanceToSeePlayer) action = GetActionVariant.StandartMove;
        if (distanceBetweenEnemyAndPlayer <= distanceToSeePlayer &&
            distanceBetweenEnemyAndPlayer >= distanceToAttackPlayer) action = GetActionVariant.SeePlayer;
        if (distanceBetweenEnemyAndPlayer < distanceToAttackPlayer) action = GetActionVariant.AttackPlayer;
    }
    private void ChangeEnemyDirectionMove()
    {
        MoveEnenyToTarget(targetPoint, enemySpeed);
        ChangePlayerScale(targetPoint.x);
        if (side == GetDirectionMove.horizontal)
        {
            ChangeDirectionMove(transform.position.x, extrimeRightPoint.x, extrimeLeftPoint.x);
        }
        if (side == GetDirectionMove.vertical)
        {
            ChangeDirectionMove(transform.position.y, extrimeRightPoint.y, extrimeLeftPoint.y);
        }
    }
    private void MoveEnenyToTarget(Vector3 targetPosition, float playerSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerSpeed);
    }
    private void ChangePlayerScale(float targetPositionX)
    {
        if (targetPositionX < transform.position.x) transform.localScale = new Vector3(scaleEnemyX, transform.localScale.y, transform.localScale.z);
        if (targetPositionX > transform.position.x) transform.localScale = new Vector3(-scaleEnemyX, transform.localScale.y, transform.localScale.z);
    }
    private void ChangeDirectionMove(float enemyPositionX, float extrimeRightPointAxis, float extrimeLeftPointAxis)
    {
        if (enemyPositionX == extrimeRightPointAxis) ChangeExtrimePoint(extrimeLeftPoint);
        if (enemyPositionX == extrimeLeftPointAxis) ChangeExtrimePoint(extrimeRightPoint);
    }
    private void ChangeExtrimePoint(Vector3 extrimePoint) => targetPoint = extrimePoint;
    private void EnemyRestriction()
    {
        var playerRestriction = transform.position;
        playerRestriction.x = Math.Clamp(playerRestriction.x, extrimeLeftPointX, extrimeRightPointX);
        transform.position = playerRestriction;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerContact = true;
            if (player.position.x < transform.position.x) enemyRB.AddForce(Vector2.right * attackForce, ForceMode2D.Impulse);
            if (player.position.x > transform.position.x) enemyRB.AddForce(Vector2.left * attackForce, ForceMode2D.Impulse);
            StartCoroutine(CoolDown());
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownTime);
        isPlayerContact = false;
    }
    IEnumerator ThrowEggs()
    {
        Vector2 directionThrow = (player.position - startEggsThrowPointRB.transform.position).normalized;
        eggsThrowRB = Instantiate(eggsThrowPrefab, startEggsThrowPointRB.transform.position, Quaternion.identity);
        eggsThrowRB.AddForce(directionThrow * throwEggsForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(throwEggsDeleyTime);

        isThrowEggs = false;
    }
}
