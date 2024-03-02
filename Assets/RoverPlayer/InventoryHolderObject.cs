using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data",menuName = "ScriptableObjects/Inventory",order = 2)]
public class InventoryHolderObject : ScriptableObject
{
    public InventoryScriptableObjects[] inventory;
}
