using UnityEngine;


public class GyroscopeRotation : MonoBehaviour
{
   
    [Tooltip("Multiply to speed up or slow down how fast the player follows the phone.")]
    [Range(1f, 30f)]
    public float smoothSpeed = 10f;

    [Tooltip("When true the rotation is mirrored (flip if the direction feels backwards).")]
    public bool invertRotation = false;

    [Tooltip("Degrees per second when testing in the Editor with keyboard.")]
    public float editorKeyboardSpeed = 90f;



    private bool _gyroAvailable;
    private Gyroscope _gyro;

    // The yaw angle (degrees) we are driving toward.
    private float _targetAngle;

    // Accumulated angle used only in the Editor keyboard fallback.
    private float _editorAngle;



    private void Start()
    {
        _gyroAvailable = SystemInfo.supportsGyroscope;

        if (_gyroAvailable)
        {
            _gyro = Input.gyro;
            _gyro.enabled = true;
            Debug.Log("[GyroscopeRotation] Gyroscope initialised.");
        }
        else
        {
            Debug.LogWarning("[GyroscopeRotation] No gyroscope found. " +
                             "Using keyboard fallback (A/D or arrow keys).");
        }
    }

    private void Update()
    {
        if (_gyroAvailable)
        {
            _targetAngle = ExtractYawDegrees(_gyro.attitude);
        }
        else
        {
            
            float input = 0f;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  input = -1f;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) input =  1f;

            _editorAngle += input * editorKeyboardSpeed * Time.deltaTime;
            _targetAngle  = _editorAngle;
        }

        ApplyRotation(_targetAngle);
    }




    private float ExtractYawDegrees(Quaternion attitude)
    {
 
        Quaternion corrected = ConvertGyroToUnity(attitude);

        
        Vector3 euler = corrected.eulerAngles;

  
        float yaw = Mathf.DeltaAngle(0f, euler.x);

        return invertRotation ? -yaw : yaw;
    }


    private static Quaternion ConvertGyroToUnity(Quaternion q)
    {
       
        Quaternion remapped = new Quaternion(q.x, q.z, q.y, -q.w);

    
        Quaternion baseline = Quaternion.Euler(90f, 0f, 0f);

        return baseline * remapped;
    }


    private void ApplyRotation(float angleDeg)
    {
        Quaternion target  = Quaternion.Euler(0f, 0f, -angleDeg);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            target,
            smoothSpeed * Time.deltaTime
        );
    }
}