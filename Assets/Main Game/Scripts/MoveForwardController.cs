using UnityEngine;
using System.Collections;

public class MoveForwardController : MonoBehaviour {

    GameObject player;
    Rigidbody2D rigid;
    float speed = 5f;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Cart");
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        float x = -this.transform.position.x + player.transform.position.x;
        float y = -this.transform.position.y + player.transform.position.y;
        rigid.velocity = new Vector2(x, y) * speed;
        

        //transform.position = player.transform.position;

	}
    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "cart")
       {
           Destroy(this.gameObject);
       }
    }
}
