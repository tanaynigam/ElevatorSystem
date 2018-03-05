using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour {
    Transform elevator;
    TextMesh text;
    public static int currentFloor;

    // Use this for initialization
    void Start () {
        elevator = GameObject.Find("Elevator").GetComponent<Transform>();
        text = GameObject.Find("Number").GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
		if(elevator.position.y > (Elevator.floor1.y - 1) && elevator.position.y < (Elevator.floor2.y - 1))
        {
            currentFloor = 1;
            text.text = "1";
        }
        if (elevator.position.y > (Elevator.floor2.y - 1) && elevator.position.y < (Elevator.floor3.y - 1))
        {
            currentFloor = 2;
            text.text = "2";
        }
        if (elevator.position.y > (Elevator.floor3.y - 1) && elevator.position.y < (Elevator.floor4.y - 1))
        {
            currentFloor = 3;
            text.text = "3";
        }
        if (elevator.position.y > (Elevator.floor4.y - 1) && elevator.position.y < (Elevator.floor5.y - 1))
        {
            currentFloor = 4;
            text.text = "4";
        }
        if (elevator.position.y > (Elevator.floor5.y - 1))
        {
            currentFloor = 5;
            text.text = "5";
        }
        
    }
}
