using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score Modifier Powerup", menuName = "Powerups")]
public class Upgrades : ScriptableObject
{     
    //This holds the durations for the upgraded levels, index 0 = not upgraded, index 1 = level 1, etc.
    public List<float> upgradedDurations;
    public List<int> upgradeCosts;


}
