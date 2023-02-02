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
    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = CarObj.transform.position;
        nextPosition = CarObj.transform.position;
        nextPosition.y += 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            Move(nextPosition);
        }
    }


    public void Move(Vector3 loc)
    {
        CarObj.transform.position = Vector3.MoveTowards(CarObj.transform.position, loc, MoveSpeed * Time.deltaTime);
    }

   public void SendCar(Vector3 loc)
    {
        nextPosition = loc;
        IsMoving = true;
    }

    public void Stop()
    {
        IsMoving = false;
        moveDirection = MoveDirection.None;
    }

    public enum MoveDirection
    {
        Up,
        Down,
        None
    }
}
