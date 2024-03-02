using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory inv;
    public GameObject[] slots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (inv.inventory[i] != null)
            {
                slots[i].GetComponent<TextMeshProUGUI>().text = inv.inventory[i].names;
            }
            else
            {
                slots[i].GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
}
