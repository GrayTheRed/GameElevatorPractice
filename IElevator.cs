using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElevator 
{
    ElevatorCar ElevatorCar { get; set; }
    List<ElevatorFloor> ElevatorFloors { get; set; }
    ElevatorQueue Queue { get; set; }
}
