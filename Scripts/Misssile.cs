using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misssile : Element {

    public Transform target;
    public bool running = false;

    public override void OnUpdate()
    {
        if (!running)   //当还没开始的时候，什么都不做
        {
            return;
        }
        if (target != null)
        {
            Vector3 dir = (target.position - this.transform.position);
            if (dir.magnitude < 0.1)
            {
                Explod();
            }
            this.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            this.transform.position += speed * Time.deltaTime * dir.normalized;
        }
    }
    public void Launch()
    {
        this.running = true;
    }
    public void Explod()
    {

    }
}
