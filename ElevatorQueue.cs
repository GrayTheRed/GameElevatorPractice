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

    public int GetNextFloor(Elevator.CarMoveDirection direction)
    {
        int currentFloor = ElevatorCar.CurrentFloor;
        int nextFloor = currentFloor;
        int temp = currentFloor;
        bool keepLooping = true;
        bool firstLoop = true;
        while (keepLooping)
        {
            switch (direction)
            {
                case Elevator.CarMoveDirection.Up:
                    temp += 1;
                    break;
                case Elevator.CarMoveDirection.Down:
                    temp -= 1;
                    break;
                default:
                    if (firstLoop)
                    {
                        Debug.Log("GetNextFloor checking up");
                        temp += 1;
                    }
                    else
                    {
                        Debug.Log("GetNextFloor checking down");
                        temp -= 1;
                    }
                    break;
            }

            if (FloorQueue[temp].WaitingForElevator)
            {
                nextFloor = temp;
                return nextFloor;
            }

            keepLooping = 0 < temp && temp < FloorQueue.Count;
        }
        IsQueueEmpty = nextFloor == currentFloor;
        return nextFloor;
    }

    public void AddFloorToQueue(int floor)
    {
        Debug.Log("AddFloorToQueue in elevator queue hit");
        FloorQueue[floor].WaitingForElevator = true;
        IsQueueEmpty = false;
    }

    public void SetFloorQueue(List<ElevatorFloor> floors)
    {
        foreach (ElevatorFloor f in floors)
        {
            Debug.Log($"Creating queue item for floor {f.FloorNumber}");
            FloorQueue.Add(f.FloorNumber, f);
        }
    }
}
