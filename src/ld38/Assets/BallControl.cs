using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour
{
    public float Speed;
    public float DegAngle;

    public float RadAngle
    {
        get { return Mathf.Deg2Rad * DegAngle; }
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    this.gameObject.transform.Translate(new Vector3(Mathf.Cos(RadAngle), Mathf.Sin(RadAngle)) * Speed * Time.deltaTime);
	}
}
