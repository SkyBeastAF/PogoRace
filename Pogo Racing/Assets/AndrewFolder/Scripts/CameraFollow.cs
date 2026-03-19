using UnityEngine;

/// <summary>
/// CameraFollow — attach this to your Camera GameObject.
///
/// Required setup:
///   • Assign the player Transform to "player" in the inspector.
///   • Camera only scrolls vertically — horizontal position is never changed.
///
/// The camera stays put until the player first reaches the screen centre,
/// then follows them vertically at all times from that point on.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("The player Transform to follow.")]
    public Transform player;

    [Header("Follow")]
    [Tooltip("How smoothly the camera catches up to the player. Lower = snappier, higher = floatier.")]
    public float smoothSpeed = 4f;

    [Tooltip("How far above the screen centre the player must be to trigger following for the first time.")]
    public float deadZoneAboveCentre = 0f;

    private bool _following = false;

    void LateUpdate()
    {
        if (player == null) return;

        // Activate following once the player reaches the centre for the first time
        if (!_following && player.position.y > transform.position.y + deadZoneAboveCentre)
            _following = true;

        if (!_following) return;

        float targetY   = player.position.y - deadZoneAboveCentre;
        float smoothedY = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * smoothSpeed);

        transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);
    }
}