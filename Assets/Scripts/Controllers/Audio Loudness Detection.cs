using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    private AudioClip microphoneClip;

    public int sampleWindow = 64;

    void Start ()
    {
        MicrophoneToAudioClip();
    }

    public void MicrophoneToAudioClip(){
                //Check if the mic is available
        if(Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("Microphone not connected!");
        } 
        else //At least one microphone is connected
        {
            //Set the default device for microphone input
            string mic = Microphone.devices[0];
            Debug.LogWarning(mic+"Microphone connected!");

            //Start recording (looping automatically)
            microphoneClip = Microphone.Start(mic, true, 20, AudioSettings.outputSampleRate);
            
        }
    }


    public float GetLoudnessFromMic(){
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);

    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip){
        int startPosition = clipPosition - sampleWindow;
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        // compute loudness
        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}