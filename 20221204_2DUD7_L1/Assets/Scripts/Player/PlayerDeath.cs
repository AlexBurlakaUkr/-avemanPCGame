using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ParticleSystem playerDeathClip;
    [SerializeField] private GameObject enemyes;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject backButtonImage;
    private Animator playerDeath;
    private GameObject club;
    void Start()
    {
        club = GameObject.FindGameObjectWithTag("Weapon");
        playerDeath = GetComponent<Animator>();
        GlobalEventManager.OnPlayerDeath.AddListener(PlayerDeathLogic);
    }

    private void PlayerDeathLogic()
    {
        if (healthBar.HP == 0)
        {
            playerDeathClip.Play();
            playerDeath.SetTrigger("Death");
            club.SetActive(false);
            enemyes.SetActive(false);
            gameMenu.SetActive(true);
            backButton.SetActive(false);
            backButtonImage.SetActive(false);

        }
    }
}
