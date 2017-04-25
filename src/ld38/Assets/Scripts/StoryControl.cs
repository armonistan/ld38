using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryControl : Assets.Scripts.StatefulMonoBehavior<StoryControl.States>
{

    public enum States
    {
        Waiting,
        Writing
    }

    public int WaitFrames = 600;

    public MovingTextControl LeftTextPrefab, RightTextPrefab;
    public RectTransform LeftSpawnPoint, LeftEndPoint, RightSpawnPoint, RightEndPoint;

    public bool tellStory;

    private List<string> _storyText;
    private int _storyCursor;
    private bool side;   

	// Use this for initialization
	void Start () {
        tellStory = false;
        side = false;

        Restart();

        State = States.Writing;
    }

    public void Restart()
    {
        _storyText = new List<string>();
        _storyText.Add("This is a really long story element how does it look after the wrap is applied to the text?");
		_storyText.Add("This is a really long story element how does it look after the wrap is applied to the text?");
		_storyText.Add("This is a really long story element how does it look after the wrap is applied to the text?");
		_storyText.Add("This is a really long story element how does it look after the wrap is applied to the text?");
		_storyText.Add("This is a really long story element how does it look after the wrap is applied to the text?");
		_storyText.Add("This is a really long story element how does it look after the wrap is applied to the text?");
		_storyText.Add("This is a really long story element how does it look after the wrap is applied to the text?");
		_storyText.Add("This is a really long story element how does it look after the wrap is applied to the text?");
        _storyCursor = 0;

        int n = _storyText.Count;
        while (n > 1)
        {
            n--;
            int k = (int)(Random.Range(0, n + 1));
            string value = _storyText[k];
            _storyText[k] = _storyText[n];
            _storyText[n] = value;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!tellStory)
        {
            return;
        }

		switch(State)
        {
            case States.Waiting:
                if (!side)
                {
                    SpawnLeftText();
                }
                else if (side)
                {
                    SpawnRightText();
                }
                break;
            case States.Writing:
                if (Counter < WaitFrames)
                {
                    IncrementCounter();
                }
                else
                {
                    State = States.Waiting;
                }
                break;
            default:
                break;
        }
	}

    private void SpawnLeftText()
    {
        if (_storyCursor == _storyText.Count)
        {
            _storyCursor = 0;
        }
        MovingTextControl storyText = Instantiate(LeftTextPrefab, LeftSpawnPoint);
        storyText.GetComponent<MovingTextControl>().SetTarget(LeftEndPoint);
        storyText.GetComponent<MovingTextControl>().SetDisplayString(_storyText[_storyCursor]);
        _storyCursor++;
        State = States.Writing;
        side = !side;
    }

    private void SpawnRightText()
    {
        if (_storyCursor == _storyText.Count)
        {
            _storyCursor = 0;
        }
        MovingTextControl storyText = Instantiate(RightTextPrefab, RightSpawnPoint);
        storyText.GetComponent<MovingTextControl>().SetTarget(RightEndPoint);
        storyText.GetComponent<MovingTextControl>().SetDisplayString(_storyText[_storyCursor]);
        _storyCursor++;
        State = States.Writing;
        side = !side;
    }
}
