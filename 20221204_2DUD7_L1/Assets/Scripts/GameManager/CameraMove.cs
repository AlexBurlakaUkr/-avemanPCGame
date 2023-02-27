using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed = 0.1f;
    [SerializeField] private float playerTeleportCameraSpeed = 0.05f;
    [SerializeField] private float cameraMoveRestrictionYMin = 0.5f;
    [SerializeField] private float cameraMoveRestrictionYMax = 100f;
    [SerializeField] private float cameraMoveRestrictionXMin = -1.5f;
    [SerializeField] private float cameraMoveRestrictionXMax = 21.5f;
    [SerializeField] private float cameraLag = 3f;
    private Vector3 playerStartPosition;
    public bool isPlayerAlive = true;
    private Transform playerPosition;

    private void Start()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
        playerStartPosition = playerPosition.position;
    }
    private void LateUpdate()
    {
        if (isPlayerAlive) CameraFollowsThePlayer(playerPosition.position, cameraMoveSpeed);
        else if (!isPlayerAlive) CameraFollowsThePlayer(playerStartPosition, playerTeleportCameraSpeed);

    }
    private void CameraFollowsThePlayer(Vector3 targetPos, float cameraMoveSpeed)
    {
        var cameraPosition = transform.position;
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, targetPos.x + cameraLag, cameraMoveSpeed * Time.deltaTime);
        cameraPosition.y = Mathf.Lerp(cameraPosition.y, targetPos.y, cameraMoveSpeed * Time.deltaTime);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, cameraMoveRestrictionYMin, cameraMoveRestrictionYMax);
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, cameraMoveRestrictionXMin, cameraMoveRestrictionXMax);
        transform.position = cameraPosition;
    }
}
