using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCar : MonoBehaviour
{
    public GameObject CarObj;
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
        
    }


    public void SendCar(Vector3 loc)
    {
       
    }

    public enum MoveDirection
    {
        Up,
        Down,
        None
    }
}
