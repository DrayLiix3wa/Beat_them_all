using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public SO_Playlist playlist;

    [SerializeField]
    private bool debugMode = false;

    private AudioSource audioSource;
    private GameObject playerTarget;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            LogDebug("Pas de composant AudioSource trouvé, ajout d'un nouveau.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(PlayMusic());
    }

    private void Update()
    {
        if (playerTarget == null)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            transform.position = playerTarget.transform.position;
        }
    }

    private IEnumerator PlayMusic()
    {
        int index = 0;

        if (playlist.sounds.Length == 0)
        {
            LogDebug("La playlist est vide.");
            yield break;
        }

        while (true) 
        {
           SO_Playlist.Sound sound = playlist.sounds[index];
            
           if(sound.clip != null)
           {
                audioSource.PlayOneShot(sound.clip, sound.volume);
                LogDebug($"Le son : {sound.name} a été joué.");
                yield return new WaitForSecondsRealtime(sound.clip.length);
           }
           
            index = (index + 1) % playlist.sounds.Length;
        }
    }

    /// <summary>
    /// Log un message en mode debug si debugMode est activé.
    /// </summary>
    /// <param name="message">Le message à logger.</param>
    private void LogDebug(string message)
    {
        if (debugMode)
        {
            Debug.Log(message);
        }
    }
}
