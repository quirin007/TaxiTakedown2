using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    public float baseSpeed = 3f; // The "normal" speed
    public float maxSpeed = 20f; // The maximum speed when sound is very loud
    public float rotationSpeed = 200f;
    private float currentSpeed = 3f;

    public AudioSource source;
    public Vector3 minScale;
    public Vector3 maxScale;
    public AudioLoudnessDetection detector;

    public float loudnessSensitivity = 100;
    public float threshold = 4.5f;

    float velX = 0f;
private void Update()
{
    velX = Input.GetAxisRaw("Horizontal");

    float loudness = detector.GetLoudnessFromMic() * loudnessSensitivity;

    float minLoudness = 0; // minimum loudness, adjust based on your needs
    float maxLoudness = 12; // maximum loudness, adjust based on your needs


 if (loudness < threshold)
    {
        // If loudness is below the threshold, keep the snake moving forward
        transform.Translate(Vector2.up * currentSpeed * Time.deltaTime, Space.Self);
    }
    else
    {
        // If loudness is above the threshold, rotate the snake

        // Normalize the loudness value to a value between -1 and 1
        float normalizedLoudness = Mathf.Clamp((loudness - minLoudness) / (maxLoudness - minLoudness) * 2 - 1, -1f, 1f);

        // Calculate the rotation direction based on the normalized loudness
        float rotationDirection = Mathf.Sign(normalizedLoudness);

        // Calculate the rotation amount based on the normalized loudness
        float rotationAmount = Mathf.Abs(normalizedLoudness) * rotationSpeed;

        // Rotate the snake accordingly
        transform.Rotate(Vector3.forward * -rotationDirection * rotationAmount * Time.deltaTime);
    }
}


    private void FixedUpdate() 
    {
        // for movement
        transform.Translate(Vector2.up * currentSpeed * Time.fixedDeltaTime, Space.Self);

        // for rotation
        transform.Rotate(Vector3.forward * -velX * rotationSpeed * Time.fixedDeltaTime);
    }
}

