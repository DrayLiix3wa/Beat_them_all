using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelDoor", menuName = "Dirk Dynamite/LevelDoor")]
public class SO_Door : ScriptableObject
{
    public bool isOpen = false;
    public SO_Level level;
}
