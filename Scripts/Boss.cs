using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public GameObject missileTemplate;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform battery;

    Misssile missile = null;
    public Unit target;

    public void OnMissileLoad()
    {

        GameObject go = Instantiate(missileTemplate, firePoint3); //于父节点创建导弹
        missile = go.GetComponent<Misssile>();
        missile.target = this.target.transform;

    }
    public void OnMissileLaunch()
    {
        if (missile == null)    //所有游戏对象都要判断一下
            return;
        missile.transform.SetParent(null);

        missile.Launch();
    }
    public override void OnStart()
    {
        StartCoroutine(FireMissile());    //创建临时协程，用于控制时间
    }
    IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(1f);    //等待 5s 后播放动画
        ani.SetTrigger("Skill");
        Debug.Log("导弹协程启动");
    }
    public override void OnUpdate()
    {
        
    }
}
