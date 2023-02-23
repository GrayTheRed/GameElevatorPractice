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
    private int CarWaitTimeSeconds = 3;

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

        if (ElevatorCar.IsMoving)
        {
           
        }
        else
        {
            int next = GetNextFloor();

            if (next != ElevatorCar.CurrentFloor)
            {
                MoveCar(next);
            }
        }
    }

    private int GetNextFloor()
    {
        Vector3 nextLocation = ElevatorCar.transform.position;
        ElevatorCar.MoveDirection direction = ElevatorCar.moveDirection;
        int nextFloor = Queue.GetNextFloor(direction);

        if(nextFloor == ElevatorCar.CurrentFloor)
        {
            
        }
        else
        {
            ElevatorFloor floor = ElevatorFloors.Where(f => f.FloorNumber == nextFloor).First();
            nextFloor =  floor.FloorNumber;
        }

        return nextFloor;
    }

    private void SetCar()
    {
        Vector3 startPosition = Queue.FloorQueue[ElevatorCar.CurrentFloor].transform.position;
        ElevatorCar.transform.position = startPosition;
    }

    private void MoveCar(Vector3 location)
    {
        ElevatorCar.transform.position = Vector3.MoveTowards(ElevatorCar.transform.position, location, ElevatorCar.MoveSpeed * Time.deltaTime);
    }

    private void MoveCar(int floorNumber)
    {
        ElevatorFloor floor = ElevatorFloors.Where(f => f.FloorNumber == floorNumber).First();
        Vector3 nextLocation = floor.transform.position;
        ElevatorCar.transform.position = Vector3.MoveTowards(ElevatorCar.transform.position, nextLocation, ElevatorCar.MoveSpeed * Time.deltaTime);

        if(ElevatorCar.transform.position == nextLocation)
        {
            ElevatorCar.CurrentFloor = floorNumber;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(CarWaitTimeSeconds);
    }

    private void CarWait()
    {
        
    }

    public void AddFloorToQueue(int floorNumber)
    {
        Queue.AddFloorToQueue(floorNumber);
    }

    private enum CarMoveDirection
    {
        Up,
        Down,
        None
    }
}
