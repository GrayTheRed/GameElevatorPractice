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
    public CarMoveDirection MoveDirection;
    public float CarWaitTimeSeconds = 3;
    private float tempWaitTimer;
    ElevatorTests Tests;
    private bool WaitForStop;

    private void Awake()
    {
        Queue.FloorQueue = new Dictionary<int, ElevatorFloor>();
        Queue.SetFloorQueue(ElevatorFloors);
    }
    // Start is called before the first frame update
    void Start()
    {
        //Queue.SetFloorQueue(ElevatorFloors);
        SetCar();
        tempWaitTimer = CarWaitTimeSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (IsTesting)
        {
            Tests = GetComponent<ElevatorTests>();
            Tests.CallCarToTestFloor();
            IsTesting = false;
        }

        if (WaitForStop)
        {
            CarWait();
        }
        else
        {
            if(!(Queue.IsQueueEmpty))
            {                
                int next = GetNextFloor();
                MoveCar(next);
            }
        }
    }

    private int GetNextFloor()
    {
        Debug.Log("Getting next floor");
        Vector3 nextLocation = ElevatorCar.transform.position;
        int nextFloor = Queue.GetNextFloor(MoveDirection);

        if(!(nextFloor == ElevatorCar.CurrentFloor))
        {
            ElevatorFloor floor = ElevatorFloors.Where(f => f.FloorNumber == nextFloor).First();
            nextFloor = floor.FloorNumber;
        }
        Debug.Log($"Next floor should be {nextFloor}");
        return nextFloor;
    }

    private void SetCar()
    {
        Vector3 startPosition = Queue.FloorQueue[ElevatorCar.CurrentFloor].transform.position;
        ElevatorCar.transform.position = startPosition;
    }

    private void MoveCar(int floorNumber)
    {

        Vector3 nextLocation = GetFloorPosition(floorNumber);
        ElevatorCar.transform.position = Vector3.MoveTowards(ElevatorCar.transform.position, nextLocation, ElevatorCar.MoveSpeed * Time.deltaTime);

        if(ElevatorCar.transform.position == nextLocation)
        {
            ElevatorCar.CurrentFloor = floorNumber;
            WaitForStop = true;
            ElevatorCar.ElevatorDoor.Open();
        }
    }

    private Vector3 GetFloorPosition(int floorNumber)
    {
        ElevatorFloor floor = ElevatorFloors.Where(f => f.FloorNumber == floorNumber).First();
        Vector3 floorPosition = floor.transform.position;
        return floorPosition;
    }

    private void CarWait()
    {
        tempWaitTimer -= Time.deltaTime;
        
        if(tempWaitTimer <= 0f)
        {
            tempWaitTimer = CarWaitTimeSeconds;
            WaitForStop = false;
        }
    }

    public void AddFloorToQueue(int floorNumber)
    {
        Queue.AddFloorToQueue(floorNumber);
    }

    public enum CarMoveDirection
    {
        Up,
        Down,
        None
    }
}
