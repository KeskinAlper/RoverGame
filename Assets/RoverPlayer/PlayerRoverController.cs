using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoverController : MonoBehaviour
{
    public WheelCollider rightf;
    public WheelCollider rightb;
    public WheelCollider leftf;
    public WheelCollider leftb;

    public Transform rfront;
    public Transform rbehind;
    public Transform lfront;
    public Transform lbehind;

    public float acceleration = 500f;
    public float breakingforce = 300f;
    public float maxturnangle = 15f;
    public float rpm = 0f;

    public AudioSource au;
    public Rigidbody rb;
    public float maxvelocity;
    public float minpitch;

    private float currentacc = 0f;
    private float currentbreak = 0f;
    private float currentturnangle = 0f;
    private void FixedUpdate()
    {
        currentacc = acceleration * Input.GetAxis("Vertical");
        if (rb.velocity.magnitude >= maxvelocity)
            currentacc = 0f;
        if (Input.GetKey(KeyCode.Space))
            currentbreak = breakingforce;
        else
            currentbreak = 0f;
        rpm = rightf.rpm;
        
            rightf.motorTorque = currentacc;
            rightb.motorTorque = currentacc;
            leftf.motorTorque = currentacc;
            leftb.motorTorque = currentacc;
        

        
            
        
        
        
        rightf.brakeTorque = currentbreak;
        rightb.brakeTorque = currentbreak;
        leftb.brakeTorque = currentbreak;
        leftf.brakeTorque = currentbreak;


        currentturnangle = maxturnangle * Input.GetAxis("Horizontal");
        rightf.steerAngle = currentturnangle;
        leftf.steerAngle = currentturnangle;

        if(rb.velocity.magnitude <= 0f)
        {
            au.pitch = minpitch;
        }
        else
        {
            au.pitch = minpitch + rb.velocity.magnitude / 50f;
        }
        //UpdateWheel(rightf, rfront);
       // UpdateWheel(rightb, rbehind);
       // UpdateWheel(leftb, lbehind);
       // UpdateWheel(leftf, lfront);
    }

   /* private void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }
    */
}
