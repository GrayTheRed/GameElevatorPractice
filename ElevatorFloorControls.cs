using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorFloorControls : MonoBehaviour
{
    public ElevatorFloor Floor;
    public Elevator Elevator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToQueue(int floorNumber)
    {
        Elevator.AddFloorToQueue(floorNumber);
    }
}
