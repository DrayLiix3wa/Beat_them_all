using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composant Move pour g�rer le d�placement d'un joueur ou d'un personnage
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Dirk Dynamite/Move")]
[DisallowMultipleComponent]
public class Move : MonoBehaviour
{

    #region Public Variables
    [Header("Move Settings")]
    [Tooltip("Vitesse de d�placement du joueur/personnage")]
    [Range(0f, 10f)]
    public float speed = 5f;

    [Tooltip("Rigidbody du joueur/personnage")]
    public Rigidbody2D rb;

    [Space(10)]

    [Header("Debug")]
    [Tooltip("Active le mode debug pour afficher les logs dans la console")]
    public bool debugMode = false;
    #endregion

    #region Private Variables

    #endregion

    [SerializeField]
    [Tooltip("Direction du d�placement du joueur/personnage")]
    private Vector2 _direction;

    #region Unity Lifecycle

    #endregion

    #region Public Methods

    public void MoveProcess( Vector2 direction )
    {
        _direction = direction;

        if ( rb == null )
        {
            LogDebug( "Le Rigidbody n'a pas �t� assign�" );
            return;
        }

        LogDebug( "D�placement du joueur vers la direction " + direction );
        rb.velocity = new Vector3( direction.x * speed, rb.velocity.y, direction.y * speed );
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Fonction de log en mode debug pour afficher des messages dans la console Unity
    /// </summary>
    /// <param name="message"> R�ceptionne le message � afficher </param>
    /// <remarks> Cette fonction n'est appel�e que si le mode debug est activ� </remarks>
    private void LogDebug(string message)
    {
        if (debugMode)
        {
            Debug.Log(message);
        }
    }
    #endregion
}

