using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventIndicator : MonoBehaviour {
    public int current_event = 0;
    public List<Material> panels;
    public GameObject panel1, panel2;
    public GameObject back_panel;
    public GameObject front_panel1,front_panel2;
    Color normal_color;
    Color front_color;
    public static EventIndicator Panels;
    public float delay = 15;
    float timer = 0;

    void Awake() {
        Panels = this.GetComponent<EventIndicator>();
    }

    // Use this for initialization
    void Start() {
        panel1 = transform.Find("EventPanel").Find("p1").gameObject;
        panel2 = transform.Find("EventPanel").Find("p2").gameObject;
        back_panel = transform.Find("back").gameObject;
        front_panel1 = transform.Find("front").gameObject;
        front_panel2 = transform.Find("front2").gameObject;
        front_color = front_panel1.GetComponent<Renderer>().material.color;
        normal_color = back_panel.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > delay) {
            SetPanel(1);
            timer = 0;
        }
    }

    public void ResetPanel() {
        back_panel.GetComponent<Renderer>().material.color = normal_color;
        front_panel1.GetComponent<Renderer>().material.color = front_color;
        front_panel2.GetComponent<Renderer>().material.color = front_color;

    }

    public void SetPanel(int panel_num) {
        //if (panel_num == 0) {
         //   back_panel.GetComponent<Renderer>().material.color = normal_color;
        //}
        if (panel_num == 0) {
            panel1.GetComponent<Renderer>().material = panels[panel_num];
            panel2.GetComponent<Renderer>().material = panels[panel_num];
            Color temp = normal_color * 0.1f;
            Color temp2 = front_color * 0.3f;
            back_panel.GetComponent<Renderer>().material.color = temp;
            front_panel1.GetComponent<Renderer>().material.color = temp2;
            front_panel2.GetComponent<Renderer>().material.color = temp2;
            Invoke("ResetPanel", 3);
        }

    }
}
