using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UiControl : MonoBehaviour {
    private struct RecordedScore
    {
        public int score, multiplier;

        public RecordedScore(int s, int m)
        {
            score = s;
            multiplier = m;
        }
    };

    public Text Score;

    public Image Menu;

    public MovingTextControl AddedScorePrefab;
    public RectTransform AddedScoreStartPoint, AddedScoreEndPoint;

    private int _score;
    private int _multiplier;
    private string _addedScoreText;

    private RecordedScore _currentScoreAdding;
    private ArrayList _scoresToAdd;
    private int _scoreAddStepSize;
    private NumberFormatInfo nfi;

    private GameControl _gameControl;

    // Use this for initialization
    void Start()
    {
        _addedScoreText = "";
        _score = 0;
        _multiplier = 1;

        _currentScoreAdding = new RecordedScore(0, 0);
        _scoresToAdd = new ArrayList();
        _scoreAddStepSize = 1;
        nfi = new CultureInfo("en-US", false).NumberFormat;
        nfi.NumberDecimalDigits = 0;

        _gameControl = FindObjectOfType<GameControl>();
    }

    public void Restart()
    {
        _currentScoreAdding = new RecordedScore(0, 0);
        _score = 0;
        _multiplier = 1;
        _scoresToAdd.RemoveRange(0, _scoresToAdd.Count);
        _scoreAddStepSize = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == GameControl.Paused)
        {
            return;
        }

        if (_gameControl.GetGameState() == GameControl.States.Menu)
        {
            _addedScoreText = "";
            if (Menu)
            {
                Menu.GetComponent<Image>().enabled = true;
            }
        }
        else
        {
            Menu.GetComponent<Image>().enabled = false;
        }

        //code to peel numbers off of a queue, add them one at a time to score for visual effect
        if (_currentScoreAdding.score == 0 && _scoresToAdd.Count > 0)
        {
            _currentScoreAdding = (RecordedScore)_scoresToAdd[0];
            _scoresToAdd.RemoveAt(0);
        }
        else if (_currentScoreAdding.score > 0)
        {
            _scoreAddStepSize = Mathf.Max(_scoresToAdd.Count + 1, _scoreAddStepSize);
            int calculatedScoreAdd = _scoreAddStepSize * _currentScoreAdding.multiplier;
            if (_currentScoreAdding.score >= calculatedScoreAdd)
            {
                _score += calculatedScoreAdd;
                _currentScoreAdding.score -= calculatedScoreAdd;
            }
            else
            {
                _score += _currentScoreAdding.score;
                _currentScoreAdding.score = 0;
            }
        }
        else
        {
            _scoreAddStepSize = 1;
        }
        Score.text = _score.ToString("N", nfi);
    }

    public void AddScore(int scoreToAdd)
    {
        _scoresToAdd.Add(new RecordedScore(scoreToAdd * _multiplier, _multiplier));
        _addedScoreText = "+" + scoreToAdd.ToString();
        if (_multiplier > 1)
        {
            _addedScoreText = _addedScoreText + "x" + _multiplier.ToString() + "!";
        }
        MovingTextControl addedScore = Instantiate(AddedScorePrefab, AddedScoreStartPoint);
        addedScore.GetComponent<MovingTextControl>().SetTarget(AddedScoreEndPoint);
        addedScore.GetComponent<MovingTextControl>().SetDisplayString(_addedScoreText);
    }

    public void BoostMultiplier()
    {
        _multiplier += 1;
    }

    public void ResetMultipler()
    {
        _multiplier = 1;
    }
}
