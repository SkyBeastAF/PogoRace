using UnityEngine;
using TMPro;

/// <summary>
/// StageTimer — attach to the UI TextMeshPro element displaying the timer.
///
/// Required setup:
///   • This component must be on a TextMeshProUGUI GameObject in a Canvas.
///   • Call StageTimer.Instance.StopTimer() from your finish line trigger when
///     the player crosses it (see FinishLine.cs).
///
/// The elapsed time is saved to PlayerPrefs under the key defined by
/// SaveKey, so it persists between attempts and can be read from anywhere.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class StageTimer : MonoBehaviour
{
    public static StageTimer Instance { get; private set; }

    [Header("Save")]
    [Tooltip("PlayerPrefs key used to save and load the last recorded time.")]
    public string saveKey = "LastStageTime";

    // The serialized backing field — visible in the Inspector and survives
    // recompiles / scene reloads without needing PlayerPrefs.
    [SerializeField, HideInInspector]
    private float _savedTime = 0f;

    // ── runtime state ──────────────────────────────────────────────────────────
    private float               _elapsed  = 0f;
    private bool                _running  = false;
    private TextMeshProUGUI     _label;

    // ──────────────────────────────────────────────────────────────────────────
    void Awake()
    {
        // Singleton so FinishLine.cs can reach it easily
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        _label = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _elapsed = 0f;
        _running = true;
        UpdateDisplay(0f);
    }

    void Update()
    {
        if (!_running) return;

        _elapsed += Time.deltaTime;
        UpdateDisplay(_elapsed);
    }

    // ── Public API ─────────────────────────────────────────────────────────────

    /// <summary>Called by FinishLine (or any other script) to stop the timer and save the result.</summary>
    public void StopTimer()
    {
        if (!_running) return;

        _running   = false;
        _savedTime = _elapsed;

        // Persist to PlayerPrefs as a secondary save (survives play-mode resets)
        PlayerPrefs.SetFloat(saveKey, _savedTime);
        PlayerPrefs.Save();

        UpdateDisplay(_savedTime);
    }

    /// <summary>Returns the most recently saved time in seconds.</summary>
    public float GetSavedTime() => _savedTime;

    // ── Display ───────────────────────────────────────────────────────────────
    private void UpdateDisplay(float totalSeconds)
    {
        int minutes      = (int)(totalSeconds / 60f);
        int seconds      = (int)(totalSeconds % 60f);
        int milliseconds = (int)((totalSeconds * 100f) % 100f);   // two-digit centiseconds

        _label.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}