using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour
{
    public float Speed;
    public float DegAngle;

    public float RadAngle
    {
        get { return Mathf.Deg2Rad * DegAngle; }
        set { DegAngle = value * Mathf.Rad2Deg; }
    }

    public Vector2 Velocity
    {
        get
        {
            return new Vector2(Mathf.Cos(RadAngle), Mathf.Sin(RadAngle)) * Speed * Time.deltaTime;
        }
        set
        {
            Speed = value.magnitude / Time.deltaTime;
            RadAngle = Mathf.Atan2(value.y, value.x);
        }
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    this.gameObject.transform.Translate(Velocity);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        WallControl wall;

        if ((wall = coll.gameObject.GetComponent<WallControl>()) != null)
        {
            // Source: http://stackoverflow.com/questions/573084/how-to-calculate-bounce-angle
            var u = (Vector2.Dot(Velocity, wall.Normal) / Vector2.Dot(wall.Normal, wall.Normal)) * wall.Normal;
            var w = Velocity - u;

            //TODO: Add friction?
            Velocity = w - u;
        }
    }
}
