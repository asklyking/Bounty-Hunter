using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlayManager : MonoBehaviour
{


	float Start_Y;
	float delta = 0.1f;
	//Score and dept
	public Text Current_Dept;
	public Text Score;

	// hook and chain
	public GameObject Chain;
	public Light Light;
	public static int hook_number = 0;
	float max_dept = 100;
	public static int chain_number = 0;
	int max_object;
	int number_object;

	// shop and cart
	public GameObject Trading_Shop;
	public GameObject Cart;

	// player in play
	public Text Total_Diamond;
	public Text Total_Diamond_Price;

	public Text Total_Boomb;
	public Text Total_Boomb_Price;

	public Text Total_Treasure;
	//public Text Total_Treasure_Price;

	public Text Total_Money;

	//player inform
	public Text player_money;
	//public Text player_highscore;
	public static bool first_blood;
	bool can_move;
	float speed = 5f;

	int total_diamond;
	int total_boomb;
	int total_treasure;

	public GameObject EdgeLeft;
	public GameObject EdgeRight;

	public GameObject diamond;
	public GameObject trash;
	public GameObject treasure;
	public GameObject explosion;

	// Clock down
	float TimeRemain;
	float TimeRemain_Trash;
	float TimeRemain_Treasure;


	// clean all
	private GameObject[] boombs, diamonds;


	Camera camera;
	Rigidbody2D rigid;

	public Color color1 = Color.red;
	public Color color2 = Color.blue;
	// Use this for initialization
	void Start ()
	{
		max_dept = 100 + hook_number * 100;
		max_object = 10 + chain_number * 10;

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//load sprite
		Sprite[] sprites = Resources.LoadAll<Sprite> ("list_hook");
		GetComponent<SpriteRenderer> ().sprite = sprites [hook_number];

		Chain.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("chain" + chain_number);
		Light.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -5f);


		this.gameObject.AddComponent<PolygonCollider2D> ();
		this.gameObject.GetComponent<PolygonCollider2D> ().isTrigger = true;
		StaticVarManager.TimeReMain = 1f;
		StaticVarManager.TimeReMain_Trash = 0.5f;
		StaticVarManager.TimeReMain_Treasure = 10f;

		camera = GetComponent<Camera> ();
		rigid = GetComponent<Rigidbody2D> ();

		//destroy all
		boombs = GameObject.FindGameObjectsWithTag ("boomb");
		diamonds = GameObject.FindGameObjectsWithTag ("diamond");
		for (int i = 0; i < boombs.Length; i++) {
			Destroy (boombs [i]);
		}
		for (int i = 0; i < diamonds.Length; i++) {
			Destroy (diamonds [i]);
		}
		//GetComponent<Rigidbody2D>().fixedAngle = true;
		Time.timeScale = 1f;
		first_blood = false;
		can_move = false;


		total_diamond = 0;
		total_boomb = 0;
		total_treasure = 0;

		Trading_Shop.SetActive (false);
		Start_Y = transform.position.y;

		//set up time remain
		TimeRemain = StaticVarManager.TimeReMain;
		TimeRemain_Trash = StaticVarManager.TimeReMain_Trash;
		TimeRemain_Treasure = StaticVarManager.TimeReMain_Treasure;

		//set up playerprefs

		if (PlayerPrefs.HasKey ("Money") == false) {
			PlayerPrefs.SetInt ("Money", 0);
		}
		if (PlayerPrefs.HasKey ("Best") == false) {
			PlayerPrefs.SetInt ("Best", 0);
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		Debug.Log ("current:" + number_object + "---max:" + max_object);
		Debug.Log (StaticVarManager.TimeReMain_Trash);
		// update money and best score

		player_money.text = "$" + PlayerPrefs.GetInt ("Money");
		//player_highscore.text = "Best Score:" + PlayerPrefs.GetInt("Best");
		// update score and best score
		int current_dept = (int)(-transform.position.y + Start_Y);
		if (current_dept > 0) {
			Current_Dept.text = "Current:" + current_dept + "/" + number_object;
		}
		if (first_blood == false) {
			//int score = (int)Mathf.Abs(transform.position.y - Start_Y);
			Score.text = "Dept/Strong:" + max_dept + "/" + max_object;


			int dept = (int)Mathf.Abs (transform.position.y - Start_Y);
			if (current_dept > 100 && current_dept < 200) {
				//StaticVarManager.TimeReMain = 0.9f;
				StaticVarManager.TimeReMain_Trash = 0.4f;
				Light.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -8 / 2f);
			} else if (current_dept > 200 && current_dept < 300) {
				//StaticVarManager.TimeReMain = 0.8f;
				StaticVarManager.TimeReMain_Trash = 0.3f;
				Light.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -6 / 2f);
			} else if (current_dept > 300 && current_dept < 400) {
				//StaticVarManager.TimeReMain = 0.7f;
				StaticVarManager.TimeReMain_Trash = 0.2f;
				Light.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -5 / 2f);
			} else if (current_dept > 400 && current_dept < 500) {
				//StaticVarManager.TimeReMain = 0.f;
				StaticVarManager.TimeReMain_Trash = 0.1f;
				//StaticVarManager.TimeReMain_Treasure -= delta;
				Light.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -4 / 2f);
			} else if (current_dept > 500) {
				//StaticVarManager.TimeReMain -= delta;
				StaticVarManager.TimeReMain_Trash = 0.05f;
				Light.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -3 / 2f);
			}
		}

		if (current_dept >= max_dept) {
			first_blood = true;
			speed = -5f;
		}

		if (transform.position.x < EdgeLeft.transform.position.x) {
			transform.position = new Vector3 (EdgeLeft.transform.position.x, transform.position.y, transform.position.z);
		}
		if (transform.position.x > EdgeRight.transform.position.x) {
			transform.position = new Vector3 (EdgeRight.transform.position.x, transform.position.y, transform.position.z);
		}


		// instance objects

		if (Camera.current) {
			float width = Camera.current.orthographicSize * Screen.width / Screen.height;
			float height = Camera.current.orthographicSize;

			var CameraCurrentPos = Camera.current.transform.position;

			Vector3 position_diamond = new Vector3 (Random.RandomRange (CameraCurrentPos.x - width + 1f, CameraCurrentPos.x + width - 1f), CameraCurrentPos.y - height - 1f, 0);
			Vector3 position_trash = new Vector3 (Random.RandomRange (CameraCurrentPos.x - width + 1f, CameraCurrentPos.x + width - 1f), CameraCurrentPos.y - height - 1f, 0);
			Vector3 position_treasure = new Vector3 (Random.RandomRange (CameraCurrentPos.x - width + 1f, CameraCurrentPos.x + width - 1f), CameraCurrentPos.y - height - 1f, 0);

			if (TimeRemain <= 0) {
				if (speed > 0) {
					Instantiate (diamond, position_diamond, Quaternion.identity);
					TimeRemain = StaticVarManager.TimeReMain;
				}
			}

			if (TimeRemain_Trash <= 0) {
				if (speed > 0) {
					Instantiate (trash, position_trash, Quaternion.identity);
					TimeRemain_Trash = StaticVarManager.TimeReMain_Trash;
				}
			}
			if (TimeRemain_Treasure <= 0) {
				if (speed > 0) {
					Instantiate (treasure, position_treasure, Quaternion.identity);
					TimeRemain_Treasure = StaticVarManager.TimeReMain_Treasure;
				}
			}
		}

		// clock down to instance diamond
		if (TimeRemain > 0) {
			TimeRemain -= Time.deltaTime;
		}

		if (TimeRemain_Trash > 0) {
			TimeRemain_Trash -= Time.deltaTime;
		}

		if (TimeRemain_Treasure > 0) {
			TimeRemain_Treasure -= Time.deltaTime;
		}

		if (can_move == false) {
#if UNITY_ANDROID

			transform.Translate (Input.acceleration.x * 2 / 3, 0, 0);
			//rigid.velocity = new Vector2(Input.acceleration.x * speed, transform.position.y, transform.position.z);
#endif

#if UNITY_EDITOR
			//rigid.velocity = transform.TransformDirection(Vector3.right *speed);
			rigid.velocity = new Vector2 (Input.GetAxis ("Horizontal") * 5f, rigid.velocity.y);
			//transform.Translate(Input.GetAxis("Horizontal") / 10, 0, 0);
#endif
			//transform.Translate(Vector3.down * Time.deltaTime * speed);
			//rigid.velocity = transform.TransformDirection(Vector3.down * speed);
			rigid.velocity = new Vector2 (rigid.velocity.x, -speed);
		}
	}



	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "diamond") {
			if (first_blood == true) {
				if (number_object < max_object) {
					total_diamond++;
					other.gameObject.AddComponent<MoveForwardController> ();
					other.transform.tag = "Finish";
					//Debug.Log("Ok");
					other.GetComponent<Collider2D> ().isTrigger = false;
					number_object++;
				}
			}
			if (first_blood == false) {
				speed = -5f;
				first_blood = true;
				//Instantiate(explosion, transform.position, Quaternion.identity);
			}


		}

		if (other.tag == "boomb") {
			if (first_blood == true) {
				if (number_object < max_object) {
					total_boomb++;
					other.gameObject.AddComponent<MoveForwardController> ();
					other.transform.tag = "Finish";
					//Debug.Log("Ok");
					other.GetComponent<Collider2D> ().isTrigger = false;
					number_object++;
				}
			}
			if (first_blood == false) {
				speed = -5f;
				first_blood = true;
				//Instantiate(explosion, transform.position, Quaternion.identity);
			}
		}

		if (other.tag == "treasure") {

			//Debug.Log("Ok");
			if (first_blood == true) {
				if (number_object < max_object) {
					total_treasure++;
					other.gameObject.AddComponent<MoveForwardController> ();
					other.transform.tag = "Finish";
					//Debug.Log("Ok");
					other.GetComponent<Collider2D> ().isTrigger = false;
					number_object++;
				}
			}
			if (first_blood == false) {
				speed = -5f;
				first_blood = true;
				//Instantiate(explosion, transform.position, Quaternion.identity);
			}


		}

		if (other.tag == "shop") {
			Time.timeScale = 0f;
			Total_Diamond.text = "x " + total_diamond;
			Total_Diamond_Price.text = "+" + total_diamond * StaticVarManager.price_diamond;

			Total_Boomb.text = "x " + total_boomb;
			Total_Boomb_Price.text = "" + total_boomb * StaticVarManager.price_boom;

			Total_Treasure.text = "x " + total_treasure;

			Total_Money.text = "Total:$" + (total_diamond * StaticVarManager.price_diamond + total_boomb * StaticVarManager.price_boom);

			PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + (total_diamond * StaticVarManager.price_diamond + total_boomb * StaticVarManager.price_boom));
			Trading_Shop.SetActive (true);



			can_move = true;
		}
	}



	public void refresh ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void loadShop ()
	{
		Application.LoadLevel ("Shop");
	}

	void addCameraController ()
	{
		if (Camera.current) {
			//Camera.current.Get
		}
	}
}
