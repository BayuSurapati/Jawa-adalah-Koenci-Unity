using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    private bool currentlyPlacing;
    private BuildingPresets curBuildingPresets;

    private float placementIndicatorUpdateRate = 0.05f;
    private float lastUpdateTime;
    private Vector3 curPlacementPos;

    public GameObject placementIndicator;
    public static BuildingPlacer instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuildingPlacement();
        }

        if(Time.time - lastUpdateTime > placementIndicatorUpdateRate && currentlyPlacing)
        {
            lastUpdateTime = Time.time;
            curPlacementPos = Selector.instance.GetCurTilePosition();
            placementIndicator.transform.position = curPlacementPos;
        }

        if(currentlyPlacing && Input.GetMouseButton(0))
        {
            PlaceBuilding();
        }
    }

    public void BeginNewBuildingPlacement (BuildingPresets buildingPresets)
    {
        if(CityScript.instance.money < buildingPresets.cost)
        {
            return;
        }
        currentlyPlacing = true;
        curBuildingPresets = buildingPresets;
        placementIndicator.SetActive(true);
    }

    public void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }

    public void DestroyBuildingPlacement()
    {
        currentlyPlacing = false;
    }

    void PlaceBuilding()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);   
        GameObject buildingObj = Instantiate(curBuildingPresets.prefab, curPlacementPos, Quaternion.Euler(rot));
        CityScript.instance.OnPlaceBuilding(curBuildingPresets);

        CancelBuildingPlacement();
    }
}
