using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "Cars")]
public class Car : ScriptableObject
{
    public string carName;

    public int carCost;

    public List<Material> colorOptions;
}

