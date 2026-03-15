using UnityEngine;


public class BasicRotation : MonoBehaviour
{
    
    public float rotationSpeed = 180f;

    void Update()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))  input =  1f;
        if (Input.GetKey(KeyCode.RightArrow)) input = -1f;

        if (input != 0f)
        {
            float angle = rotationSpeed * input * Time.deltaTime;
            transform.Rotate(0f, 0f, angle);
        }
    }
}