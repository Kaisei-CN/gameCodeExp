using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Unit {

	//public float addForce = 100f;
	public delegate void DeathNotify(); //定义委托，只能在子类实现，不能放在父类——事件不可派生
	public event DeathNotify OnDeath;   //定义事件，管理者需要知道被管理者发生了什么事

    public override void OnUpdate()
    {
        if (this.death) return;     //判断为死后则不再接收鼠标点击

        /*if (Input.GetMouseButtonDown(0))
        {
			rigidbodyBird.velocity = Vector2.zero;
			rigidbodyBird.AddForce(new Vector2(0, addForce), ForceMode2D.Force);
        }*/
        Vector2 pos = this.transform.position;  //获取角色位移控制
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        this.transform.position = pos;

        if (Input.GetButton("Fire1"))	//发射子弹
        {
            this.Fire();
        }
    }

    public void die()
    {
		this.death = true;
		if(this.OnDeath != null)	//事件不为空，即玩家已死
        {
			this.OnDeath();		//写在Game中的方法会被调用
        }
    }
	
	public void OnTriggerEnter2D(Collider2D col)
    {
		Element bullet = col.GetComponent<Element>();	
		Enemy enemy = col.GetComponent<Enemy>();
		if (bullet == null && enemy == null)
        {
			return;
        }
        if (bullet != null && bullet.side == Side.enemy)	//扣血判断逻辑
        {
			HP -= bullet.power;
            if (HP <= 0)
            {
				this.die();
			}
		}
        if (enemy != null)
        {
			HP = 0;
			die();
        }
    }
	/*public void OnTriggerExit2D(Collider2D col)
	{
        if (col.transform.name.Equals("ScoreArea"))
        {
            if (this.OnScore != null)
                this.OnScore(1);
        }
    }*/
}
