using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    float elapseSeconds;
    bool timerRunning;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        elapseSeconds = 0;
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            elapseSeconds += Time.deltaTime;
            scoreText.text = ((int)elapseSeconds).ToString();
        }
    }

    public void StopGameTimer() {
        timerRunning = false;
    }
}
