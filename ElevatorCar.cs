using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCar : MonoBehaviour
{
    public GameObject CarObj;
    public float MoveTo;
    public float MoveSpeed;
    public bool IsMoving;
    public MoveDirection moveDirection;
    public int CurrentFloor;
    Vector3 currentPosition;
    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = CarObj.transform.position;
        nextPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsMoving)
        {
            Move(nextPosition);
        }
    }


    public void Move(Vector3 loc)
    {
        Debug.Log("Move called in ElevatorCar");
        CarObj.transform.position = Vector3.MoveTowards(CarObj.transform.position, loc, MoveSpeed * Time.deltaTime);
    }

    public void SendCar(Vector3 loc)
    {
        if(loc != currentPosition)
        {
            Debug.Log("SendCar location not current position");
            nextPosition = loc;
            IsMoving = true;
        }
    }

    public void Stop()
    {
        IsMoving = false;
        moveDirection = MoveDirection.None;
    }

    public void ChangeDirection()
    {
        switch (moveDirection)
        {
            case ElevatorCar.MoveDirection.Up:
                moveDirection = MoveDirection.Down;
                break;
            case ElevatorCar.MoveDirection.Down:
                moveDirection = MoveDirection.Up;
                break;
            default:
                break;
        }
    }

    public enum MoveDirection
    {
        Up,
        Down,
        None
    }
}
