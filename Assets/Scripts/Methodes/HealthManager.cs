using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composant HealthManager pour gérer la santé d'un joueur ou d'un personnage
/// </summary>
[AddComponentMenu("Dirk Dynamite/HealthManager")]
[DisallowMultipleComponent]
public class HealthManager : MonoBehaviour
{
    #region Public Variables
    [Header( "Health Settings" )]
    [Tooltip( "Valeur maximale de la santé" )]
    [Range(0, 100)]
    public int max = 100;
    public int current;

    [Space( 10 )]

    [Header( "Debug" )]
    [Tooltip( "Active le mode debug pour afficher les logs dans la console" )]
    public bool debugMode = false;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Lifecycle

    void Start()
    {
        current = max;
        LogDebug( "La santé a été initialisée à " + current );
    }
    #endregion

    #region Public Methods

    public void OnEnable()
    {
        ResetHealth();
    }

    public void Hurt( int damage )
    {
        if ( current > 0 )
        {
            if ( current - damage < 0 )
            {
                current = 0;
                LogDebug( "Points de vie à zero" );
            }
            else
            {
                current -= damage;
                LogDebug( "A subi " + damage + " points de dégâts" );
            }
        }
    }

    public void Regen( int heal )
    {
        if ( current < max )
        {
            if ( current + heal > max )
            {
                current = max;
                LogDebug( "Points de vie au maximum" );
            }
            else
            {
                current += heal;
                LogDebug( "A regagné " + heal + " points de vie" );
            }
        }
        LogDebug("Points de vie au maximum");
        return;
    }

    /// <summary>
    /// Fonction de test pour donner de la santé via le menu contextuel de l'éditeur Unity
    /// </summary>
    [ContextMenu("Test Regen (20)")]
    public void TestRegen() {
        Regen( 20 );
    }

    /// <summary>
    /// Fonction de test pour infliger des dégâts via le menu contextuel de l'éditeur Unity
    /// </summary>
    [ContextMenu( "Test Hurt (20)" )]
    public void TestHurt()
    {
        Hurt( 20 );
    }

    #endregion

    #region Private Methods

    void ResetHealth()
    {
        current = max;
        LogDebug( "La santé a été réinitialisée à " + current );
    }

    /// <summary>
    /// Fonction de log en mode debug pour afficher des messages dans la console Unity
    /// </summary>
    /// <param name="message"> Réceptionne le message à afficher </param>
    /// <remarks> Cette fonction n'est appelée que si le mode debug est activé </remarks>
    private void LogDebug(string message)
    {
        if (debugMode)
        {
            Debug.Log(message);
        }
    }
    #endregion
}
