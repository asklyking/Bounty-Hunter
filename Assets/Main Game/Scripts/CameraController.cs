using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public float smoothX = 0.1f;
    public float smoothY = 0.1f;
    public float MinCameraPosX;
    public float MaxCameraPosX;
    private Vector2 offset;

    float speedCam = 13f;
    bool follow = false;
    public GameObject player;
	// Use this for initialization
	void Start () {
        follow = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        //float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref offset.x, smoothX);
        //float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref offset.x, smoothY);

        if (GamePlayManager.first_blood == false)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y-5f, transform.position.z);
        
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * speedCam);
            Invoke("SetCameraBack",1.2345f);
        }
        
        if(follow == true)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5f, transform.position.z);
        }
        if (player.transform.position.x <= MinCameraPosX || player.transform.position.x >= MaxCameraPosX )
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinCameraPosX, MaxCameraPosX), transform.position.y, transform.position.z);
            //Mathf.Clamp(transform.position.y, MinCameraPos.y, MaxCameraPos.y),
            //Mathf.Clamp(transform.position.z, MinCameraPos.z, MaxCameraPos.z));
            //transform.position = new Vector3(1.43f, transform.position.y, transform.position.z);
        }
	}


    void SetCameraBack()
    {
        speedCam = 0;
        transform.Translate(Vector3.zero);

        follow = true;
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
