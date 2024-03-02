using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPGProjectile : MonoBehaviour
{
    public float muzzlevelocity;
    public GameObject boom;
    private Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.velocity = transform.forward * muzzlevelocity;
        Destroy(gameObject, 30f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        var boomba = Instantiate(boom, transform.position, transform.rotation);
        Destroy(boomba, 1f);
        Destroy(gameObject);
    }
}
