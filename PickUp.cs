using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {


    public GameObject obj1;
    private const float MaxDistance = 1000.0f;
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
        Ray fwd_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit cursorHit = new RaycastHit();
        Physics.Raycast(fwd_ray, out cursorHit, MaxDistance);

        if (cursorHit.collider != null)
        {
            if (cursorHit.collider.gameObject == obj1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    print(transform.Find("Capsule/RightHand"));
                    Vector3 left_pos = transform.Find("Capsule/LeftHand").position;
                    Vector3 right_pos = transform.Find("Capsule/RightHand").position;

                    Vector3 new_pos = left_pos + (right_pos - left_pos) / 2; //Calculate midpoint
                    obj1.transform.position = new_pos;
                    obj1.transform.parent = transform.Find("Capsule");
                }           
            }
        }
	}
}
