using System.Collections;
using UnityEngine;

/// <summary>
/// Composant StaminaManager pour gérer l'endurance d'un joueur ou d'un personnage
/// </summary>
[AddComponentMenu("Dirk Dynamite/StaminaManager")]
[DisallowMultipleComponent]
public class StaminaManager : MonoBehaviour
{
    #region Public Variables
    [Header("Stamina Settings")]
    [Tooltip("Valeur maximale de l'endurance")]
    [Range(0f, 100f)]
    public int max = 100;
    [Tooltip("Valeur actuelle de l'endurance")]
    public int current;
    [Tooltip("Temps de recharge de l'endurance (en secondes)")]
    [Range(0f, 10f)]
    public float cooldown = 5f;
    [Tooltip("Taux de régénération de l'endurance (par seconde)")]
    [Range(0f, 20f)]
    public float regenRate = 10f;

    [Space(10)]

    [Header("Debug")]
    [Tooltip("Active le mode debug pour afficher les logs dans la console")]
    public bool debugMode = false;
    #endregion

    #region Private Variables
    [SerializeField]
    private float _chrono = 0f;
    #endregion

    #region Unity Lifecycle

    /// <summary>
    /// Initialisation de l'endurance au démarrage du jeu au niveau de sa valeur maximale
    /// </summary>
    void Start()
    {
        current = max;
        LogDebug( "La stamina a été initialisée à " + current );
    }

    void Update()
    {
        if ( current < max )
        {
            _chrono += Time.deltaTime;
            if ( _chrono >= cooldown )
            {
                LogDebug( "Temps de recharge écoulé - lancement de la régénération de la stamina" );
                StartCoroutine( Regen() );
                _chrono = 0f;
            }
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Fonction pour consommer de l'endurance (ex: sprint, attaque spéciale, etc.)
    /// </summary>
    /// <param name="cost"> Coût en endurance à consommer </param>
    /// <remarks> Retourne true si l'endurance est suffisante pour la consommation, false sinon et lance la fonction Not_Enough </remarks>
    public bool Consume( int cost )
    {
        if ( current >= cost )
        {
            current -= cost;
            _chrono = 0f;
            LogDebug( "Stamina consommée " + cost + ". Stamina actuelle : " + current );
            return true;
        }
        else
        {
            Not_Enough();
            return false;
        }
    }

    /// <summary>
    /// Fonction de test pour consommer de l'endurance via l'inspecteur Unity
    /// </summary>
    [ContextMenu( "Consume Stamina Test (20)" )]
    public bool TestConsume()
    {
        return Consume( 20 );
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Fonction de régénération de l'endurance via une coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator Regen()
    {
        while ( current < max )
        {
            current += 1;
            if ( current > max )
            {
                current = max;
            }
            LogDebug( "Régénération de la stamina. Stamina actuelle: " + current );
            yield return new WaitForSeconds( regenRate );
        }
    }

    /// <summary>
    /// Fonction appelée si l'endurance n'est pas suffisante pour la consommation (pour l'instant un simple log)
    /// </summary>
    private void Not_Enough()
    {
        LogDebug( "Pas assez de stamina pour la consommation" );
    }

    /// <summary>
    /// Fonction de log en mode debug pour afficher des messages dans la console Unity
    /// </summary>
    /// <param name="message"> Réceptionne le message à afficher </param>
    /// <remarks> Cette fonction n'est appelée que si le mode debug est activé </remarks>
    private void LogDebug( string message )
    {
        if ( debugMode )
        {
            Debug.Log( message );
        }
    }
    #endregion
}
