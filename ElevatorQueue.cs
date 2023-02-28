using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorQueue : MonoBehaviour
{
    public Dictionary<int, ElevatorFloor> FloorQueue;
    public ElevatorCar ElevatorCar;
    public bool IsQueueEmpty = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetNextFloor(ref Elevator.CarMoveDirection direction)
    {
        int nextFloor = FindWaitingFloor(direction); 
        
        if(nextFloor == ElevatorCar.CurrentFloor)
        {
            direction = Reverse(direction);
            nextFloor = FindWaitingFloor(direction);
        }
        
        IsQueueEmpty = nextFloor == ElevatorCar.CurrentFloor;
        return nextFloor;
    }

    private int FindWaitingFloor(Elevator.CarMoveDirection direction)
    {
        int nextFloor = ElevatorCar.CurrentFloor;
        int temp = ElevatorCar.CurrentFloor;
        bool keepLooping = true;
        while (keepLooping)
        {
            switch (direction)
            {
                case Elevator.CarMoveDirection.Up:
                    temp += 1;
                    break;
                case Elevator.CarMoveDirection.Down:
                    if (temp > 1)
                    {
                        temp -= 1;
                    }
                    break;
                default:
                    return nextFloor;
            }

            if (FloorQueue[temp].WaitingForElevator)
            {
                nextFloor = temp;
                return nextFloor;
            }

            keepLooping = 0 < temp && temp < FloorQueue.Count;
        }
        return nextFloor;
    }

    public void AddFloorToQueue(int floor)
    {
        Debug.Log("AddFloorToQueue in elevator queue hit");
        FloorQueue[floor].WaitingForElevator = true;
        IsQueueEmpty = false;
    }

    public void RemoveFloorFromQueue(int floor)
    {
        Debug.Log($"Removing floor {floor} from queue");
        //FloorQueue[floor].WaitingForElevator = false;
    }

    public void SetFloorQueue(List<ElevatorFloor> floors)
    {
        foreach (ElevatorFloor f in floors)
        {
            Debug.Log($"Creating queue item for floor {f.FloorNumber}");
            FloorQueue.Add(f.FloorNumber, f);
        }
    }

    private Elevator.CarMoveDirection Reverse(Elevator.CarMoveDirection direction)
    {
        switch (direction)
        {
            case Elevator.CarMoveDirection.Up:
                return Elevator.CarMoveDirection.Down;
            case Elevator.CarMoveDirection.Down:
                return Elevator.CarMoveDirection.Up;
            default:
                return Elevator.CarMoveDirection.None;
        }
    }
}
