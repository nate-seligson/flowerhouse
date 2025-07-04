using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public static bool orderReady = false;

    public static CustomerMove customer;
    public static void GenerateOrder(CustomerMove c)
    {
        customer = c;
        orderReady = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PickupHandler.held_object == null) return;
        if (
            orderReady &&
            PickupHandler.held_object.TryGetComponent<Plant>(out Plant p) &&
            Vector3.Distance(PlayerController.player.transform.position, transform.position) < 2 &&
            Input.GetKeyDown(KeyCode.E)
            )
        {
            completeOrder(p);
        }
    }
    void completeOrder(Plant p)
    {
        Destroy(PickupHandler.held_object);
        PickupHandler.held_object = null;
        customer.Remove();
        Debug.Log(PlantAnalysis.GetPlantScore(p));
    }
}
