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
        FloorQueue = new Dictionary<int, ElevatorFloor>();
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
        bool firstLoop = true;
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

            try
            {
                if (FloorQueue[temp].WaitingForElevator)
                {
                    nextFloor = temp;
                    keepLooping = false;
                    break;
                }
            }
            catch
            {
                Debug.Log("No floors for this");
            }

            keepLooping = 0 < temp && temp <= FloorQueue.Count;
        }

        return nextFloor;
    }

    public void AddFloorToQueue(int floor)
    {
        Debug.Log("AddFloorToQueue in elevator queue hit");
        FloorQueue[floor].WaitingForElevator = true;
    }

    public void SetFloorQueue(List<ElevatorFloor> floors)
    {
        foreach (ElevatorFloor f in floors)
        {
            string debugMessage = "Creating queue item for floor " + f.FloorNumber.ToString();
            Debug.Log(debugMessage);
            FloorQueue.Add(f.FloorNumber, f);
        }
    }

}
