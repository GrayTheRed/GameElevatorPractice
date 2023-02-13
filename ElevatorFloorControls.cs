using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorFloorControls : MonoBehaviour
{
    public ElevatorFloor Floor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToQueue(int floor, Elevator elevator)
    {
        elevator.Queue.AddFloorToQueue(floor);
    }
}
