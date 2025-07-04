using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnalysis : MonoBehaviour
{
    public static float GetPlantScore(Plant plant)
    {
        if (plant.dead) return 0;

        return ((plant.lifetime - plant.timer) / plant.lifetime) * 5;
    }
}
