using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ArcDraw : MonoBehaviour {

	public Transform startLeft;
	
	public Transform end;

	public Transform middle;

	public Transform startRight;

	private LineRenderer lineRenderer;

	private int lineRendererPos = 10;
	void Start () {
		this.lineRenderer = GetComponent<LineRenderer>();
		this.lineRenderer.numPositions = lineRendererPos;
	}
	
	// Update is called once per frame
	void Update () {
		DrawAim2();
	}
	
	void DrawAim() {   
		var newEnd = Input.mousePosition;
		newEnd.z = Input.mousePosition.y.Map(0,Screen.height,0,10);
		newEnd = Camera.main.ScreenToWorldPoint(newEnd);
		end.position = new Vector3(newEnd.x, 0, newEnd.z);

		var newMiddle = Vector3.Lerp(startLeft.position, end.position, 0.5f);
		newMiddle.y = Input.mousePosition.y.Map(0,Screen.height,0,3);
						
		middle.position = newMiddle;
		Vector3 currentPos = startLeft.position;
		lineRenderer.SetPosition (0 , startLeft.position);
		lineRenderer.SetPosition (1 , newMiddle);
		lineRenderer.SetPosition (2 , end.position);
		Debug.Log(newMiddle);
	}

	void DrawAim2() {   
		var newEnd = Input.mousePosition;
		newEnd.z = Input.mousePosition.y.Map(0,Screen.height,0,10);
		newEnd = Camera.main.ScreenToWorldPoint(newEnd);
		end.position = new Vector3(newEnd.x, 0, newEnd.z);

		Vector3 currentPos = startLeft.position;
		for(float i=0;i<lineRendererPos;i++){
			var posHeigth = i.Map(0, lineRendererPos-1, 0, Mathf.PI);	
			posHeigth = Convert.ToSingle(Math.Sin(posHeigth));
			currentPos = Vector3.Lerp(currentPos, end.position, i/lineRendererPos);
			Debug.Log(posHeigth);
			currentPos.y = Input.mousePosition.y.Map(0f,Screen.height,0f,4f) * posHeigth;
			lineRenderer.SetPosition (Convert.ToInt32(i) , currentPos);
		}
	}
}
