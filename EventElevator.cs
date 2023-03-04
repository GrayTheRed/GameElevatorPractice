using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventElevator : MonoBehaviour
{
    [SerializeField] private EventElevatorCar Car;
    [SerializeField] private List<EventElevatorFloor> Floors;
    [SerializeField] private int CarFloorLocation;
    private bool IsQueueEmpty = true;
    private bool IsCarWaiting = false;
    private CarMoveDirection MoveDirection;
    private EventElevatorFloor NextFloor;

    private void Awake()
    {
        SetCarStartLocation();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!IsQueueEmpty && !IsCarWaiting)
        {
            MoveCar(NextFloor);
        }
    }


    void SetCarStartLocation()
    {
       Car.transform.position = Floors.Where(i => i.FloorNumber == CarFloorLocation).Select(k => k.transform.position).First();
    }

    void SetNextFloor()
    {
        int? nextFloor = NextFloorNumber();
        if (nextFloor == null)
        {
            ChangeCarDirection();
            nextFloor = NextFloorNumber();
        }
        NextFloor = Floors.Where(i => i.FloorNumber == nextFloor).FirstOrDefault();
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

    int? NextFloorNumber()
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
            CarArrival(floor);
        }
        else
        {
            Car.transform.position = Vector3.MoveTowards(Car.transform.position, nextPosition, Time.deltaTime);
        }
    }

    void CarArrival(EventElevatorFloor floor)
    {
        Debug.Log($"Car has arrived on floor {floor.FloorNumber}");
        IsCarWaiting = true;
        floor.DeactivateFloor();
        CarFloorLocation = floor.FloorNumber;
        StartCoroutine(LoadAndUnload());
        SetNextFloor();
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
        SetNextFloor();

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
            Debug.Log("Queue is empty");
            MoveDirection = CarMoveDirection.Stationary;
            NextFloor = null;
        }
    }

    IEnumerator LoadAndUnload()
    {
        yield return new WaitForSeconds(5);
        IsCarWaiting = false;
    }

    private enum CarMoveDirection
    {
        Up,
        Down,
        Stationary
    }
}
