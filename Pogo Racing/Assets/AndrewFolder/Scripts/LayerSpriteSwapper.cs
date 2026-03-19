using UnityEngine;

/// <summary>
/// LayerSpriteSwapper.cs
///
/// Attach this to the same GameObject as your SpriteRenderer.
///
/// HOW IT WORKS:
///   - Every frame, reads gameObject.layer and compares it to the last known layer.
///   - If the layer has changed, swaps the SpriteRenderer's sprite to match.
///   - No coupling to your existing layer script — it simply watches the layer
///     value that your other script is already writing.
///
/// SETUP:
///   1. Attach this script to your player GameObject.
///   2. In the Inspector, set the four Layer Name strings to exactly match
///      the layer names used in your other script's LayerMask.NameToLayer() calls.
///   3. Assign a sprite to each of the four sprite slots.
///   4. That's it — no references to your other script needed.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class LayerSpriteSwapper : MonoBehaviour
{
    // -----------------------------------------------------------------------
    // Inspector fields
    // -----------------------------------------------------------------------

    [Header("Layer Names — must match your LayerMask.NameToLayer() strings exactly")]
    public string layerName0 = "Layer0";
    public string layerName1 = "Layer1";
    public string layerName2 = "Layer2";
    public string layerName3 = "Layer3";

    [Header("Sprites — one per layer, in the same order as the names above")]
    public Sprite spriteForLayer0;
    public Sprite spriteForLayer1;
    public Sprite spriteForLayer2;
    public Sprite spriteForLayer3;

    // -----------------------------------------------------------------------
    // Private state
    // -----------------------------------------------------------------------

    private SpriteRenderer _spriteRenderer;

    // Cache the layer integers at startup so we're not calling
    // LayerMask.NameToLayer() every single frame.
    private int _layerId0;
    private int _layerId1;
    private int _layerId2;
    private int _layerId3;

    // Track the last layer we acted on so we only swap when something changes.
    private int _lastLayer = -1;

    // -----------------------------------------------------------------------
    // Unity lifecycle
    // -----------------------------------------------------------------------

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Resolve layer names → integer IDs once, up front.
        _layerId0 = LayerMask.NameToLayer(layerName0);
        _layerId1 = LayerMask.NameToLayer(layerName1);
        _layerId2 = LayerMask.NameToLayer(layerName2);
        _layerId3 = LayerMask.NameToLayer(layerName3);

        // Warn if any name didn't resolve — a typo here is easy to miss.
        WarnIfInvalidLayer(_layerId0, layerName0);
        WarnIfInvalidLayer(_layerId1, layerName1);
        WarnIfInvalidLayer(_layerId2, layerName2);
        WarnIfInvalidLayer(_layerId3, layerName3);

        // Apply the correct sprite immediately on start.
        ApplySpriteForLayer(gameObject.layer);
        _lastLayer = gameObject.layer;
    }

    private void Update()
    {
        int currentLayer = gameObject.layer;

        // Only do any work when the layer has actually changed.
        if (currentLayer == _lastLayer) return;

        ApplySpriteForLayer(currentLayer);
        _lastLayer = currentLayer;
    }

    // -----------------------------------------------------------------------
    // Helpers
    // -----------------------------------------------------------------------

    /// <summary>
    /// Looks up which sprite belongs to <paramref name="layer"/> and assigns it.
    /// If the layer doesn't match any of the four configured layers, the
    /// sprite is left unchanged and a warning is logged.
    /// </summary>
    private void ApplySpriteForLayer(int layer)
    {
        Sprite next = null;

        if      (layer == _layerId0) next = spriteForLayer0;
        else if (layer == _layerId1) next = spriteForLayer1;
        else if (layer == _layerId2) next = spriteForLayer2;
        else if (layer == _layerId3) next = spriteForLayer3;
        else
        {
            Debug.LogWarning($"[LayerSpriteSwapper] gameObject is on layer " +
                             $"'{LayerMask.LayerToName(layer)}' ({layer}), " +
                             $"which isn't mapped to a sprite. Sprite unchanged.");
            return;
        }

        if (next == null)
        {
            Debug.LogWarning($"[LayerSpriteSwapper] Sprite slot for layer " +
                             $"'{LayerMask.LayerToName(layer)}' is empty in the Inspector.");
            return;
        }

        _spriteRenderer.sprite = next;
    }

    private static void WarnIfInvalidLayer(int id, string name)
    {
        if (id == -1)
            Debug.LogError($"[LayerSpriteSwapper] Layer '{name}' not found. " +
                           $"Check spelling in the Inspector and in Edit > Project Settings > Tags & Layers.");
    }
}