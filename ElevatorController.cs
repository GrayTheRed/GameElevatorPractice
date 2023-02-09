using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public ElevatorCar car;
    public Vector3 floor;
    public float timer;
    float tempTimer;
    // Start is called before the first frame update
    void Start()
    {
        floor = car.transform.position;
        tempTimer = timer;
    }

    // Update is called once per frame
    void Update()
    { 
        tempTimer -= .001f;
        if(tempTimer <= 0)
        {
            tempTimer = timer;
            Debug.Log("sending car down now");
            car.SendCar(floor);
        }
    }

    public void GetCar()
    {
        car.Move(floor);
    }
}
