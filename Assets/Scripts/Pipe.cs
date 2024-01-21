using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject flow;
    public float timeUntilPass = .1f;
    public Transform[] connections;
    public GameObject leakParticles;
    public bool waiting, activated, disable, isWin;

    void Awake() {
        timeUntilPass = .1f;
    }

    void OnMouseDown(){
        if(!disable && !Manager.instance.started)
            transform.RotateAround(gameObject.transform.position, Vector3.forward, 90);
    }

    void Update() {
        if(waiting) {
            timeUntilPass -= Time.deltaTime;
            if(timeUntilPass < 0) {
                if(isWin) {
                    LevelSelect.instance.Finish();
                }
                waiting = false;
                foreach(Transform connection in connections) {
                    Collider2D[] intersecting = Physics2D.OverlapCircleAll(connection.position, 0.1f);
                    bool found = false;
                    foreach(Collider2D c in intersecting) {
                        if(c.gameObject.layer != 6 || c.gameObject == connection.gameObject)
                            continue;
                        Pipe p = c.GetComponentInParent<Pipe>();
                        found = true;
                        if(!p.activated) {
                            p.Activate();
                            p.timeUntilPass += timeUntilPass;
                        }
                    }
                    if(!found) {
                        Instantiate(leakParticles, connection);
                        LevelSelect.instance.floodSpeed += .1f;
                    }
                }
            }
        }
    }

    public void Activate() {
        flow.SetActive(true);
        activated = true;
        waiting = true;
    }
}
