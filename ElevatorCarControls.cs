using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCarControls : MonoBehaviour
{
    Dictionary<int, Vector3> floorList;
    ElevatorCar car;
    ElevatorDoor door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendCar(int floor)
    {
        car.SendCar(floorList[floor]);
    }
}
