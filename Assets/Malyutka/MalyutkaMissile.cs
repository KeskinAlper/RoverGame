using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalyutkaMissile : MonoBehaviour
{
    public float speed;
    public GameObject boom;
    public float turnamount;
    public float turnspeed;
    public Rigidbody rigid;
    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, 60f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = -transform.forward * speed;
        var rotation = rigid.rotation;
        if (Input.anyKey)
        {          
            if(Input.GetKey(KeyCode.H))
            {
                rotation *= Quaternion.Euler(0f, turnamount, 0f);
            }
            else if(Input.GetKey(KeyCode.K))
            {
                rotation *= Quaternion.Euler(0f, -turnamount, 0f);
            }
            if(Input.GetKey(KeyCode.U))
            {
                rotation *= Quaternion.Euler(-turnamount, 0f, 0f);
            }
            if(Input.GetKey(KeyCode.J))
            {
                rotation *= Quaternion.Euler(turnamount, 0f, 0f);
            }
        }
        rigid.rotation = Quaternion.Slerp(rigid.rotation, rotation, Time.deltaTime * turnspeed);

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        var boomba = Instantiate(boom, transform.position, transform.rotation);
        Destroy(boomba, 1f);
        Destroy(gameObject);
    }
}
