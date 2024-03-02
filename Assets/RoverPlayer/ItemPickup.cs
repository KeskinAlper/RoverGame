using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool entered = false;
    public GameObject obj;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        inv = gameObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        int val = 0;
        if(Input.GetKeyDown(KeyCode.E) && entered && obj != null)
        {
          val = inv.AddItem(obj.GetComponent<GroundItem>().item);
          if(val == 1)
          {
                Destroy(obj);
                obj = null;
                entered = false;
          }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        entered = true;
        obj = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        entered = false;
        obj = null;
    }
}
