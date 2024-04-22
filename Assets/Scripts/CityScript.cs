using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CityScript : MonoBehaviour
{
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int maxPopulations;
    public int maxJobs;
    public int incomePerJob;

    public TextMeshProUGUI statsText;

    private List<BuildingPresets> buildings = new List<BuildingPresets>();

    public static CityScript instance;

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
        
    }

    public void OnPlaceBuilding(BuildingPresets building)
    {
        maxPopulations += building.population;
        maxJobs += building.jobs;
        buildings.Add(building);
    }

    void CalculateMoney()
    {
        money += curJobs * incomePerJob;

        foreach(BuildingPresets building in buildings)
        {
            money -= building.costPerTurn;
        }
    }

    void CalculatePopulation()
    {
        maxPopulations = 0;

        foreach(BuildingPresets building in buildings)
        {
            maxPopulations += building.population; //setting populasi penduduk max nya dari rumah yang dibangun
        }

        if(curFood >= curPopulation && curPopulation < maxPopulations)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFood / 4), maxPopulations);
        }else if(curFood < curPopulation)
        {
            curPopulation = curFood;
        }
    }

    void CalculateJobs()
    {
        curJobs = 0;
        maxJobs = 0;

        foreach(BuildingPresets building in buildings)
        {
            maxJobs += building.jobs;
        }
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }

    void CalculateFood()
    {
        curFood = 0;

        foreach(BuildingPresets building in buildings)
        {
            curFood += building.food;
        }
    }

    public void EndTurn()
    {
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();

        statsText.text = string.Format("Day:{0} Money: Rp.{1} Pop: {2} / {3} Jobs: {4} / {5} Food: {6}", new object[7] { day, money, curPopulation, maxPopulations, curJobs, maxJobs, curFood });
    }
}
