using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    [SerializeField] private float waitTeleportSeconds = 1.5f;
    private Vector3 playerStartPosition;
    private Vector3 flyStartPosition;
    private GameObject flyObject;
    private CameraMove cameraMain;
    private void Start()
    {
        flyObject = GameObject.FindGameObjectWithTag("Fly");
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<CameraMove>();
        flyStartPosition = flyObject.gameObject.transform.position;
        playerStartPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava")) StartCoroutine(TeleportPayer());
    }

    IEnumerator TeleportPayer()
    {
        cameraMain.isPlayerAlive = false;
        yield return new WaitForSeconds(waitTeleportSeconds);
        flyObject.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        transform.position = playerStartPosition;
        flyObject.gameObject.transform.position = flyStartPosition;
        flyObject.gameObject.SetActive(true);
        transform.gameObject.SetActive(true);
        cameraMain.isPlayerAlive = true;
    }
}
