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
        _storyText.Add("Isn't it funny how we can't really tell what anyone else is thinking? How sometimes, we can't even tell what we're thinking?");
        _storyText.Add("How much of you is actually you? I mean, you think you've got your body, but even that is made up of other living things...");
        _storyText.Add("We are islands to each other, building hopeful bridges on a troubled sea.");
        _storyText.Add("I can't decide to make my heart beat again, or prevent my eyes from seeing. These things so core to us are alien to our control.");
        _storyText.Add("We feel so much like we're part of this huge world. How much of it have you seen though?");
        _storyText.Add("Is our identity the only thing we are? It sounds really stupid, I know, but if that's it, then the rest of this is pretty unreal.");
        _storyText.Add("Each of us a world apart -- Alone and yet together like two passing ships.");
        _storyText.Add("I don't know if you've felt this, but everything gets smaller when you're alone. But friends, family, love... we all add to each other's little worlds.");
        _storyText.Add("We make things so we can take a little part of ourselves and spread it around. It's in our nature.");
        _storyCursor = 0;

        int n = _storyText.Count;
        while (n > 1)
        {
            n--;
            int k = (int)(Random.Range(0, n));
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
