using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour {

	public enum Game_Status
    {
		Ready,
		InGame,
		GameOver
    }
	private Game_Status status;
	Game_Status Status	//使用属性而不是字段
    {
        get { return status; }
        set { this.status = value;  //更改状态，也要更改UI
			this.UpdateUI();
		}
    }

	public GameObject panelReady;
	public GameObject panelInGame;
	public GameObject panelGameOver;

	//public PipleLineManager pipleLineManager;   //拖拽获取管道管理对象
	public UnitManager unitManager;
	public Player player;

	public int score;
	public int Score	//定义属性
    {
		get { return score; }
		set
        {
			this.score = value;
			this.uiScore.text = this.score.ToString();
			this.uiScore2.text = this.score.ToString();
        }
    }
	public Text uiScore;
	public Text uiScore2;

	public Slider hpBar;


	// Use this for initialization
	void Start () {
		this.panelReady.SetActive(true);
		Status = Game_Status.Ready;
        this.player.OnDeath += Player_OnDeath;  //按tab后自动创建的方法名
		this.player.OnScore = OnPlayerScore;
	}
	void OnPlayerScore(int score)
    {
		this.Score += score;
    }

    private void Player_OnDeath()	//自动创建的方法
    {
		Status = Game_Status.GameOver;  //通过委托来知道角色死亡，
										//并且当玩家死亡后会得到事件的通知，并做出及时的响应
		//this.pipleLineManager.Stop();
		this.unitManager.Stop();
    }

    // Update is called once per frame
    void Update () 
	{
		hpBar.value = Mathf.Lerp(hpBar.value, player.HP, 0.01f);
	}
	public void StartGame()
    {
		this.Status = Game_Status.InGame;
		//pipleLineManager.StartRun();    //开启管道生成协程
		unitManager.Begin();	//开启敌人生成协程
		Debug.Log("start");
		
		player.Fly();
		hpBar.value = player.HP;
	}
	public void UpdateUI()
    {
		this.panelReady.SetActive(this.Status == Game_Status.Ready);
		this.panelInGame.SetActive(this.Status == Game_Status.InGame);
		this.panelGameOver.SetActive(this.Status == Game_Status.GameOver);
    }

	public void Restart()
    {
		this.Status = Game_Status.Ready;
		//this.pipleLineManager.Init();
		this.player.Init();
    }
}
