using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Elevator : MonoBehaviour, IElevator
{
    //private ElevatorQueue queue;
    //public ElevatorCar elevatorCar;
    //public List<ElevatorFloor> elevatorFloors;

    public ElevatorCar ElevatorCar { get; set; }
    public ElevatorQueue Queue { get; set; }
    public List<ElevatorFloor> ElevatorFloors { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(ElevatorCar.IsMoving))
        {
            Vector3 next = GetNextLocation();

            if( next != ElevatorCar.transform.position)
            {
                ElevatorCar.SendCar(next);
            }
        }
    }

    public Vector3 GetNextLocation()
    {
        Vector3 nextLocation = ElevatorCar.transform.position;
        ElevatorCar.MoveDirection direction = ElevatorCar.moveDirection;
        int nextFloor = Queue.GetNextFloor(direction);

        if(nextFloor == ElevatorCar.CurrentFloor)
        {
            ElevatorCar.ChangeDirection();
        }
        else
        {
            ElevatorFloor floor = ElevatorFloors.Where(f => f.FloorNumber == nextFloor).First();
            nextLocation =  floor.transform.position;
        }

        return nextLocation;
    }
}
