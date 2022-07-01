using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipleLine : MonoBehaviour {

	public float speed;
	public float maxRange;
	public float minRange;

	void Start () 
	{
		Init();
	}

	float t = 0;
	public void Init()	//初始化
    {
		float y = Random.Range(minRange, maxRange); //随机生成竖直位置
		this.transform.localPosition = new Vector3(0, y, 0);

	}
	
	void Update () 
	{
		this.transform.position += new Vector3(-speed, 0) * Time.deltaTime;
		t += Time.deltaTime;
        if (t > 6f)
        {
			t = 0;
			Init();
        }
	}
}
