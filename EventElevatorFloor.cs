using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloorEvent : UnityEvent<int>
{

}

public class EventElevatorFloor : MonoBehaviour
{
    public EventElevator Elevator;
    public int FloorNumber;
    public bool IsWaiting;
    [HideInInspector] public FloorEvent FloorActivateEvent;
    [HideInInspector] public FloorEvent FloorDeactivateEvent;

    private void Awake()
    {
        FloorActivateEvent.AddListener(Elevator.AddToQueue);
        FloorDeactivateEvent.AddListener(Elevator.RemoveFromQueue);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateFloor()
    {
        Debug.Log($"Activating floor {FloorNumber}");
        FloorActivateEvent.Invoke(FloorNumber);
    }

    public void DeactivateFloor()
    {
        Debug.Log($"Deactivating Floor {FloorNumber}");
        FloorDeactivateEvent.Invoke(FloorNumber);
    }
}
