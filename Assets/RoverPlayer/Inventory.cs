using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryScriptableObjects[] inventory ;
    public InventoryHolderObject iddatabase;
    public int[] inventoryids = new int[8];
    public InventoryUI ui;
    private IDataService DataService = new JsonDataService();
    public void Start()
    {
       DataService.LoadData<int[]>("/roverinventory.json", false).CopyTo(inventoryids,0);
        FillInventory(inventoryids, inventory);
        ui.ChangeUI();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            FillIDs(inventoryids, inventory);
            DataService.SaveData<int[]>("/roverinventory.json", inventoryids, false);
        }
    }
    public int AddItem(InventoryScriptableObjects item)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                ui.ChangeUI();
                return 1;
            }
        }
        return 0;
    }
    public void FillInventory(int[] id, InventoryScriptableObjects[] inv)
    {
        for (int i = 0; i < inv.Length; i++)
        {
            if (id[i] != 0)
                inv[i] = iddatabase.inventory[id[i]];
        }
    }
    public void FillIDs(int[] id, InventoryScriptableObjects[] inv)
    {
        for (int i = 0; i < id.Length; i++)
        {
            id[i] = 0;
            if (inv[i] != null)
                id[i] = inv[i].id;
        }
    }
}
