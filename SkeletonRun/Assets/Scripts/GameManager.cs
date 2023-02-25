using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool winState = false;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text pointsText;
    private float seconds = 0, minutes = 0;
    public int points = 0;
    private void Update()
    {
        if (!winState)
        {
            /// show elapsed time
            seconds += Time.deltaTime;
            if (seconds >= 60)
            {
                minutes++;
                seconds -= 60.0f;
            }
            timeText.text = Mathf.Floor(minutes).ToString();
            if (timeText.text.Length == 1)
                timeText.text = "0" + timeText.text;
            string secs = Mathf.Floor(seconds).ToString();
            if (secs.Length == 1)
                secs = "0" + secs;
            timeText.text += ":" + secs;

            /// show current score
            pointsText.text = points.ToString();
        }
    }

}
