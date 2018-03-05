using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    float speed;
    float acc = 2f;
    public static bool start = false;
    public static bool ElevatorRun = false;
    public static bool reached = true;
    
    public static new Vector3 floor1 = new Vector3(0, 0, 0);
    public static new Vector3 floor2 = new Vector3(0, 15, 0);
    public static new Vector3 floor3 = new Vector3(0, 30, 0);
    public static new Vector3 floor4 = new Vector3(0, 45, 0);
    public static new Vector3 floor5 = new Vector3(0, 60, 0);
    

    public static new Vector3 destination;
    new Vector3 source;
    new float accelerateDist;

    [SerializeField]
    Transform elevator;


    // Use this for initialization
    void Start () {
//        destination = floor3;
//        ElevatorRun = true;
//        start = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (ElevatorRun == true)
        {
            if (Door.DoorOpen == false)
            {
                start = true;
            }
        }

        ElevatorAccelerate();
    }


    public void ElevatorAccelerate()
    {
        if (start == true)
        {
            if (speed < 1)
            {
                speed += 0.1f;
                accelerateDist = Vector3.Distance(elevator.position, source);
            }
            else
                start = false;
        }

        if (start == false)
        {
            if (Vector3.Distance(elevator.position, destination) < accelerateDist && speed > 0.1)
            {
                speed-= 0.1f;
            }
        }

        elevator.position = Vector3.MoveTowards(elevator.position, destination, speed * acc * Time.deltaTime);

        if (Vector3.Distance(elevator.position, destination) < 0.1)
        {
            
            if(EventSystem.source_check[Display.currentFloor] == true)
            {
                if(EventSystem.dir_up == true)
                {
                    System.Random rnd = new System.Random();
                    int temp = rnd.Next(Display.currentFloor, 5);
                    EventSystem.upq.Enqueue(temp);
                }
                else
                {
                    System.Random rnd = new System.Random();
                    int temp = rnd.Next(1, Display.currentFloor);
                    EventSystem.downq.Enqueue(temp);
                }
            }

            reached = true;
            Door.OpenDoor();
        }
    }


    
}
