using UnityEngine;
using System.Collections;

public class StaticVarManager : MonoBehaviour {


    public static int price_diamond = 1000;
    public static int price_boom = -500;


    // timeremain

    public static float TimeReMain = 1f;
    public static float TimeReMain_Trash = 0.5f;
    public static float TimeReMain_Treasure = 10f;
	// Use this for initialization

    void Awake()
    {
        TimeReMain = 1f;
        TimeReMain_Trash = 0.5f;
        TimeReMain_Treasure = 10f;
    }
	void Start () {
	    TimeReMain = 1f;
        TimeReMain_Trash = 0.5f;
        TimeReMain_Treasure = 10f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
