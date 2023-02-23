using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public float autoCloseTimer;
    public DoorState currentState;
    private float tempCloseTimer;
    // Start is called before the first frame update
    void Start()
    {
        tempCloseTimer = autoCloseTimer;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (DoorState.Opening):
                tempCloseTimer -= Time.deltaTime;
                if (tempCloseTimer <= 0)
                {
                    currentState = DoorState.Opened;
                }
                break;
            case (DoorState.Opened):
                tempCloseTimer -= Time.deltaTime;
                if (tempCloseTimer <= 0)
                {
                    tempCloseTimer = autoCloseTimer;
                    Close();
                }
                break;
            case (DoorState.Closing):
                tempCloseTimer -= Time.deltaTime;
                if (tempCloseTimer <= 0)
                {
                    tempCloseTimer = autoCloseTimer;
                    currentState = DoorState.Closed;
                }
                break;
            
            default:
                break;
        } 
    }

    public void Open()
    {
        Debug.Log("Opening the door");

        currentState = DoorState.Opening;
    }

    public void Close()
    {
        Debug.Log("Closing the door");
        currentState = DoorState.Closing;
    }

    public enum DoorState
    {
        Opened,
        Closed,
        Opening,
        Closing
    }
}
