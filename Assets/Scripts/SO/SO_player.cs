using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Player", menuName = "Dirk Dynamite/Player")]
public class SO_player : ScriptableObject
{
    public string playerName;

    public int health;
    public int maxHealth;

    public int damage;
    public int maxDamage;

    public int stamina;
    public int maxStamina;

}
