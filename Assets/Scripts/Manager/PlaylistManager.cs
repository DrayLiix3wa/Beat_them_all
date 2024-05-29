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
        // Ajoute un composant AudioSource si aucun n'est déjà présent
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            LogDebug("Pas de composant AudioSource trouvé, ajout d'un nouveau.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Joue un son aléatoire à partir d'une playlist spécifiée par son nom.
    /// </summary>
    /// <param name="playlistName">Le nom de la playlist.</param>
    public void PlayRandomSound(string playlistName)
    {
        SO_Playlist playlist = playlists.Find(pl => pl.name == playlistName);

        if (playlist != null && playlist.sounds.Length > 0)
        {
            // Sélectionne un son aléatoire dans la playlist
            SO_Playlist.Sound sound = playlist.sounds[Random.Range(0, playlist.sounds.Length)];

            audioSource.PlayOneShot(sound.clip, sound.volume);

            LogDebug($"Le son : {sound.name} a été joué.");
        }
        else
        {
            LogDebug($"Playlist {playlistName} introuvable ou vide.");
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
