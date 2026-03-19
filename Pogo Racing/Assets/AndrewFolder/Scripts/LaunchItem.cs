using UnityEngine;

/// <summary>
/// LaunchItem — attach to the player GameObject.
///
/// Required setup:
///   • The player must have a Rigidbody2D and a Collider2D.
///   • Tag any launch item GameObjects with the tag defined in itemTag below.
///   • Each launch item needs a Collider2D with "Is Trigger" ticked.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class LaunchItem : MonoBehaviour
{
    [Header("Launch")]
    [Tooltip("Upward impulse applied to the player on collection.")]
    public float launchForce = 25f;

    [Tooltip("If true, launches in the player's local up direction. If false, always world up.")]
    public bool usePlayerLocalUp = true;

    [Tooltip("If true, cancels the player's current velocity before launching (consistent height).")]
    public bool resetVelocityBeforeLaunch = true;

    [Header("Detection")]
    [Tooltip("Tag used to identify launch item GameObjects.")]
    public string itemTag = "LaunchItem";

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(itemTag)) return;

        if (resetVelocityBeforeLaunch)
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

        Vector2 direction = usePlayerLocalUp ? (Vector2)transform.up : Vector2.up;
        _rb.AddForce(direction * launchForce, ForceMode2D.Impulse);

        Destroy(other.gameObject);
    }
}