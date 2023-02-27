using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalWork : MonoBehaviour
{
    private ParticleSystem portalLight;
    private GameManager loadNextScene;
    private float loadSceneDeley = 1.5f;
    [SerializeField] private int numberOfStartScene = 0;
    private void Start()
    {
        portalLight = GetComponentInChildren<ParticleSystem>();
        loadNextScene = FindObjectOfType<GameManager>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TeleportPlayer(collision);
    }

    private void TeleportPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            portalLight.Play();
            StartCoroutine(LoadNewScene());
        }
    }
    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(loadSceneDeley);
        loadNextScene.StartNumberOfScene(numberOfStartScene);
    }
}
