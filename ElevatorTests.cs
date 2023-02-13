using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTests : MonoBehaviour
{
    public GameObject newElevator;
    public int testFloor;
    Elevator elevator;    
    // Start is called before the first frame update
    void Start()
    {
        elevator = newElevator.GetComponent<Elevator>();
    }

    // Update is called once per frame
    void Update()
    {
      
            
        
    }

    public void CallCarToTestFloor()
    {
        try
        {
            Debug.Log("Testing Call Car");
            elevator.Queue.FloorQueue[testFloor].CallCar();
        }
        catch
        {
            Debug.Log("Unable to find the floor in the queue");
        }
    }
}
