using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public RoverDataObject dataobj;
    public GameObject playerrover;
    public GameObject weapons;
    public GameObject inventory;

    private void Start()
    {
        InventoryUI inv = inventory.GetComponent<InventoryUI>();
        WeaponsUI wep = weapons.GetComponent<WeaponsUI>();
        GameObject player = Instantiate(playerrover,dataobj.rover.position,dataobj.rover.rotation);
        Inventory playerinv = player.GetComponent<Inventory>();
        WeaponsInventory playerwep = player.GetComponent<WeaponsInventory>();
        playerinv.ui = inv;
        playerwep.ui = wep;
        inv.inv = playerinv;
        wep.guninv = playerwep;
    }
}
