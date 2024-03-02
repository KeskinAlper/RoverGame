using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponsUI : MonoBehaviour
{
    public WeaponsInventory guninv;
    public GameObject[] slots;
    public GameObject[] ammocounts;
    public GameObject[] magazinecounts;
    public GameObject[] outlines;
    public GameObject[] rs;
    public GameObject[] crosshairpieces;
    public GameObject[] crosshairpiecesl;

    private void Awake()
    {
        ChangeOutline();
    }
    private void Update()
    {
        DisplayReload();
    }
    public void ChangeUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (guninv.wepninv.inventory[i] != null)
                slots[i].GetComponent<TextMeshProUGUI>().text = guninv.wepninv.inventory[i].names;
            else
                slots[i].GetComponent<TextMeshProUGUI>().text = "";
        }
    }
    public void ChangeAmmo()
    {
        for (int i = 0; i < ammocounts.Length; i++)
        {
            if (guninv.wepninv.inventory[i] != null)
                ammocounts[i].GetComponent<TextMeshProUGUI>().text = guninv.wepninv.inventory[i].ammocount.ToString();
            else
                ammocounts[i].GetComponent<TextMeshProUGUI>().text = "";
        }
    }
    public void ChangeMagazine()
    {
        for (int i = 0; i < ammocounts.Length; i++)
        {
            if (guninv.wepninv.inventory[i] != null)
                magazinecounts[i].GetComponent<TextMeshProUGUI>().text = guninv.wepninv.inventory[i].magazinecount.ToString();
            else
                magazinecounts[i].GetComponent<TextMeshProUGUI>().text = "";
        }
    }
    public void ChangeOutline()
    {
        for(int i = 0; i < guninv.wepninv.inventory.Length; i++)
        {
            if (guninv.wepninv.inventory[i] != null)
            {
                if (guninv.wepninv.inventory[i].isselectedgun)
                {
                    outlines[i].SetActive(true);
                }
                else
                {
                    outlines[i].SetActive(false);
                }
            }
        }
    }
    public void DisplayReload()
    {
        for(int i = 0; i < guninv.wepninv.inventory.Length; i++)
        {
            if (guninv.wepninv.inventory[i] != null)
            {
                if (guninv.wepninv.inventory[i].reloading)
                {
                    rs[i].SetActive(true);
                }
                else
                {
                    rs[i].SetActive(false);
                }
            }
        }
    }
    public void SetCrosshair(float sideoffset, float heightoffset)
    {
       var initialheightoffset = heightoffset;
        var initialsideoffset = sideoffset/6;
        for(int i = 0; i < crosshairpieces.Length; i++)
        {
            crosshairpieces[i].transform.SetLocalPositionAndRotation(new Vector3(sideoffset, heightoffset, 0f),Quaternion.identity);
            crosshairpiecesl[i].transform.SetLocalPositionAndRotation(new Vector3( - sideoffset, heightoffset, 0f), Quaternion.identity);
            heightoffset += initialheightoffset;
            sideoffset -= initialsideoffset;
            initialsideoffset -= initialsideoffset / 6;
            Debug.Log("crosshairsetting");
        }
    }
}
