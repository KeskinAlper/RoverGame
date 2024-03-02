using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInventoryHandler : MonoBehaviour
{
    public GameStateObject gamestate;
    public InventoryHolderObject iddatabase;
    public InventoryScriptableObjects[] roverinv;
    public int[] roverids = new int[8];
    public InventoryHolderObject roverobj;
    public MapRoverUI roverui;
    public InventoryScriptableObjects[] shipinv;
    public int[] shipids = new int[14];
    public InventoryHolderObject shipobj;
    public MapRoverUI shipui;
    public InventoryScriptableObjects[] weapons;
    public int[] weaponsids = new int[4];
    public InventoryHolderObject weaponobj;
    public MapRoverUI weaponsui;
    public InventoryScriptableObjects turret;
    public int turretid;
    public InventoryHolderObject turretholder;
    public MapRoverUI turretui;
    private IDataService DataService = new JsonDataService();

    void Start()
    {
        if (gamestate.makingnewgame == false)
        {
            DataService.LoadData<int[]>("/roverinventory.json", false).CopyTo(roverids, 0);
            DataService.LoadData<int[]>("/shipinventory.json", false).CopyTo(shipids, 0);
            DataService.LoadData<int[]>("/weaponsinventory.json", false).CopyTo(weaponsids, 0);
            turretid = DataService.LoadData<int>("/playerturret.json", false);
            FillInventory(roverids, roverinv);
            FillInventory(shipids, shipinv);
            FillInventory(weaponsids, weapons);
            if(turretid != 0)
            turret = iddatabase.inventory[turretid];
        }
        roverinv.CopyTo(roverobj.inventory, 0);
        shipinv.CopyTo(shipobj.inventory, 0);
        weapons.CopyTo(weaponobj.inventory, 0);
        turretholder.inventory[0] = turret;
        roverui.ChangeUI();
        shipui.ChangeUI();
        weaponsui.ChangeUI();
        turretui.ChangeUI();
    }

    private void OnDisable()
    {
        roverobj.inventory.CopyTo(roverinv, 0);
        shipobj.inventory.CopyTo(shipinv, 0);
        weaponobj.inventory.CopyTo(weapons, 0);
        turret = turretholder.inventory[0];
        FillIDs(roverids, roverinv);
        FillIDs(shipids, shipinv);
        FillIDs(weaponsids, weapons);
        if(turret != null)
             turretid = turret.id;
        DataService.SaveData<int[]>("/roverinventory.json", roverids, false);
        DataService.SaveData<int[]>("/shipinventory.json", shipids, false);
        DataService.SaveData<int[]>("/weaponsinventory.json", weaponsids, false);
        DataService.SaveData<int>("/playerturret.json", turretid, false);
        
    }

    void Update()
    {

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
        for(int i = 0; i < id.Length; i++)
        {
            id[i] = 0;
            if (inv[i] != null)
            id[i] = inv[i].id;
        }
    }
        


}
