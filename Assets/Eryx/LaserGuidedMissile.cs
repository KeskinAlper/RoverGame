using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGuidedMissile : MonoBehaviour
{
    public Camera cam;
    public GameObject boom;
    public float speed;
    private RaycastHit hit;
    private Rigidbody rigid;
    public float RotationSpeed;
    private Quaternion _lookRotation;
    private Vector3 vectortoadd;
    public float vectortoaddmult;
    private Vector3 _direction;
    private Vector3 velocity = Vector3.zero;
    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        cam = Camera.main;
        Destroy(gameObject, 60f);
    }
    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * speed;
        if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,1000f) && hit.collider.gameObject != gameObject)
        {
            //find the vector pointing from our position to the target
            _direction = (hit.point - rigid.position).normalized;
            vectortoadd = (_direction - rigid.transform.forward).normalized;
            _direction = rigid.transform.forward + vectortoadd * vectortoaddmult;
            //Vector3.SmoothDamp(rigid.transform.forward, _direction, ref velocity, smoothTime);
            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);
            
            //rotate us over time according to speed until we are in the required rotation
            //rigid.rotation = Quaternion.Slerp(rigid.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
            rigid.rotation = _lookRotation;
        }
        else
        {
            _direction = ((cam.transform.position + cam.transform.forward * 1000f) - rigid.position).normalized;
            //Vector3.SmoothDamp(rigid.transform.forward, _direction, ref velocity, smoothTime);
            vectortoadd = (_direction - rigid.transform.forward).normalized;
            _direction = rigid.transform.forward + vectortoadd * vectortoaddmult;
            _lookRotation = Quaternion.LookRotation(_direction);
            //rigid.rotation = Quaternion.Slerp(rigid.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
            rigid.rotation = _lookRotation;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        Destroy(gameObject);
        var boomba = Instantiate(boom, transform.position, transform.rotation);
        Destroy(boomba,1f);
    }
}
