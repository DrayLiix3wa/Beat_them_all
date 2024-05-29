using System.Collections.Generic;
using UnityEngine;

public class PlaylistManager : MonoBehaviour
{
    [SerializeField]
    private List<SO_Playlist> playlists = new List<SO_Playlist>();

    [SerializeField]
    private bool debugMode = false;

    private AudioSource audioSource;

    private void Awake()
    {
        // Ajoute un composant AudioSource si aucun n'est d�j� pr�sent
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            LogDebug("Pas de composant AudioSource trouv�, ajout d'un nouveau.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Joue un son al�atoire � partir d'une playlist sp�cifi�e par son nom.
    /// </summary>
    /// <param name="playlistName">Le nom de la playlist.</param>
    public void PlayRandomSound(string playlistName)
    {
        SO_Playlist playlist = playlists.Find(pl => pl.name == playlistName);

        if (playlist != null && playlist.sounds.Length > 0)
        {
            // S�lectionne un son al�atoire dans la playlist
            SO_Playlist.Sound sound = playlist.sounds[Random.Range(0, playlist.sounds.Length)];

            audioSource.PlayOneShot(sound.clip, sound.volume);

            LogDebug($"Le son : {sound.name} a �t� jou�.");
        }
        else
        {
            LogDebug($"Playlist {playlistName} introuvable ou vide.");
        }
    }

    /// <summary>
    /// Log un message en mode debug si debugMode est activ�.
    /// </summary>
    /// <param name="message">Le message � logger.</param>
    private void LogDebug(string message)
    {
        if (debugMode)
        {
            Debug.Log(message);
        }
    }
}
