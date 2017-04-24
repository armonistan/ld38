using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingTextControl : StatefulMonoBehavior<MovingTextControl.States> {

    public enum States
    {
        Spawned,
        Moving,
        Stopped
    }

    public float MoveSpeed = 0.3f;

    public int DisplayFrames = 15;
    private string _displayString = "";
    private RectTransform _position, _endPoint;
    private Text _text;
    private float _alpha = 0f;
    private float _epsilon = 0.1f;

	// Use this for initialization
	void Start () {
        State = States.Moving;
        _alpha = 0f;
        _text = GetComponent<Text>();
        _position = GetComponent<RectTransform>();
	}

    public void SetDisplayString(string s)
    {
        _displayString = s;
    }

    public void SetTarget(RectTransform end)
    {
        _endPoint = end;
    }
	
	// Update is called once per frame
	void Update () {
        if (_displayString != "")
        {
            _text.text = _displayString;
        }
		switch(State)
        {
            case States.Spawned:
                break;
            case States.Moving:
                MoveText();
                break;
            case States.Stopped:
                FadeOut();
                break;
            default:
                break;
        }
	}

    private void MoveText()
    {
        if (_alpha < 1.0f)
        {
            _alpha = Mathf.Lerp(_alpha, 1f, _epsilon);
        }
        Color c = _text.color;
        c.a = _alpha;
        _text.color = c;

        if (_endPoint)
        {
            _position.position = Vector2.MoveTowards(_position.position, _endPoint.position, MoveSpeed);

            if (Vector2.Distance(_position.position, _endPoint.position) < _epsilon)
            {
                State = States.Stopped;
            }
        }
    }

    private void FadeOut()
    {
        if (Counter < DisplayFrames)
        {
            IncrementCounter();
            _alpha = Mathf.Lerp(_alpha, 0f, _epsilon * 2);
            Color c = _text.color;
            c.a = _alpha;
            _text.color = c;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
