using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public ElevatorCar ElevatorCar;
    public ElevatorQueue Queue;
    public List<ElevatorFloor> ElevatorFloors;    
    public bool IsTesting = false;
    ElevatorTests Tests;

    // Start is called before the first frame update
    void Start()
    {
        Queue.SetFloorQueue(ElevatorFloors);
        SetCar();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTesting)
        {
            Tests = GetComponent<ElevatorTests>();
            Tests.CallCarToTestFloor();
            IsTesting = false;
        }

        if (!(ElevatorCar.IsMoving))
        {
            Vector3 next = GetNextLocation();

            if (next != ElevatorCar.transform.position)
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

    private void SetCar()
    {
        Vector3 startPosition = Queue.FloorQueue[ElevatorCar.CurrentFloor].transform.position;
        ElevatorCar.transform.position = startPosition;
    }
}
