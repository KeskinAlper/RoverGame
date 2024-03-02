using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMainGun : MonoBehaviour
{
    public InventoryHolderObject iddatabase;
    public Transform turretspot;
    public InventoryScriptableObjects turret;
    public TurretUI ui;
    public int turretid;
    private IDataService DataService = new JsonDataService();

    private void Start()
    {
        turretid = DataService.LoadData<int>("/playerturret.json", false);
        if (turretid != 0)
        {
            GameObject spawnedturret;
            turret = iddatabase.inventory[turretid];
            spawnedturret = Instantiate(turret.gunobj, turretspot);
            spawnedturret.GetComponent<TurretShootingProjectile>().ui = ui;
            spawnedturret.GetComponent<TurretShootingProjectile>().ui.ChangeUI();
            ui.ChangeUI();
            ui.SetCrosshair(turret.crosshairheightoffset);
        }
    }
    private void OnDisable()
    {
        turretid = 0;
        if (turret != null)
        {
            turretid = turret.id;
            DataService.SaveData<int>("/playerturret.json", turretid, false);
        }
    }
}
