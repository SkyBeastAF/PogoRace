using UnityEngine;

/// <summary>
/// FinishLine — attach to any GameObject that acts as the finish trigger.
///
/// Required setup:
///   • Add a Collider2D to this GameObject and tick "Is Trigger".
///   • Tag your player GameObject as "Player" (or change playerTag below).
///   • StageTimer must exist in the scene.
/// </summary>
public class FinishLine : MonoBehaviour
{
    [Tooltip("Tag used to identify the player GameObject.")]
    public string playerTag = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (StageTimer.Instance != null)
            StageTimer.Instance.StopTimer();
    }
}