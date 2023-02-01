using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public ElevatorCar car;
    public float timer;
    float tempTimer;
    // Start is called before the first frame update
    void Start()
    {
        car.SendCar(3.0f);
        tempTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        tempTimer -= 0.001f;
        Debug.Log(tempTimer);
        if(tempTimer <= 0.0f)
        {
            Debug.Log("timer hit");
            car.SendCar(1.0f);
            tempTimer = timer;
        }
    }
}
