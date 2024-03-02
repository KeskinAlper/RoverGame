using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvReseterOnNewGame : MonoBehaviour
{
    public InventoryHolderObject roverinv;
    public InventoryHolderObject weaponsinv;
    public InventoryHolderObject shipinv;
    public GameStateObject gameState;
    public MapRoverUI wepons;
    public MapRoverUI inv;
    public MapRoverUI ship;
    // Start is called before the first frame update
    void Start()
    {
        if(gameState.makingnewgame == true)
        {
            ResetInv(roverinv);
            inv.ChangeUI();
            ResetInv(weaponsinv);
            wepons.ChangeUI();
            ResetInv(shipinv);
            ship.ChangeUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetInv(InventoryHolderObject inv)
    {
        for(int i = 0; i < inv.inventory.Length; i++)
        {
            inv.inventory[i] = null;
        }
    }
}
