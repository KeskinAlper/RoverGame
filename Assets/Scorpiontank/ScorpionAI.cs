using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScorpionAI : MonoBehaviour
{
    public Transform campos;
    public GameObject turret;
    public GameObject projectile;
    public Transform muzzle;
    private Rigidbody rb;
    private NavMeshAgent agent;
    public float distancetodisable;


    public float maxvelocity;

   

    public float spotbar;
    public float spottingspeed;
    
    public float fieldOfViewAngle = 90f;
    public float viewDistance = 700f;

    private Transform player;
   

    public float ShootDelay = 0.5f;
    public float LastShootTime;
    public bool spottedplayer = false;

    public float speed;
    public float brakespeed;
    public float RotationSpeed;
    private Quaternion _lookRotation;
    private Vector3 vectortoadd;
    public float vectortoaddmult;
    private Vector3 _direction;
    private RaycastHit slopeHit;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb.velocity = Vector3.zero;
    }
    public void FixedUpdate()
    {
        


        Scan();
        
        
        
         if (spotbar >= 100f && spottedplayer)
        {
            Attack();
            RotateToPoint(player.position);
            Brake();
        }
        else if(spottedplayer)
        {
            RotateToPoint(player.position);
            Vector3 delta = (player.position - rb.position).normalized;
            if (Vector3.Angle(rb.transform.forward, delta) <= 10f)
            {
                //currsteer = 0f;
                MoveForward();
            }
            ReleaseBrake();
        }

    }
     public void RotateToPoint(Vector3 point)
    {
        Vector3 delta = (point - rb.position).normalized;
        Vector3 cross = Vector3.Cross(delta, rb.transform.forward);
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, Quaternion.LookRotation((player.transform.position - turret.transform.position).normalized), Time.deltaTime * 10f);
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.LookRotation(delta), Time.deltaTime * RotationSpeed);
    }
    public void MoveForward()
    {

        //rb.velocity = (player.transform.position - turret.transform.position).normalized * speed;
         if(OnSlope())
        {
            Vector3 slopedir = Vector3.ProjectOnPlane(transform.forward, slopeHit.normal);
            rb.AddForce((slopedir * speed));
        }
         else
        {
            rb.AddForce((player.transform.position - turret.transform.position).normalized * speed * 2f);
        }
        
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 2f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public void Brake()
    {
        rb.velocity -= rb.velocity.normalized * brakespeed * Time.deltaTime; 
    }
    public void ReleaseBrake()
    {
        
    }

    public void Scan()
    {
        float distance = Vector3.Distance(campos.position, player.transform.position);
            if(CanSeePlayer() == true)
            {
               spotbar += Time.deltaTime * spottingspeed / distance * 20;          
            }
            else
            {
                spotbar -= Time.deltaTime * spottingspeed / 0.2f;
            }
       spotbar = Mathf.Clamp(spotbar, 0f, 100f);
        if(spotbar >= 100f)
        {
            spottedplayer = true;
        }
        
    }
    
    
    public void Attack()
    {
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, Quaternion.LookRotation((player.transform.position - turret.transform.position).normalized), Time.deltaTime * 20f);
        Vector3 directionToPlayer = player.GetComponent<Rigidbody>().position - campos.position;
        if (LastShootTime + ShootDelay < Time.time)// && Vector3.Angle(turret.transform.forward,directionToPlayer) < 0.5f)
            {
                Instantiate(projectile, muzzle.position, muzzle.rotation);
                LastShootTime = Time.time;
                
            }
    }
    
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
 
    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.GetComponent<Rigidbody>().position - campos.position;
        float angleToPlayer = Vector3.Angle(campos.forward, directionToPlayer);

        if (angleToPlayer < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
           if (Physics.Raycast(campos.position, directionToPlayer, out hit, viewDistance))
            {
                Debug.DrawRay(campos.position, directionToPlayer);
                if (hit.collider.gameObject.CompareTag("Player"))
                {

                    return true;
                }
            }
        }
        return false;
    }
    
    
}

