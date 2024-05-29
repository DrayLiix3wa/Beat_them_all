using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Dirk Dynamite/VibrationManager")]
public class VibrationManager : MonoBehaviour
{
    public float defaultDuration = 0.5f;

    public void Vibrate( float intensity )
    {
        StartCoroutine(VibrateCoroutine( intensity ) );
    }

    private IEnumerator VibrateCoroutine( float intensity )
    {
        Gamepad.current.SetMotorSpeeds( intensity, intensity );
        yield return new WaitForSeconds( defaultDuration );

        StopVibration();
    }

    private void StopVibration()
    {
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
