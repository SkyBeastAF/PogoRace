using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    
    
    public Transform player;

   
    
    public float smoothSpeed = 4f;

    
    public float deadZoneAboveCentre = 0f;

    // The highest Y the camera has ever reached — never decreases
    private float _highestCameraY;
    private Camera _cam;

    void Awake()
    {
        _cam = GetComponent<Camera>();
        _highestCameraY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // World-space Y of the screen's centre + optional dead zone
        float triggerY = transform.position.y + deadZoneAboveCentre;

        // Only move the camera if the player is above the trigger point
        if (player.position.y > triggerY)
        {
            float targetY = Mathf.Max(player.position.y - deadZoneAboveCentre, _highestCameraY);
            float smoothedY = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * smoothSpeed);

            // Never scroll back down
            float newY = Mathf.Max(smoothedY, _highestCameraY);
            _highestCameraY = newY;

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}