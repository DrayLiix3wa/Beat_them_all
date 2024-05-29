using UnityEngine;
using System.Collections;

[AddComponentMenu("Dirk Dynamite/Camera Shake")]
public class CameraShake : MonoBehaviour
{
    #region Public Variables

    [Header("Shake Settings")]
    public AnimationCurve curve;

    public CameraFollow cameraFollow;

    [Space(10)]
    [Header("Debug")]
    public bool debugMode = false;

    #endregion

    #region Public Methods
    public void ShakeCamera(float shakeTime)
    {
        LogDebug("Shake Camera");
        StartCoroutine(Shake(shakeTime));
    }
    #endregion

    private IEnumerator Shake(float shakeTime)
    {
        float timer = 0f;

        while (timer < shakeTime)
        {
            timer += Time.deltaTime;
            float strength = curve.Evaluate(timer / shakeTime);

            cameraFollow.posOffset = Random.insideUnitCircle * strength;

            cameraFollow.posOffset.z = -10;

            yield return null;
        }

        cameraFollow.posOffset.x = 0;
        cameraFollow.posOffset.y = 0;

    }

    #region Private Methods
    private void LogDebug(string message)
    {
        if (debugMode)
        {
            Debug.Log(message);
        }
    }
    #endregion
}
