using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Dirk Dynamite/VibrationManager")]
public class VibrationManager : MonoBehaviour
{
    public void Vibrate( float duration, float intensity )
    {
        StartCoroutine(VibrateCoroutine( duration, intensity ) );
    }

    private IEnumerator VibrateCoroutine( float duration, float intensity )
    {
        Gamepad.current.SetMotorSpeeds( intensity, intensity );
        yield return new WaitForSeconds( duration );

        StopVibration();
    }

    private void StopVibration()
    {
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
