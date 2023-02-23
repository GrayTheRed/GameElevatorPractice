using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCarController : MonoBehaviour
{
    public ElevatorCar Car;
    public Elevator Elevator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToFloor(int floorNumber)
    {
        Elevator.AddFloorToQueue(floorNumber);
    }
}
