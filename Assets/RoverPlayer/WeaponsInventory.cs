using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public InventoryHolderObject iddatabase;
    public int equipedweapon = 1;
    public int slotcount;
    public Transform[] turretspots;
    public WeaponsUI ui;
    public InventoryScriptableObjects[] weaponsinv;
    public int[] weaponsinvids = new int[4];
    public InventoryScriptableObjects test;
    public InventoryHolderObject wepninv;
    private IDataService DataService = new JsonDataService();
    public void AddGun(InventoryScriptableObjects gun)
    {
        for(int i = 0; i < slotcount; i++)
        {
            if (weaponsinv[i] == null)
            {
                weaponsinv[i] = gun;
                Instantiate(weaponsinv[i].gunobj, turretspots[i]);
                ui.ChangeUI();
                break;
            }
        }
    }
    public void Start()
    {
        DataService.LoadData<int[]>("/weaponsinventory.json", false).CopyTo(weaponsinvids,0);
        for(int i = 0; i < weaponsinv.Length; i++)
        {
            if (weaponsinvids[i] != 0)
            AddGun(iddatabase.inventory[weaponsinvids[i]]);
        }
        DeselectAllGuns();
    }
    private void OnDisable()
    {
        FillIDs(weaponsinvids, weaponsinv);
        DataService.SaveData<int[]>("/weaponsinventory.json", weaponsinvids, false);
    }
    public void Update()
    {
        if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                equipedweapon = 1;
                Selectgun(0);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                equipedweapon = 2;
                Selectgun(1);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                equipedweapon = 3;
                Selectgun(2);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                equipedweapon = 4;
                Selectgun(3);
            }

        }
    }
    public void Selectgun(int index)
    {
        for(int i = 0; i < weaponsinv.Length; i++)
        {
            if (weaponsinv[i] != null)
                weaponsinv[i].isselectedgun = false;
        }
        ui.SetCrosshair(0f, 0f);
        if (weaponsinv[index] != null)
        {
            weaponsinv[index].isselectedgun = true;
            ui.SetCrosshair(weaponsinv[index].crosshairsideoffset, weaponsinv[index].crosshairheightoffset);
        }
        ui.ChangeOutline();
    }
    public void DeselectAllGuns()
    {
        for (int i = 0; i < wepninv.inventory.Length; i++)
        {
            if (weaponsinv[i] != null)
                weaponsinv[i].isselectedgun = false;
        }
        ui.ChangeOutline();
        ui.SetCrosshair(0f, 0f);
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
