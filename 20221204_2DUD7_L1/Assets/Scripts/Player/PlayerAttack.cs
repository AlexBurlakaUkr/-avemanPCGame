using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMove player;
    private Animator playerAnimation;
    private string typeOfAttack = "IsAttackTrg", typeOfThrow = "IsThrowTrg";
    private void Start()
    {
        player = GetComponentInParent<PlayerMove>();
        playerAnimation = GetComponent<Animator>();
    }
    private void Update()
    {
        GetPlayerAttack();
    }
    private void GetPlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            PlayerAttackAnimation(typeOfAttack);
            player.OnLanding();
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerAttackAnimation(typeOfThrow);
            player.OnLanding();
        }
    }
    private void PlayerAttackAnimation(string typeOfAttack) => playerAnimation.SetTrigger(typeOfAttack);

}
