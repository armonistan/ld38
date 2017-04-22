using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour
{
    public float Speed;
    public float DegAngle;

	private int PAUSED = 0;

    public float RadAngle
    {
        get { return Mathf.Deg2Rad * DegAngle; }
        set { DegAngle = value * Mathf.Rad2Deg; }
    }

    public Vector2 Velocity
    {
        get
        {
            return new Vector2(Mathf.Cos(RadAngle), Mathf.Sin(RadAngle)) * Speed * Time.fixedDeltaTime;
        }
        set
        {
            Speed = value.magnitude / Time.fixedDeltaTime;
            RadAngle = Mathf.Atan2(value.y, value.x);
        }
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == PAUSED) {
			return;
		}		

	    this.gameObject.transform.Translate(Velocity);
	}
}
