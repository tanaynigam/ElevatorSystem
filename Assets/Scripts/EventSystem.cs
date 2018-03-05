using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class EventSystem : MonoBehaviour {

    public static new Vector3[] loc = new Vector3[6];
    int source;
    int dest;

    public static bool dir_up = true;

    public static Queue upq;
    public static Queue downq;

    bool wait = false;

    public static bool[] source_check = new bool[6];
	//Initialization
	void Start () {

        upq = new Queue();
        downq = new Queue();

        for (int i = 1; i<6; i++)
        {
            source_check[i] = false;
        }

        loc[1] = Elevator.floor1;
        loc[2] = Elevator.floor2;
        loc[3] = Elevator.floor3;
        loc[4] = Elevator.floor4;
        loc[5] = Elevator.floor5;


    }
	
	void Update () {


        //Sort Queues to read directional destinations in Ascending/Descending order 
        SortQueues();


        if (wait == false)
        {
            wait = true;
            StartCoroutine(Timer());
        }
/*
        if(Elevator.ElevatorRun == false)
        {
            if (upq.Count != 0)
            {
                Elevator.destination = loc[(int)upq.Dequeue()];
                Elevator.ElevatorRun = true;
                dir_up = true;
                Elevator.reached = false;
            }
            else if(downq.Count != 0)
            {
                Elevator.destination = loc[(int)downq.Dequeue()];
                Elevator.ElevatorRun = true;
                dir_up = false;
                Elevator.reached = false;
            }
        }
*/

        //Check if Elevator has reached
        if (Elevator.reached = true && Elevator.ElevatorRun == true)
        {
            if (dir_up == true)
            {
                //If Going up, check for next destination or source in that direction
                if (upq.Count != 0)
                {
                    Elevator.destination = loc[(int)upq.Dequeue()];
                    Elevator.reached = false;
                }
                else
                    dir_up = false;
            }

            else if(dir_up == false)
            {
                //If going down, check for next destination or source in that direction
                if (downq.Count != 0)
                {
                    Elevator.destination = loc[(int)downq.Dequeue()];
                    Elevator.reached = false;
                }
                else
                    dir_up = true;
            }
        }

        //If no Source/destination exists in any direction, Halt the Elevator
        if (upq.Count == 0 && downq.Count == 0 && Elevator.reached == true)
            Elevator.ElevatorRun = false;
		
	}

    //At every 10seconds, add a source
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10f);

        System.Random rnd = new System.Random();
        source = rnd.Next(1, 5);
        while (source == Display.currentFloor)
            source = rnd.Next(1, 5);

        //Add source to call an Elevator. Add to the specific Direction Queue
        if (Elevator.ElevatorRun == true)
        {
            if (source == 1)
                upq.Enqueue(source);
            else if (source == 5)
                downq.Enqueue(source);
            else
            {
                int dir = rnd.Next(1, 2);
                if (dir == 1)
                    upq.Enqueue(source);
                else
                    downq.Enqueue(source);
            }
        }
        else if(Elevator.reached == false)
        {
            Elevator.destination = loc[source];
            Elevator.ElevatorRun = true;
            Elevator.reached = false;

            if (source > Display.currentFloor)
                dir_up = true;
            else
                dir_up = false;
        }

        wait = false;
        //Debug.Log(upq.Peek());
        Debug.Log("Run" + Elevator.ElevatorRun);
        Debug.Log("Reached" + Elevator.reached);
        //        Debug.Log(loc[(int)upq.Dequeue()]);
        Debug.Log("Start" + Elevator.start);
        Debug.Log("Destination" + Elevator.destination);

    }

    void SortQueues()
    {
        if( upq.Count != 0 )
        {
            var a = upq.ToArray().ToList();
            a.Sort();
            upq = new Queue(a);
        }

        if (downq.Count != 0)
        {
            var b = downq.ToArray().ToList();
            b.Sort();
            b.Reverse();
            downq = new Queue(b);
        }
    }
}
