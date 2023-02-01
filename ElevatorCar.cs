using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCar : MonoBehaviour
{
    public GameObject CarObj;
    public float MaxHeight;
    public float MoveTo;
    public float MoveSpeed;
    public bool IsMoving;
    public MoveDirection moveDirection;
    Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = CarObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        currentPosition = CarObj.transform.position;
        float spd = MoveSpeed;
        if (moveDirection == MoveDirection.Down)
        {
            Debug.Log("Should be going down now");
            spd = -Mathf.Abs(spd);
        }

        if ((moveDirection == MoveDirection.Down && currentPosition.y > MoveTo) || (moveDirection == MoveDirection.Up && currentPosition.y <= MoveTo))
        {
            currentPosition.y += spd;
            CarObj.transform.position = currentPosition;
        }
        else
        {
            IsMoving = false;
            moveDirection = MoveDirection.None;
        }
    }

    public void SendCar(float floor)
    {
        Debug.Log("Sending the car");

        MoveTo = floor;

        if(floor < currentPosition.y)
        {
            Debug.Log("sending down");
            moveDirection = MoveDirection.Down;
        }
        else if (floor > currentPosition.y)
        {
            Debug.Log("Sending up");
            moveDirection = MoveDirection.Up;
        }
        else
        {
            moveDirection = MoveDirection.None;
        }

        IsMoving = true;
    }

    void ChangeDirection()
    {
        switch (moveDirection)
        {
            case MoveDirection.Up:
                moveDirection = MoveDirection.Down;
                break;
            case MoveDirection.Down:
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
