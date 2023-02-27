using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthBarCounter : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private TMP_Text healthValueString;
    [SerializeField] private float duckHit = 20f;
    [SerializeField] private float blueBirdHit = 5f;
    [SerializeField] private float fatBirdHit = 20f;
    [SerializeField] private float eggdHit = 10f;
    private int blueBirdLayer = 8, duckLayer = 11, fatBirdLayer = 12, eggLayer = 9;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SubstractEnemyDamageFromPlayerHP(collision.gameObject.layer, blueBirdLayer, blueBirdHit);
        SubstractEnemyDamageFromPlayerHP(collision.gameObject.layer, duckLayer, duckHit);
        SubstractEnemyDamageFromPlayerHP(collision.gameObject.layer, fatBirdLayer, fatBirdHit);
        SubstractEnemyDamageFromPlayerHP(collision.gameObject.layer, eggLayer, eggdHit);
        ChangeTextHPValue();
        LavaContact(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void SubstractEnemyDamageFromPlayerHP(int collisionLayerNumber, int enemyLollisionLayer, float hitValue)
    {
        if (collisionLayerNumber == enemyLollisionLayer)
        {
            healthBar.HP -= hitValue;
            RestrictionForHP();
        }
    }
    private void RestrictionForHP()
    {
        if (healthBar.HP <= 0)
        {
            healthBar.HP = 0;
            GlobalEventManager.SendPlayerDeath();
        }

    }
    private void ChangeTextHPValue()
    {
        healthValueString.text= healthBar.HP.ToString();
    }
    private void LavaContact(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava")){
            healthBar.HP = 0;
            RestrictionForHP();
        }
    }
}
