using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapRoverUI : MonoBehaviour
{
    public InventoryHolderObject inv;
    public MapRoverUI destui;
    public MapRoverUI turretui;
    public MapRoverUI weaponui;
    public MapRoverUI roverui;
    public GameObject[] slots;
    private void Awake()
    {
        
            ChangeUI();
        
    }
    public void ChangeUI()
    {
        for (int i = 0; i < inv.inventory.Length; i++)
        {
            if (inv.inventory[i] != null)
            {
                slots[i].GetComponent<TextMeshProUGUI>().text = inv.inventory[i].names;
            }
            else
            {
                slots[i].GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    public void Click(int slotnum)
    {
        if (destui.AddItem(inv.inventory[slotnum]) == true)
        {
            inv.inventory[slotnum] = null;
            ChangeUI();
        }
    }
    public void ShipClick(int slotnum)
    {
        if (inv.inventory[slotnum].isturret == true)
        {
            destui = turretui;
        }
        else if (inv.inventory[slotnum].gunobj != null)
        {
            destui = weaponui;
        }
        else
        {
            destui = roverui;
        }
        Click(slotnum);
    }
    public bool AddItem(InventoryScriptableObjects item)
    {
        for(int i = 0; i < inv.inventory.Length; i++)
        {
            if (inv.inventory[i] == null)
            {
                inv.inventory[i] = item;
                ChangeUI();
                return true;
            }
        }
        return false;
    }

}
