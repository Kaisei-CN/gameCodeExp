using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipleLineManager : MonoBehaviour {

	public GameObject Template; //模板

	List<PipleLine> pipleLines = new List<PipleLine>();	//泛型列表，用于存储生成的管道

	Coroutine coroutine = null;	//协程初始化

	public void Init()
    {
        for (int i = 0; i < pipleLines.Count; i++)
        {
			Destroy(pipleLines[i].gameObject);
        }
		pipleLines.Clear();
    }


	public void StartRun()
    {
		coroutine = StartCoroutine(GeneratePipleLines());	//协程赋值并启动
    }
	public void Stop()
    {
		StopCoroutine(coroutine);
        //StopCoroutine(GeneratePipleLines());	//停止协程方法
        for (int i = 0; i < pipleLines.Count; i++)	//将 3 个管道依次停止
        {
			pipleLines[i].enabled = false;
        }
    }
	IEnumerator GeneratePipleLines()	//定义协程方法，用于生成管道
    {
        for (int i = 0; i < 3; i++)		//只循环 3 次
        {
			if (pipleLines.Count < 3)
				GeneratePipleLine();
            else
            {
				pipleLines[i].enabled = true;
				pipleLines[i].Init();

			}
			yield return new WaitForSeconds(2.0f);
		}
    }

	void GeneratePipleLine()
    {
        if (pipleLines.Count < 3)
        {
			GameObject obj = Instantiate(Template, this.transform);
			PipleLine p = obj.GetComponent<PipleLine>();
			pipleLines.Add(p);
		}

	}
}
