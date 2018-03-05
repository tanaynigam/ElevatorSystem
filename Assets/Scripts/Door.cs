using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public static bool DoorOpen = false;
    //static new Vector3 Open = new Vector3(-4.5f, 1.25f, 0.25f);
    //static new Vector3 Close = new Vector3(0, 1.25f, 0.25f);
    public static Transform door;

      void Start()
    {
        door = GameObject.Find("Door").GetComponent<Transform>();
    }

    void Update()
    {
        if(DoorOpen == true)
        {
            StartCoroutine(Countdown());
        }
    }

    public static void OpenDoor()
    {
        door.position = Vector3.MoveTowards(door.position, new Vector3(-4.5f, door.position.y, door.position.z), 2f * Time.deltaTime);
        DoorOpen = true;
    }

    public static void CloseDoor()
    {
        door.position = Vector3.MoveTowards(door.position, new Vector3(0, door.position.y, door.position.z), 2f * Time.deltaTime);
        if (Vector3.Distance(door.position, new Vector3(0, door.position.y, door.position.z)) == 0)
        {
            DoorOpen = false;
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5f);
        Door.CloseDoor();
    }
}
