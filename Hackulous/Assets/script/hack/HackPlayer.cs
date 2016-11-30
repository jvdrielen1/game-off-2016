using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HackPlayer : MonoBehaviour {

	public float speed = 3.5f;
	public int points = 0;
	public int pointsNeeded = 35;

	public float timeLeft = 60;

	public GameObject time, scoreboard;

	// Use this for initialization
	void Start () {
		timeLeft = 60;
		points = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;

		if (Input.GetKey (KeyCode.W)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
		}

		if (Input.GetKey (KeyCode.S)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + -speed * Time.deltaTime, transform.position.z);
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.position = new Vector3 (transform.position.x + -speed * Time.deltaTime, transform.position.y, transform.position.z);
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.position = new Vector3 (transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
		}

		if (Input.GetKey (KeyCode.O)) {
			points++;
		}

		if (Mathf.Round(timeLeft) == 0) {
			GameObject camera = GameObject.Find ("Main Camera");
			SwitchScene s = camera.GetComponent<SwitchScene> ();
			HackManager.Instance ().setSuccesfull (false);
			s.openScene ("TextComposerScene");
		}

		updateScoreboard ();
	}

	void OnCollisionEnter2D(Collision2D coll){
		/*if (coll.gameObject.name == "Trail") {
			GameObject camera = GameObject.Find ("Main Camera");
			SwitchScene s = camera.GetComponent<SwitchScene> ();
			HackManager.Instance ().setSuccesfull (false);
			s.openScene ("TextComposerScene");
		}*/

		if (coll.gameObject.name == "Skull") {
			points++;
			coll.gameObject.transform.position = new Vector3(Random.Range(-7.8f, 7.8f), Random.Range(-2.9f, 5f), coll.gameObject.transform.position.z);

			if (points >= pointsNeeded) {
				GameObject camera = GameObject.Find ("Main Camera");
				SwitchScene s = camera.GetComponent<SwitchScene> ();
				HackManager.Instance ().setSuccesfull (true);
				s.openScene ("TextComposerScene");	
			}
		}
	}

	void updateScoreboard(){
		time.GetComponent<Text> ().text = "Time left:" + Mathf.Round(timeLeft);
		scoreboard.GetComponent<Text> ().text = "Points:" + points + "/" + pointsNeeded;
	}
}
