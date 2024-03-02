using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPGShooting : MonoBehaviour
{
    private AudioSource au;
    public WeaponsUI ui;
    public Transform spawnpoint;
    public InventoryScriptableObjects invgun;
    public GameObject projectile;
    public AudioClip clip;
    public float reloadtime;
    public float ShootDelay = 0.5f;
    public float lastreloadtime;
    private float LastShootTime;

    private void Awake()
    {
        au = gameObject.GetComponent<AudioSource>();
        ui = GameObject.Find("Weapons").GetComponent<WeaponsUI>();
        invgun.ammocount = invgun.maxammo;
        invgun.magazinecount = invgun.maxmag;
        invgun.reloading = false;
        ui.ChangeMagazine();
        ui.ChangeAmmo();
    }
    private void Update()
    {
        if (lastreloadtime <= reloadtime)
            lastreloadtime += Time.deltaTime;
        else
        {
            invgun.reloading = false;
            ui.ChangeAmmo();
        }
        if (invgun.isselectedgun)
        {
            if (Input.GetKey(KeyCode.Mouse0) && invgun.ammocount > 0 && invgun.reloading == false)
                Shoot();
            if (Input.GetKeyDown(KeyCode.R))
                Reload();
        }
    }
    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            Instantiate(projectile, spawnpoint.position, spawnpoint.transform.rotation);
            LastShootTime = Time.time;
            invgun.ammocount--;
            au.PlayOneShot(clip);
        }
    }
    public void Reload()
    {
        if (lastreloadtime >= reloadtime && invgun.magazinecount > 0 && invgun.ammocount == 0 && invgun.reloading == false)
        {
            invgun.ammocount = invgun.maxammo;
            invgun.magazinecount -= 1;
            lastreloadtime = 0f;
            invgun.reloading = true;
        }
        ui.ChangeMagazine();
    }
}
