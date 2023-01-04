using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{
    public GameObject cloud1;
    public GameObject cloud2;
    public float speed;

    float c1Distance;
    float c2Distance;

    float c1x;
    float c2x;

    // Start is called before the first frame update
    void Start()
    {
        c1x = cloud1.GetComponent<SpriteRenderer>().bounds.size.x;
        c2x = cloud2.GetComponent<SpriteRenderer>().bounds.size.x;
        c1Distance = c2x;
        c2Distance = 0;
        //print("x:  " + c1x);
        //print(c2x);
    }

    // Update is called once per frame
    void Update()
    {
        cloud1.transform.Translate(Vector2.left * Time.deltaTime * speed);
        cloud2.transform.Translate(Vector2.left * Time.deltaTime * speed);

        c1Distance += Time.deltaTime * speed;
        c2Distance += Time.deltaTime * speed;

        if (c1Distance > c1x + c2x)
        {
            cloud1.transform.position = new Vector3(c2x, cloud1.transform.position.y, cloud1.transform.position.z);
            c1Distance = 0;
        }
        else if (c2Distance > c1x + c2x)
        {
            cloud2.transform.position = new Vector3(c1x, cloud2.transform.position.y, cloud2.transform.position.z);
            c2Distance = 0;
        }

    }

    private void FixedUpdate()
    {
    }
}
