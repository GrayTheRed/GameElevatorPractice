using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCar : MonoBehaviour
{
    public GameObject CarObj;
    public ElevatorDoor ElevatorDoor;
    public float MoveSpeed;
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

}
