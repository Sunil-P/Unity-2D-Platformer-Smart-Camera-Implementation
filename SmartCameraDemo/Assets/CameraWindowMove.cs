using UnityEngine;
using System.Collections;

public class CameraWindowMove : MonoBehaviour {
	private BoxCollider2D playerBoxCollider;
	private BoxCollider2D WindowBoxCollider;

	private Vector2 _playerMin, _playerMax;
	private Vector2 _WindowMin, _WindowMax;
	private GameObject Player;
	private float x, y;

	public Vector2 velocity, smoothing;
	public int lookAhead;

	private float Maxdistance;
	private float distanceY1;
	void Awake()
	{
		playerBoxCollider = GameObject.Find ("Player").GetComponent<BoxCollider2D>();
		WindowBoxCollider = GetComponent<BoxCollider2D>();
		Player = GameObject.Find ("Player");

	}

	void Start()
	{
		x=transform.position.x;
		y=transform.position.y;
		lookAhead=0;
		Maxdistance = ((WindowBoxCollider.bounds.size.x/2)-(playerBoxCollider.bounds.size.x/2));
		distanceY1 = ((WindowBoxCollider.bounds.size.y/2)-(playerBoxCollider.bounds.size.y/2));
	}
	void Update () {
		_playerMin = playerBoxCollider.bounds.min;
		_playerMax = playerBoxCollider.bounds.max;
		_WindowMin = WindowBoxCollider.bounds.min;
		_WindowMax = WindowBoxCollider.bounds.max;
		if (_playerMax.x >= _WindowMax.x)
		{
			x=Player.transform.position.x-Maxdistance;
			lookAhead=1;
		}
		else if (_playerMin.x <= _WindowMin.x)
		{
			x=Player.transform.position.x+Maxdistance;
			lookAhead=-1;
		}

		else 
			lookAhead =0;
		if(_playerMax.y > _WindowMax.y)
		{
			y=Player.transform.position.y;
		}
		if(_playerMin.y < _WindowMin.y)
		{
			y=Player.transform.position.y;
			
		}


		transform.position = new Vector3(x, y, transform.position.z);

	}
}
