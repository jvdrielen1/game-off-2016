using UnityEngine;
using System.Collections;

public class SphereMove : MonoBehaviour {

	public float speed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		transform.position = new Vector3 (transform.position.x + horizontal * Time.deltaTime * speed, transform.position.y + vertical * Time.deltaTime * speed, 0);
	}
}
