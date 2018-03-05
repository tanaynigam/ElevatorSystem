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

    
    void Start () {
//        destination = floor3;
//        ElevatorRun = true;
//        start = true;
	}
	
	void Update () {

        //Check if Door is open. Run only when it is closed
        if (ElevatorRun == true)
        {
            if (Door.DoorOpen == false)
            {
                start = true;
            }
        }

        //Accelerate/Decelerate Lift
        ElevatorAccelerate();
    }


    public void ElevatorAccelerate()
    {
        //Elevator Acceleration
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

        //Elevator Deceleration
        if (start == false)
        {
            if (Vector3.Distance(elevator.position, destination) < accelerateDist && speed > 0.1)
            {
                speed-= 0.1f;
            }
        }

        elevator.position = Vector3.MoveTowards(elevator.position, destination, speed * acc * Time.deltaTime);

        //Check Whether Elevator has reached the destination
        if (Vector3.Distance(elevator.position, destination) == 0)
        {
            
            //Check whether Elevator is at Source.
            if(EventSystem.source_check[Display.currentFloor] == true)
            {

                //Input Destination if Lift is at Source
                System.Random rnd = new System.Random();
                int temp = rnd.Next(Display.currentFloor, 5);
                while (temp == Display.currentFloor)
                    temp = rnd.Next(1, 5);

                //Queue the destination floor in the up or down direction.
                if (temp > Display.currentFloor)
                {
                    EventSystem.upq.Enqueue(temp);
                }
                else
                {
                    EventSystem.downq.Enqueue(temp);
                }
            }

            //Change States and Open and Close Door
            if(reached == false)
            {

                reached = true;
                start = false;
                Door.DoorComplete = true;
 //               Door.DoorComplete = true;
//                StartCoroutine(Countdown());
            }
            ///StartCoroutine(Countdown());

        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5f);
        Door.CloseDoor();
    }

}
