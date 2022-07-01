using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour {

	public Rigidbody2D rigidbodyBird;	//组件相关
	public Animator ani;

	public float speed = 5.0f;  //子弹相关
	public float fireRate = 10f;
	public GameObject bulletTemplate;
	public float fireTimer = 0;

	protected bool death = false;	//生命相关
	public float HP = 10f;

	protected Vector3 initPos;
	public UnityAction<int> OnScore;    //内置action委托，不用定义委托

	public void Init()
	{
		this.transform.position = initPos;
		//this.Idle();
		this.Fly();
		death = false;   //死亡重开后要重新改回非死亡状态
		HP = 10f;
	}
	void Start()
    {
		ani = GetComponent<Animator>();
		initPos = this.transform.position;
		OnStart();
	}
	void Update()
    {
		fireTimer += Time.deltaTime;
		OnUpdate();
	}
	public virtual void OnStart()
    {

    }
	public virtual void OnUpdate()
    {

    }


	public void Idle()
	{
		rigidbodyBird.simulated = false;
		ani.SetTrigger("Idle");
	}
	public void Fly()
	{
		rigidbodyBird.simulated = true;
		ani.SetTrigger("Fly");
	}
	public void Fire()
	{
		if (fireTimer > 1f / fireRate)
		{
			GameObject go = Instantiate(bulletTemplate);
			go.transform.position = this.transform.position;
            if (go.GetComponent<Element>().side == Side.player)
            {
				go.GetComponent<Element>().direction = 1;
			}
			else go.GetComponent<Element>().direction = -1;
			/*SpriteRenderer[] sprs = go.GetComponentsInChildren<SpriteRenderer>();   //component后面加 s 转为泛型
            for (int i = 0; i < sprs.Length; i++)
            {
                sprs[i].color = Color.red;
            }*/
			fireTimer = 0f;
		}
	}
	/*public void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("发生碰撞");
		this.die();
	}*/
}
