using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PogoTest : MonoBehaviour
{
    [Header("Bounce")]
    
    public float bounceForce = 18f;

    
    public float chargeBonus = 8f;

    
    public float maxChargeTime = 0.4f;

    [Header("Ground Detection")]
    
    public LayerMask groundLayer;

    [Tooltip("Small downward offset for the ground-check ray origin, relative to collider bottom.")]
    public float groundCheckOffset = 0.05f;

    [Tooltip("Radius of the overlap circle used for ground detection.")]
    public float groundCheckRadius = 0.15f;

    [Header("Squash & Stretch (optional)")]
    
    public Transform pogoVisual;

    
    public float squashAmount = 0.35f;

    
    public float squashRecoverySpeed = 12f;

   
    private Rigidbody2D _rb;
    private Collider2D  _col;

    private bool  _isGrounded;
    private bool  _wasGrounded;          // grounded state last frame
    private float _chargeTimer;          // how long jump button has been held
    private bool  _holdingJump;

    private Vector3 _baseVisualScale;    // original scale of the visual child
    private float   _currentSquash = 1f; // 1 = normal

    
    void Awake()
    {
        _rb  = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();

        if (pogoVisual != null)
            _baseVisualScale = pogoVisual.localScale;
    }

    void Update()
    {
        // Track how long the jump button is held (for charge bonus)
        if (Input.GetButtonDown("Jump"))
        {
            _holdingJump  = true;
            _chargeTimer  = 0f;
        }

        if (_holdingJump)
            _chargeTimer += Time.deltaTime;

        if (Input.GetButtonUp("Jump"))
            _holdingJump = false;

        // Animate squash/stretch recovery every frame
        AnimateSquash();
    }

    void FixedUpdate()
    {
        _isGrounded = CheckGrounded();

        
        if (_isGrounded && !_wasGrounded)
        {
            Bounce();
        }

        _wasGrounded = _isGrounded;
    }

    
    void Bounce()
    {
        // Kill current vertical velocity so every bounce feels consistent
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

        // Calculate charge bonus (clamped to maxChargeTime)
        float chargeRatio  = Mathf.Clamp01(_chargeTimer / maxChargeTime);
        float totalForce   = bounceForce + chargeBonus * chargeRatio;

        _rb.AddForce((Vector2)transform.up * totalForce, ForceMode2D.Impulse);

        // Reset charge
        _chargeTimer = 0f;

        // Trigger squash
        TriggerSquash(chargeRatio);
    }

    
    bool CheckGrounded()
    {
        // Find the bottom centre of the collider
        Bounds bounds      = _col.bounds;
        Vector2 checkPoint = new Vector2(bounds.center.x,
                                         bounds.min.y - groundCheckOffset);

        return Physics2D.OverlapCircle(checkPoint, groundCheckRadius, groundLayer);
    }

    
    void TriggerSquash(float chargeRatio)
    {
        if (pogoVisual == null) return;

        // Stronger charge = more squash
        _currentSquash = 1f - squashAmount * (0.6f + 0.4f * chargeRatio);
    }

    void AnimateSquash()
    {
        if (pogoVisual == null) return;

        // Smoothly recover to 1
        _currentSquash = Mathf.Lerp(_currentSquash, 1f,
                                     Time.deltaTime * squashRecoverySpeed);

        // Keep volume: if Y squashes, X stretches, and vice-versa
        float yScale = _currentSquash;
        float xScale = 1f + (1f - _currentSquash) * 0.5f; // lateral bulge

        pogoVisual.localScale = new Vector3(
            _baseVisualScale.x * xScale,
            _baseVisualScale.y * yScale,
            _baseVisualScale.z
        );
    }

    
    void OnDrawGizmosSelected()
    {
        if (_col == null) _col = GetComponent<Collider2D>();
        if (_col == null) return;

        Bounds  bounds     = _col.bounds;
        Vector3 checkPoint = new Vector3(bounds.center.x,
                                          bounds.min.y - groundCheckOffset, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkPoint, groundCheckRadius);
    }
}