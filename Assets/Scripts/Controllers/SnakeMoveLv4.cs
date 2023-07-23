using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveLv4 : MonoBehaviour
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
private bool isTurning = false;

private void Update()
{
    velX = Input.GetAxisRaw("Horizontal");
    float maxLoudness = 12;
    float loudness = detector.GetLoudnessFromMic() * loudnessSensitivity;

    if (loudness < threshold)
    {
        // If loudness is below the threshold, keep the snake moving forward
        isTurning = false;
        transform.Translate(Vector2.up * currentSpeed * Time.deltaTime, Space.Self);
    }
    else
    {
        // If loudness is above the threshold, make the snake turn right
        isTurning = true;

        // Normalize the loudness value to a value between 0 and 1
        float normalizedLoudness = Mathf.Clamp((loudness - threshold) / (maxLoudness - threshold), 0f, 1f);

        // Calculate the rotation amount based on the normalized loudness and the rotationSpeed
        float rotationAmount = normalizedLoudness * rotationSpeed;

        // Rotate the snake accordingly
        transform.Rotate(Vector3.forward * -rotationAmount * Time.deltaTime);
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

