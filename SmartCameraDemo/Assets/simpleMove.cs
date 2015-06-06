using UnityEngine;
using System.Collections;

public class simpleMove : MonoBehaviour {

	private Vector3 force = new Vector3 (500, 0, 0);

	void Update()
	{

		if(Input.GetKey(KeyCode.RightArrow))
		   {
			transform.position = new Vector3 (transform.position.x + (3f)*Time.deltaTime, transform.position.y, transform.position.z);
		    }
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position = new Vector3 (transform.position.x + (-3f)*Time.deltaTime, transform.position.y, transform.position.z);
		}

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 500f));
		}


	}
}
