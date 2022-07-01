using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	public GameObject enemyTemplate;	// 1 号敌人
	public GameObject enemy2Template;	// 2 号敌人
	public GameObject enemy3Template;	// 3 号敌人


	public List<Enemy> enemies = new List<Enemy>();

	Coroutine coroutine = null; //协程初始化
	public float speed1 = 1.0f;	//各类敌人的生成时间间隔
	public float speed2 = 2.0f;
	public float speed3 = 5.0f;
	int timer1 = 0;		//定义各类敌人的计时器
	int timer2 = 0;
	int timer3 = 0;
	

	public void Begin()
    {
		coroutine = StartCoroutine(GenerateEnemy());   //协程赋值并启动
	}
	public void Stop()
    {
		StopCoroutine(coroutine);	//停止协程
		this.enemies.Clear();	//清理列表
    }
	IEnumerator GenerateEnemy()    //定义协程方法，只执行一次，用于生成，如果不做特殊的事情那就和普通的函数没有区别
	{
        while (true)
        {
            if (timer1 > speed1)
            {
				CreateEnemy(enemyTemplate);
				timer1 = 0;
			}
			if (timer2 > speed2)
            {
				CreateEnemy(enemy2Template);
				timer2 = 0;
            }
			if (timer3 > speed3)
            {
				CreateEnemy(enemy3Template);
				timer3 = 0;
            }
			timer1++;
			timer2++;
			timer3++;
			yield return new WaitForSeconds(1f);	//协程 1s 循环一次
		}

	}
	void CreateEnemy(GameObject templates)
	{
		if (templates == null) return;
		GameObject obj = Instantiate(templates, this.transform);
		Enemy p = obj.GetComponent<Enemy>();
		this.enemies.Add(p);
	}
}
