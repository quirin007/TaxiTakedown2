using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveLv1 : MonoBehaviour
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

        if (loudness < threshold)
            loudness = 0;

        //Debug.Log(loudness);
float minLoudness = 0; // minimum loudness, adjust based on your needs
float maxLoudness = 20; // maximum loudness, adjust based on your needs

// Normalize the loudness value to a value between baseSpeed and maxSpeed
float newSpeed = ((loudness - minLoudness) / (maxLoudness - minLoudness)) * (maxSpeed - baseSpeed) + baseSpeed;

// Check and correct if new speed is below baseSpeed or above maxSpeed
if (newSpeed < baseSpeed) newSpeed = baseSpeed;
if (newSpeed > maxSpeed) newSpeed = maxSpeed;

        
        //transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
        currentSpeed = newSpeed; // apply new speed
    }

    private void FixedUpdate() 
    {
        // for movement
        transform.Translate(Vector2.up * currentSpeed * Time.fixedDeltaTime, Space.Self);

        // for rotation
        transform.Rotate(Vector3.forward * -velX * rotationSpeed * Time.fixedDeltaTime);
    }
}

