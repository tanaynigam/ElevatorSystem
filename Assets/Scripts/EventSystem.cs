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
	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {

        SortQueues();
        if (wait == false)
        {
            wait = true;
            StartCoroutine(Timer());
        }

        if(Elevator.ElevatorRun == false && Elevator.reached == true)
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

        if (Elevator.reached = true && Elevator.ElevatorRun == true)
        {
            if (dir_up == true)
            {
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
                if (downq.Count != 0)
                {
                    Elevator.destination = loc[(int)downq.Dequeue()];
                    Elevator.reached = false;
                }
                else
                    dir_up = true;
            }
        }

        if (upq.Count == 0 && downq.Count == 0 && Elevator.reached == true)
            Elevator.ElevatorRun = false;
		
	}

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10f);

        System.Random rnd = new System.Random();
        source = rnd.Next(1, 5);
        while (source == Display.currentFloor)
            source = rnd.Next(1, 5);

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

        wait = false;
        Debug.Log(upq.Peek());
        Debug.Log(Elevator.ElevatorRun);
        Debug.Log(Elevator.reached);
        Debug.Log(loc[(int)upq.Dequeue()]);
        Debug.Log(Elevator.destination);
        Debug.Log(dir_up);

    }

    void SortQueues()
    {
        var a = upq.ToArray().ToList();
        var b = downq.ToArray().ToList();

        a.Sort();
        b.Sort();
        b.Reverse();

        upq = new Queue(a);
        downq = new Queue(b);
    }
}
