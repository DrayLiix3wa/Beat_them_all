using UnityEngine;
using System.Collections;

[AddComponentMenu("Dirk Dynamite/Camera Shake")]
public class CameraShake : MonoBehaviour
{
    #region Public Variables

    [Header("Shake Settings")]
    public AnimationCurve curve;
    public float ShakeTime = 1f;

    [Space(10)]
    [Header("Debug")]
    public bool debugMode = false;

    #endregion

    #region Private Variables
    private Transform _transformCamera;
    private Vector3 _initialOffset;

    #endregion

    #region Unity Lifecycle
    void Awake()
    {
        _transformCamera = Camera.main.transform;
        _initialOffset = transform.position - _transformCamera.position;
    }
    #endregion

    #region Public Methods
    public void ShakeCamera()
    {
        LogDebug("Shake Camera");
        StartCoroutine(Shake());
    }
    #endregion

    private IEnumerator Shake()
    {
        float timer = 0f;

        while (timer < ShakeTime)
        {
            timer += Time.deltaTime;
            float strength = curve.Evaluate(timer / ShakeTime);

            transform.position = _transformCamera.position + _initialOffset + Random.insideUnitSphere * strength;

            yield return null;
        }

        transform.position = _transformCamera.position + _initialOffset;
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
