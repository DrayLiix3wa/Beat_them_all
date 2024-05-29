using UnityEngine;

[CreateAssetMenu(fileName = "New Playlist", menuName = "Dirk Dynamite/Playlist")]
public class SO_Playlist : ScriptableObject
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
    }

    public string playlistName;
    public Sound[] sounds;

}