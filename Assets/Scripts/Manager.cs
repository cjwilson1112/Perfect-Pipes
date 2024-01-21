using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    void Awake() {
        instance = this;
    }

    public Pipe startPipe;
    public float timer = 10;
    public bool started;
    public float finalTimer = 2;

    void Update() {
        timer -= Time.deltaTime;
        if(!started) {
            if(timer < 0) {
                LevelSelect.instance.pipe.SetActive(false);
                started = true;
                LevelSelect.instance.timer.text = "0";
                startPipe.Activate();
            } else if(timer > 3)
                LevelSelect.instance.timer.text = "" + ((int)timer);
            else {
                LevelSelect.instance.timer.text = "" + (((int)(timer * 10))/10f);
            }
        }
        else {
            finalTimer -= Time.deltaTime;
            LevelSelect.instance.timer.text = "" + (((int)(finalTimer * 10))/10f);
            if(finalTimer < 0 && LevelSelect.instance.waiting)
                LevelSelect.instance.Lose();
        }
    }
}
