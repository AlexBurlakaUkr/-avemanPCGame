using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMove : MonoBehaviour
{
    [SerializeField] private float flyMoveSpeed = 0.1f;
    [SerializeField] private float flyLagX = 0f;
    [SerializeField] private float flyLagY = 0f;
    private Transform targetPosition;

    private void Start()
    {
        targetPosition = GameObject.Find("Target").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        CameraFollowsThePlayer(targetPosition);
    }
    private void CameraFollowsThePlayer(Transform targetPos)
    {
        var flyPosition = transform.position;
        flyPosition.x = Mathf.Lerp(flyPosition.x, targetPos.position.x + flyLagX, flyMoveSpeed * Time.deltaTime);
        flyPosition.y = Mathf.Lerp(flyPosition.y, targetPos.position.y + flyLagY, flyMoveSpeed * Time.deltaTime);
        transform.position = flyPosition;
    }
}
