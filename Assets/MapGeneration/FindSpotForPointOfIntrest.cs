using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSpotForPointOfIntrest : MonoBehaviour
{
    public GameStateObject scenestate;
    public Transform[] placestrans;
    public Vector3[] places;
    public Vector3[] pointsofintrest;
    public float[] pos;
    public GameObject[] setpieces;
    public int amountofplaces;
    private IDataService DataService = new JsonDataService();
    private void Start()
    {
        Random.InitState((int)Time.time);
        for(int i = 0; i < places.Length; i++)
        {
            places[i] = placestrans[i].position;
        }
        if(scenestate.makingnewgame == true)
        {
            SetAllPOI();
            SpawnPOI();
        }
        else 
        {
            LoadPOI();
            SpawnPOI();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SavePOI();
        }
    }
    public void SetAllPOI()
    {
        Vector3 place;
        int placetoerase;
        for(int i = 0; i < amountofplaces;i++)
        {
            do
            {
                placetoerase = (int)(Random.Range(0f, places.Length));
                place = places[placetoerase];
            } while (place == new Vector3(0f,0f,0f));
            places[placetoerase] = new Vector3(0f, 0f, 0f);
            pointsofintrest[i] = place;           
            
        }
    }
    public void SpawnPOI()
    {
        for(int i = 0; i < amountofplaces; i++)
        {
            Instantiate(setpieces[(int)Random.Range(0f, setpieces.Length)], pointsofintrest[i], Quaternion.identity);
        }
    }
    public void SavePOI()
    {
        for (int i = 0; i < amountofplaces; i++)
        {
            pos[0] = pointsofintrest[i].x;
            pos[1] = pointsofintrest[i].y;
            pos[2] = pointsofintrest[i].z;
            DataService.SaveData<float[]>("/pointofintrest" + i, pos, false);
        }
    }
    public void LoadPOI()
    {
        for(int i= 0; i < amountofplaces; i++)
        {
            pos = DataService.LoadData<float[]>("/pointofintrest" + i, false);
            pointsofintrest[i].Set(pos[0], pos[1], pos[2]);
        }
    }
}
