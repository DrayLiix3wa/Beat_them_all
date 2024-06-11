using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityManager : MonoBehaviour
{
    private PlayerHitBoxManager hitBox;
    private SpriteFlash _spriteFlash;

    private void Awake()
    {
        hitBox = GetComponent<PlayerHitBoxManager>();
        _spriteFlash = GetComponent<SpriteFlash>();
    }

    public void StartInvincibility(float invincibilityDuration, Color flashColor, int numberOfFlashes)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration, flashColor, numberOfFlashes));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration, Color flashColor, int numberOfFlashes)
    {
        Debug.Log("StartInvincibilityCoroutine");
        hitBox.isInvincible = true;
        yield return _spriteFlash.FlashCoroutine(invincibilityDuration, flashColor, numberOfFlashes);
        hitBox.isInvincible = false;
    }
}
