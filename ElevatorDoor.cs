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
            case DoorState.Opening:
                Open();
                break;
            case DoorState.Closing:
                Close();
                break;
            case DoorState.Opened:
                tempCloseTimer -= Time.deltaTime;
                if(tempCloseTimer <= 0)
                {
                    tempCloseTimer = autoCloseTimer;
                    Close();
                }
                break;
            case DoorState.Closed:
                // do nothing
                break;
            default:
                Close();
                break;
        }
        
    }

    public void Open()
    {
        Debug.Log("Opening the door");

        currentState = DoorState.Opening;

        tempCloseTimer -= Time.deltaTime;

        if (tempCloseTimer <= 0)
        {
            tempCloseTimer = autoCloseTimer;
            currentState = DoorState.Opened;
        }
    }

    public void Close()
    {
        Debug.Log("Closing the door");
        currentState = DoorState.Closing;
        tempCloseTimer -= Time.deltaTime;
        if (tempCloseTimer <= 0)
        {
            tempCloseTimer = autoCloseTimer;
            currentState = DoorState.Closed;
        }
    }

    public enum DoorState
    {
        Opened,
        Closed,
        Opening,
        Closing
    }
}
