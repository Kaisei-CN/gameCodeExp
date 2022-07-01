using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

	public float speed;
	public int direction = 1;

	public Side side;
	public float power = 1;

	public float lifeTime = 3f;
	void Start () {
		Destroy(this.gameObject, lifeTime);	//3s后自毁
	}
	
	// Update is called once per frame
	void Update () {
		OnUpdate();
	}
	public virtual void OnUpdate()
    {
		this.transform.position += new Vector3(speed * Time.deltaTime * direction, 0, 0);

		//子弹飞出屏幕后需要销毁
		if (Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position)) == false)
		{
			Destroy(this.gameObject, 0.5f); //延迟0.5s再销毁
		}
	}
}
