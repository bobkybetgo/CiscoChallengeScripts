using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    public GameObject Target;

    private float y_distance = 0.0f;
    private Vector3 vecMoveTo = new Vector3(0, 0, 0);
    private float speed_0 = 0.001f;
    private float speed_1 = 0.5f;
    private float max_y_distance;
    private float original_y;

    private bool done_waiting = false;

    private bool ascending = true;
    private bool descending = false;
    private bool done = false;
    
	// Use this for initialization
	void Start () {
        max_y_distance = Target.transform.Find("mesh").GetComponent<Renderer>().bounds.size.y;
        original_y = transform.position.y;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        ascending = false;
    }

    IEnumerator Wait2()
    {

        yield return new WaitForSeconds(2.0f);
        done_waiting = true;

    }

    // Update is called once per frame
    void Update() {
        if (done) return;
        if (ascending)
        {
            y_distance += speed_0 * Time.deltaTime;
            transform.Translate(0, y_distance, 0);
            if (transform.position.y >= max_y_distance)
            {
                ascending = false;
                y_distance = 0;
            }
        }
        else if(descending)
        {
            y_distance -= speed_0 * Time.deltaTime;
            transform.Translate(0, y_distance, 0);
            if (transform.position.y <= original_y)
            {
                done = true;
                descending = false;
            }
        }
        else {
            StartCoroutine("Wait");
 
            float step = speed_1 * Time.deltaTime;
            Vector3 new_pos = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, new_pos, step);

            if (transform.position.x == new_pos.x && transform.position.z == new_pos.z)
            {
                if (!done_waiting) StartCoroutine("Wait2");
                else
                {
                    Target.transform.Find("mesh").GetComponent<Renderer>().enabled = false;
                    descending = true;
                }
            }
        }

	}
}
