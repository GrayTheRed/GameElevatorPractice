using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorQueue : MonoBehaviour
{
    public Dictionary<int, ElevatorFloor> FloorQueue;
    public ElevatorCar ElevatorCar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetNextFloor(ElevatorCar.MoveDirection direction)
    {
        int currentFloor = ElevatorCar.CurrentFloor;
        int nextFloor = currentFloor;
        int temp = currentFloor;
        bool keepLooping = true;
        while (keepLooping)
        {
            switch (direction)
            {
                case ElevatorCar.MoveDirection.Up:
                    temp += 1;
                    break;
                case ElevatorCar.MoveDirection.Down:
                    temp -= 1;
                    break;
                default:
                    keepLooping = false;
                    break;
            }

            if (FloorQueue[nextFloor].WaitingForElevator)
            {
                nextFloor = temp;
                keepLooping = false;
            }

            keepLooping = 0 < temp && temp <= FloorQueue.Count;
        }

        return nextFloor;
    }

    public void AddFloorToQueue(int floor)
    {
        FloorQueue[floor].WaitingForElevator = true;
    }

    public void SetFloorQueue(List<ElevatorFloor> floors)
    {
        foreach (ElevatorFloor f in floors)
        {
            FloorQueue.Add(f.FloorNumber, f);
        }
    }

}
