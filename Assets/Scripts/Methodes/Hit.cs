using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dirk Dynamite/Hit")]
[DisallowMultipleComponent]

public class Hit : MonoBehaviour
{
    public BoxCollider2D weakStrikeHitBox;
    public BoxCollider2D strongStrikeHitBox;

    public void WeakStrikeActivation()
    {
        weakStrikeHitBox.enabled = true;
    }

    public void StrongStrikeActivation()
    {
        strongStrikeHitBox.enabled = true;
    }

    public void WeakStrikeDeactivate()
    {
        weakStrikeHitBox.enabled = false;
    }
    public void StrongStrikeDeactivate()
    {
        strongStrikeHitBox.enabled = false;
    }
}
