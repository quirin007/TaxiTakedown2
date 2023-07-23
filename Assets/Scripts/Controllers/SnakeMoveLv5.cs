using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeMoveLv5 : MonoBehaviour
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

    private float velX = 0f;
    private bool isTurning = false;

    private void Update()
    {
        // Handle arrow keys to turn the snake
        velX = Input.GetAxisRaw("Horizontal");
        if (velX != 0)
        {
            isTurning = true;
            // Rotate the snake accordingly
            transform.Rotate(Vector3.forward * -velX * rotationSpeed * Time.deltaTime);
        }
        else
        {
            isTurning = false;
        }

        float maxLoudness = 12;
        float loudness = detector.GetLoudnessFromMic() * loudnessSensitivity;

        // Calculate the normalized loudness within the range [0, 1]
        float normalizedLoudness = Mathf.Clamp01((loudness - threshold) / (maxLoudness - threshold));

        // Calculate the speed reduction factor based on the normalized loudness
        float speedReduction = 1f - normalizedLoudness;

        // Apply the speed reduction to the currentSpeed
        currentSpeed = baseSpeed + speedReduction * (maxSpeed - baseSpeed);

        // Move the snake forward at the currentSpeed
        transform.Translate(Vector2.up * currentSpeed * Time.deltaTime, Space.Self);
    }
}
