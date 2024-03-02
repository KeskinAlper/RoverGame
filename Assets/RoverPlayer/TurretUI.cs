using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TurretUI : MonoBehaviour
{
    public TurretMainGun maingun;
    public TextMeshProUGUI gunsname;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI mag;
    public TextMeshProUGUI r;
    public GameObject[] crosshairpieces;
    public void ChangeUI()
    {
        if (maingun.turret != null)
        {
            gunsname.text = maingun.turret.names;
            ammo.text = "" + maingun.turret.ammocount;
            mag.text = "" + maingun.turret.magazinecount;
            if(maingun.turret.reloading == true)
            {
                r.text = "R";
            }
            else
            {
                r.text = "";
            }
        }
    }
    public void SetCrosshair(float heightoffset)
    {
        var initialheightoffset = heightoffset;
        for (int i = 0; i < crosshairpieces.Length; i++)
        {
            crosshairpieces[i].transform.SetLocalPositionAndRotation(new Vector3(0f, heightoffset, 0f), Quaternion.identity);
            heightoffset += initialheightoffset;
            Debug.Log("turretcrosshairsetting");
        }
    }
}
