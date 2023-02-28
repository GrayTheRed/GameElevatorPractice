using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventElevator : MonoBehaviour
{
    public EventElevatorCar Car;
    public List<EventElevatorFloor> Floors;
    public int CarFloorLocation;
    private Dictionary<int, bool> FloorWaitStatus;
    private bool IsQueueEmpty = true;
    private bool IsCarWaiting = false;
    private CarMoveDirection MoveDirection;

    private void Awake()
    {
        SetFloorWaitStatus();
        CarFloorLocation = 2;
        SetCarStartLocation();
    }
    // Start is called before the first frame update
    void Start()
    {
        // These are for testing and will be removed later
        Floors[0].ActivateFloor();
        Floors[2].ActivateFloor();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsQueueEmpty && !IsCarWaiting)
        {
            Debug.Log("There are floors waiting and we should be moving");
            EventElevatorFloor nextFloor = GetNextFloor();
            MoveCar(nextFloor);
        }
    }

    void SetFloorWaitStatus()
    {
        FloorWaitStatus = new Dictionary<int, bool>();
        foreach (var floor in Floors)
        {
            FloorWaitStatus.Add(floor.FloorNumber, false);
        }
    }

    void SetCarStartLocation()
    {
       Car.transform.position = Floors.Where(i => i.FloorNumber == CarFloorLocation).Select(k => k.transform.position).First();
    }

    EventElevatorFloor GetNextFloor()
    {
        int? nextFloor = NextFloor();
        if(nextFloor == null)
        {
            ChangeCarDirection();
            nextFloor = NextFloor();
        }
        return Floors.Where(i => i.FloorNumber == nextFloor).FirstOrDefault();
    }

    void ChangeCarDirection()
    {
        if (MoveDirection == CarMoveDirection.Up)
        {
            MoveDirection = CarMoveDirection.Down;
        }
        else if (MoveDirection == CarMoveDirection.Down)
        {
            MoveDirection = CarMoveDirection.Up;
        }
    }

    int? NextFloor()
    {
        int? nextFloor = null;
        List<int> tempList = new List<int>();

        if (MoveDirection == CarMoveDirection.Up)
        {
            tempList = FloorWaitStatus.Where(k => k.Value == true && k.Key > CarFloorLocation).Select(k => k.Key).OrderBy(x => x).ToList();

        }
        else if (MoveDirection == CarMoveDirection.Down)
        {
            tempList = FloorWaitStatus.Where(k => k.Value == true && k.Key < CarFloorLocation).Select(k => k.Key).OrderByDescending(x => x).ToList();
        }

        if(tempList.Count > 0)
        {
            nextFloor = tempList.First();
        }

        return nextFloor;
    }

    void MoveCar(EventElevatorFloor floor)
    {
        // should move the car closer to goal
        Vector3 nextPosition = floor.transform.position;
        if(nextPosition == Car.transform.position)
        {
            Debug.Log("Arrived");
            CarArrival(floor);
        }
        else
        {
            Car.transform.position = Vector3.MoveTowards(Car.transform.position, nextPosition, Time.deltaTime);
        }
    }

    void CarArrival(EventElevatorFloor floor)
    {
        IsCarWaiting = true;
        floor.DeactivateFloor();
        CarFloorLocation = floor.FloorNumber;
        StartCoroutine(LoadAndUnload());
    }

    bool FloorsWaiting()
    {
        return FloorWaitStatus.ContainsValue(true);
    }

    public void AddToQueue(int floor)
    {
        //set floor to active
        // if car is not moving, set to moving

        Debug.Log($"Adding floor {floor} to queue");
        FloorWaitStatus[floor] = true;

        if (IsQueueEmpty)
        {
            IsQueueEmpty = false;
        }

        if(MoveDirection == CarMoveDirection.Stationary)
        {
            if(floor > CarFloorLocation)
            {
                MoveDirection = CarMoveDirection.Up;
            }
            else
            {
                MoveDirection = CarMoveDirection.Down;
            }
        }
    }

    IEnumerator LoadAndUnload()
    {
        yield return new WaitForSeconds(5);
        IsCarWaiting = false;
    }

    public void RemoveFromQueue (int floor)
    {
        // set floor to inactive
        // check for any more active floors
        // if none, set is car moving to false
        Debug.Log($"Removing floor {floor} from queue");
        FloorWaitStatus[floor] = false;
        IsQueueEmpty = !(FloorsWaiting());

        if (IsQueueEmpty)
        {
            Debug.Log("no more items in queue");
            MoveDirection = CarMoveDirection.Stationary;
        }
    }

    public enum CarMoveDirection
    {
        Up,
        Down,
        Stationary
    }
}
