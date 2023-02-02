using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElevator 
{
    ElevatorCar ElevatorCar { get; set; }
    ElevatorFloor ElevatorFloor { get; set; }
}
