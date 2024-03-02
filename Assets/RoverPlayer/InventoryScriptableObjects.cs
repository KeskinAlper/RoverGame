using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
[System.Serializable]
public class InventoryScriptableObjects : ScriptableObject
{
    public string names;
    public int id;
    public bool isselectedgun = false;
    public GameObject prefabobj;
    public bool isturret;
    public GameObject gunobj;
    public float crosshairsideoffset;
    public float crosshairheightoffset;
    public float ammocount;
    public float magazinecount;
    public float maxammo;
    public float maxmag;
    public bool reloading;
}
