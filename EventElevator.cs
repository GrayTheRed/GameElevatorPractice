using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventElevator : MonoBehaviour
{
    public EventElevatorCar Car;
    public List<EventElevatorFloor> Floors;
    public int CarFloorLocation;
    private bool IsQueueEmpty = true;
    private bool IsCarWaiting = false;
    private CarMoveDirection MoveDirection;

    private void Awake()
    {
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
            tempList = Floors.Where(f => f.IsWaiting == true && f.FloorNumber > CarFloorLocation).Select(n => n.FloorNumber).OrderBy(i => i).ToList();

        }
        else if (MoveDirection == CarMoveDirection.Down)
        {
            tempList = Floors.Where(f => f.IsWaiting == true && f.FloorNumber < CarFloorLocation).Select(n => n.FloorNumber).OrderByDescending(i => i).ToList();
        }

        if(tempList.Count > 0)
        {
            nextFloor = tempList.First();
        }

        return nextFloor;
    }

    EventElevatorFloor GetFloor(int floorNumber)
    {
        EventElevatorFloor floor = Floors.Where(x => x.FloorNumber == floorNumber).FirstOrDefault();
        return floor;
    }

    void MoveCar(EventElevatorFloor floor)
    {
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
        int countOfWaiting = Floors.Where(i => i.IsWaiting == true).Count();
        return countOfWaiting > 0;
    }

    public void AddToQueue(int floor)
    {
        Debug.Log($"Adding floor {floor} to queue");
        EventElevatorFloor eleFloor = GetFloor(floor);
        eleFloor.IsWaiting = true;

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


    public void RemoveFromQueue (int floor)
    {
        Debug.Log($"Removing floor {floor} from queue");
        EventElevatorFloor eleFloor = GetFloor(floor);
        eleFloor.IsWaiting = false;

        IsQueueEmpty = !(FloorsWaiting());

        if (IsQueueEmpty)
        {
            Debug.Log("no more items in queue");
            MoveDirection = CarMoveDirection.Stationary;
        }
    }

    IEnumerator LoadAndUnload()
    {
        yield return new WaitForSeconds(5);
        IsCarWaiting = false;
    }

    public enum CarMoveDirection
    {
        Up,
        Down,
        Stationary
    }
}
