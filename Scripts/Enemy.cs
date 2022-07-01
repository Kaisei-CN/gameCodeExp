using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit {

	public delegate void DeathNotify(); //定义委托，只能在子类实现，不能放在父类
	public event DeathNotify OnDeath;   //定义事件，管理者需要知道被管理者发生了什么事

	public float lifeTime = 4f;		//存活时间
	public enemy_type enemyType;    //定义敌人类型
	public Vector2 range;   //设置随机生成位置的范围
	private float initY = 0;


    public override void OnStart()
    {
		Destroy(this.gameObject, lifeTime);

		initY = Random.Range(range.x, range.y); //随机生成竖直位置
		this.transform.localPosition = new Vector3(0, initY, 0);
	}

    public override void OnUpdate()
    {

		if (this.death) return;     //判断为死后则不再接收鼠标点击

		float y = 0;
		if (enemyType == enemy_type.swing_enemy)
		{
			y = Mathf.Sin(Time.timeSinceLevelLoad) * 3f;
		}
		this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime * speed, initY + y);
		this.Fire();
	}


    public void die()
	{
		this.ani.SetTrigger("Die");
		this.death = true;
		if (this.OnDeath != null)   //事件不为空，即玩家已死
		{
			this.OnDeath();     //写在Game中的方法会被调用
		}
		Destroy(this.gameObject, 0.25f);
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		Element bullet = col.GetComponent<Element>();
		if (bullet == null)
		{
			return;
		}
		if (bullet.side == Side.player)
		{
			this.die();
		}
	}

}
