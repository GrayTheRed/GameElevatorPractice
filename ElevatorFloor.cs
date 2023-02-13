using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorFloor : MonoBehaviour
{
    public Elevator Elevator;
    public int FloorNumber;
    public bool WaitingForElevator = false;
    ElevatorFloorControls controller;
    ElevatorDoor door;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ElevatorFloorControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallCar()
    {
        Debug.Log("Call Car called on Elevator Floor");
        controller.AddToQueue(FloorNumber, Elevator);
    }
}
