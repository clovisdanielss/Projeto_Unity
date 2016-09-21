using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickEvent : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;
	public int z;
	public GameObject prefab,prefabLine,prefabTerminal;
	private GameObject lastObject;
	private int option = 0;
	private List<GameObject> nodes;

	void Start () {
		nodes = new List<GameObject> ();
	}
	
	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray,out hit))
		{	
			Debug.Log(hit.transform.gameObject.tag);
			if(Input.GetButtonDown("Fire1") && option == 1){
				GameObject obj=Instantiate(prefab,new Vector3(hit.point.x,hit.point.y,z), Quaternion.identity) as GameObject;
				nodes.Add(obj);
			}
			if(Input.GetButtonDown("Fire1") && option == 2){
				GameObject obj=Instantiate(prefabTerminal,new Vector3(hit.point.x,hit.point.y,z), Quaternion.identity) as GameObject;
				nodes.Add(obj);
			}
			if(Input.GetButton("Fire1") && option == 3){ 
				// Para ser click and click, troque esse evento por true. Desse jeito é click and drag[ACIMA];
				if(Input.GetButtonDown("Fire1")&& hit.transform.gameObject.tag == "node"){
					Debug.Log(hit.point);
					lastObject = Instantiate(prefabLine,new Vector3(hit.point.x,hit.point.y,z), Quaternion.identity) as GameObject;
					LineRenderer lr = lastObject.GetComponent<LineRenderer>();
					lr.SetWidth(0.05F,0.05F);
					lr.SetVertexCount(2);
					lr.SetPosition(0,new Vector3(hit.point.x,hit.point.y,z));					
				}
				if(lastObject != null){
					LineRenderer lr = lastObject.GetComponent<LineRenderer>();
					lr.SetPosition(1,new Vector3(hit.point.x,hit.point.y,z));
				}
			}
			if(Input.GetButtonUp("Fire1") && hit.transform.gameObject.tag != "node" && lastObject != null && option == 3){
				Destroy(lastObject);
			}
		}	
	}
	
	public void SetNewLine(){
		option = 3;
	}
	
	public void SetNewNode(){
		option = 1;
	}

	public void SetNewTerminalNode(){
		option = 2;
	}
}

