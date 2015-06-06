using UnityEngine;
using System.Collections;
using System;

public class CameraSmooth2D : MonoBehaviour {


	private Transform cameraWindow ;
	private Vector2 velocity;
	private float xRound, yRound;
	private float nearestTenthX;
	public Vector2 UpperLeftPos, LowerRightPos;
	private float cameraHalfWidth;
	public Vector2 smoothing;
	public bool isFollowing ;

	public bool RestoreCamera= false;
	public bool moveCamera = false;
	private float finalPos;
	private Vector2 restoreVector;

	private float x, y;

	private CameraWindowMove WindowScript;

	private BoxCollider2D cameraBounds;
	private Vector2 CameraXlimit;

	void Awake()
	{
		cameraWindow = GameObject.Find ("CameraWindow").GetComponent<Transform>();
		UpperLeftPos.y = transform.position.y + GetComponent<Camera>().orthographicSize;
		LowerRightPos.x = transform.position.x + cameraHalfWidth;
		UpperLeftPos.x = transform.position.x - cameraHalfWidth;
		LowerRightPos.y = transform.position.y - GetComponent<Camera>().orthographicSize;
		WindowScript = GameObject.Find ("CameraWindow").GetComponent<CameraWindowMove>();
		cameraBounds = GameObject.Find("CameraLimits").GetComponent<BoxCollider2D>();
		CameraXlimit = new Vector2 (cameraBounds.bounds.min.x, cameraBounds.bounds.max.x);

	}


	public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
	{
		float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
		return adjustedUnityUnits;
	}

	void Start () {
		cameraHalfWidth = GetComponent<Camera>().orthographicSize*((float) Screen.width/Screen.height);

	}
	
	void Update () {
		UpperLeftPos.y = transform.position.y + GetComponent<Camera>().orthographicSize;
		LowerRightPos.x = transform.position.x + cameraHalfWidth;
		UpperLeftPos.x = transform.position.x - cameraHalfWidth;
		LowerRightPos.y = transform.position.y - GetComponent<Camera>().orthographicSize;

		 x = transform.position.x;
		 y = transform.position.y;
	
		if(isFollowing)
		{
			if(WindowScript.lookAhead == 1)
			{
			x=Mathf.SmoothDamp( x, cameraWindow.position.x+5, ref velocity.x, smoothing.x);
			xRound=(float)Math.Round (x, 2);
			nearestTenthX = (int)(xRound/0.01f)*0.01f;
			xRound=nearestTenthX;
			}

			if(WindowScript.lookAhead == -1)
			{
				x=Mathf.SmoothDamp( x, cameraWindow.position.x-5, ref velocity.x, smoothing.x);
				xRound=(float)Math.Round (x, 2);
				nearestTenthX = (int)(xRound/0.01f)*0.01f;
				xRound=nearestTenthX;
			}

			if(WindowScript.lookAhead == 0)
			{
				x=Mathf.SmoothDamp( x, cameraWindow.position.x, ref velocity.x, smoothing.x);
				xRound=(float)Math.Round (x, 2);
				nearestTenthX = (int)(xRound/0.01f)*0.01f;
				xRound=nearestTenthX;
			}
			xRound = Mathf.Clamp(xRound, CameraXlimit.x+cameraHalfWidth, CameraXlimit.y-cameraHalfWidth);
			y=Mathf.SmoothDamp( y, cameraWindow.position.y+1.5f, ref velocity.y, smoothing.y);
			yRound=(float)Math.Round(y, 2);
			yRound = (int)(yRound/0.01f)*0.01f;


		}
		//NOT USED, WILL IMPROVE FURTHER LATER ON
		if (moveCamera == true)
		{
			isFollowing = false;
			y = Mathf.SmoothDamp(y, finalPos, ref velocity.y, smoothing.y);
			yRound = (float)Math.Round (y, 2);
			if(yRound <= finalPos+0.1f)
				moveCamera = false;

		}

		if(RestoreCamera == true)
		{
			y=Mathf.SmoothDamp( y ,restoreVector.y+0.1f, ref velocity.y, smoothing.y);
			yRound = (float)Math.Round (y, 2);
			if (yRound >= restoreVector.y)
			{
				isFollowing = true;
				RestoreCamera= false;
				
			}

		}
		//NOT USED, WILL IMPROVE FURTHER LATER ON

		Vector3 roundPos = new Vector3(RoundToNearestPixel(xRound, GetComponent<Camera>()),RoundToNearestPixel(yRound, GetComponent<Camera>()),transform.position.z);
		transform.position = roundPos;                


	}

	public void cameraMove(bool Pressed = true, float movementDir = -3.5f)
	{
		if(Pressed == true && moveCamera == false && RestoreCamera == false)
		{
			restoreVector.x = cameraWindow.transform.position.x;
			restoreVector.y = cameraWindow.transform.position.y;
			finalPos = y+movementDir;
			moveCamera = true;
		}
		if(Pressed == false ) 
		{				
			moveCamera = false;
			RestoreCamera = true;
		}
	}
}
