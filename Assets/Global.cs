using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
    public static Global S;

    public delegate void DestroyBullets();
    public static event DestroyBullets DestroyLeftBullets;
    public static event DestroyBullets DestroyRightBullets;

    void DestroyLeft()
    {
        DestroyLeftBullets();
    }

    void DestroyRight()
    {
        DestroyRightBullets();
    }


    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
