using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dirk Dynamite/Hit")]
[DisallowMultipleComponent]

public class Hit : MonoBehaviour
{
    public GameObject weakStrikeHitBox;
    public GameObject strongStrikeHitBox;

    public void WeakStrikeActivation()
    {
        weakStrikeHitBox.SetActive(true);
    }

    public void StrongStrikeActivation()
    {
        strongStrikeHitBox.SetActive(true);
    }

    public void WeakStrikeDeactivate()
    {
        weakStrikeHitBox.SetActive(false);
    }
    public void StrongStrikeDeactivate()
    {
        strongStrikeHitBox.SetActive(false);
    }
}
