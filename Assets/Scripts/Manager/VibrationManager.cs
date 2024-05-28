using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationManager : MonoBehaviour
{
    public void Vibrate(float duration, float intensity)
    {
        StartCoroutine(VibrateCoroutine( duration, intensity ) );
    }

    private IEnumerator VibrateCoroutine(float duration, float intensity)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            Gamepad.current.SetMotorSpeeds(intensity, intensity);

            yield return null;
        }

        StopVibration();
    }

    private void StopVibration()
    {
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
