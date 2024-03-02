using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public WeaponsUI ui;
    public InventoryScriptableObjects invgun;
    public float reloadtime;
    public float lastreloadtime;
    public bool reloading = false;
    public AudioClip sound;
    public AudioSource au;
    [SerializeField]
    private Transform BulletSpawnPoint;
    [SerializeField]
    
    private TrailRenderer BulletTrail;
    [SerializeField]
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask;
    [SerializeField]
    private float BulletSpeed = 100;

    private float LastShootTime;

    public void Awake()
    {
        ui = GameObject.Find("Weapons").GetComponent<WeaponsUI>();
        ui.ChangeMagazine();
        ui.ChangeAmmo();
    }
    public void Update()
    {

        if (lastreloadtime <= reloadtime)
            lastreloadtime += Time.deltaTime;
        else
        {
            reloading = false;
            ui.ChangeAmmo();
        }
        if(Input.GetKey(KeyCode.Mouse0) && invgun.ammocount > 0 && reloading == false)
            Shoot();
        if (Input.GetKeyDown(KeyCode.R)) 
            Reload();
    }


    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            // Use an object pool instead for these! To keep this tutorial focused, we'll skip implementing one.
            // For more details you can see: ntrhttps://youtu.be/fsDE_mO4RZM or if using Unity 2021+: https://youtu.be/zyzqA_CPz2E

            
            Vector3 direction = GetDirection();

            if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, 1000f, Mask))
            {
                GameObject trail  = ObjectPool.SharedInstance.GetPooledObject();
                if (trail != null)
                {
                    trail.transform.position = BulletSpawnPoint.transform.position;
                    trail.transform.rotation = Quaternion.identity;
                    trail.gameObject.SetActive(true);
                }

                StartCoroutine(SpawnTrail(trail.GetComponent<TrailRenderer>(), hit.point, hit.normal, true));

                LastShootTime = Time.time;

                invgun.ammocount--;
            }
            // this has been updated to fix a commonly reported problem that you cannot fire if you would not hit anything
            else
            {
                GameObject trail = ObjectPool.SharedInstance.GetPooledObject();
                if (trail != null)
                {
                    trail.transform.position = BulletSpawnPoint.transform.position;
                    trail.transform.rotation = Quaternion.identity;
                    trail.gameObject.SetActive(true);
                }
                StartCoroutine(SpawnTrail(trail.GetComponent<TrailRenderer>(), BulletSpawnPoint.position + GetDirection() * 1000, Vector3.zero, false));

                LastShootTime = Time.time;

                invgun.ammocount--;
            }
            au.PlayOneShot(sound);
            ui.ChangeAmmo();
        }
    }
    public void Reload()
    {
        if(lastreloadtime >= reloadtime && invgun.magazinecount > 0 && reloading == false)
        {
            invgun.ammocount = invgun.maxammo;
            invgun.magazinecount -= 1;
            lastreloadtime = 0f;
            reloading = true;
        }
        ui.ChangeMagazine();
    }
    private Vector3 GetDirection()
    {
        Vector3 direction = transform.up;

        

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        // This has been updated from the video implementation to fix a commonly raised issue about the bullet trails
        // moving slowly when hitting something close, and not
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        
        Trail.transform.position = HitPoint;


        StartCoroutine(RemoveAfterSeconds(Trail.time, Trail.gameObject)); ;
    }
    IEnumerator RemoveAfterSeconds(float seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
