using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopController : MonoBehaviour {

    public Text properties;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackToGame()
    {
        Application.LoadLevel("Game Play");
    }

    public void ChooseHook0()
    {
        setText("100m");
        GamePlayManager.hook_number = 0;
        

    }
    public void ChooseHook1()
    {
        setText("200m");
        GamePlayManager.hook_number = 1;
       
    }

    public void ChooseHook2()
    {
        setText("300m");
        GamePlayManager.hook_number = 2;

    }

    public void ChooseHook3()
    {
        setText("400m");
        GamePlayManager.hook_number = 3;

    }

    public void ChooseHook4()
    {
        setText("500m");
        GamePlayManager.hook_number = 4;

    }

    public void ChooseHook5()
    {
        setText("600m");
        GamePlayManager.hook_number = 5;

    }
    

    public void ChooseChain1()
    {
        setText("20 objects");
        GamePlayManager.chain_number = 1;
    }

    public void ChooseChain2()
    {
        setText("30 objects");
        GamePlayManager.chain_number = 2;
    }

    public void ChooseChain3()
    {
        setText("40 objects");
        GamePlayManager.chain_number = 3;
    }

    public void ChooseChain4()
    {
        setText("50 objects");
        GamePlayManager.chain_number = 4;
    }

    public void ChooseChain5()
    {
        setText("60 objects");
        GamePlayManager.chain_number = 5;
    }


    void setText(string text)
    {
        properties.text = text;
    }
}
