using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RangeFinder : MonoBehaviour
{
    public TextMeshProUGUI text;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position,transform.forward,out hit,2000f))
        {
            text.text = "" + (int)(Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            text.text = "^^^^";
        }
    }
}
