using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Tiles")]
public class Tiles : ScriptableObject
{
    public List<GameObject> tilePrefabs;
}
