using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollosionTest : MonoBehaviour
{
    public float force;
    public float friction;

    public Rigidbody rb1;
    public GameObject other;

    //上一帧结束时的速度
    private Vector3 preV;
    private Vector3 curV;
    Vector3 lastVelocity;

    [NonSerialized]
    public Vector3 currentV;

    public Vector3 frictionDeltaV; //摩擦力
    public Vector3 fDir;//物体运动方向

    public float pushForce;

    void Start()
    {
        pushForce = 3;
        preV = Vector3.zero;
        rb1 = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        //摩擦力
       /* Vector3 frictionDeltaV = -Time.deltaTime * friction * preV.normalized;*/
        //防止摩擦力反向运动
        Vector3 finalV = preV + frictionDeltaV;
        if (finalV.x * preV.x <= 0)
            frictionDeltaV.x = -preV.x;
        if (finalV.y * preV.y <= 0)
            frictionDeltaV.y = -preV.y;
        if (finalV.z * preV.z <= 0)
            frictionDeltaV.z = -preV.z;

        //计算用户用力方向
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 fDir = new Vector3(moveHorizontal, 0.0f, moveVertical);
        fDir.Normalize();


        //计算加速度
        Vector3 acceleration = force * fDir;

        Vector3 prePos = transform.position;

        //应用加速度
        Vector3 curV = preV + Time.deltaTime * acceleration + frictionDeltaV;
        transform.Translate((curV + preV) * Time.deltaTime / 2);
        preV = curV;


        //检测是否与其他球相撞
        Vector3 pos = transform.position;
        if (other != null)
        {
            OtherSphere otherSphere = other.GetComponent<OtherSphere>();
            Vector3 otherPos = other.transform.position;

            //球体间碰撞检测，判断球心距离与两球半径之和即可
            if (Vector3.Distance(pos, otherPos) < 0.5 + otherSphere.radius) //简单起见，认为自己的半径为0.5
            {
                Debug.Log("碰撞发生!");
                Vector3 v1 = preV;
                float m1 = 1.0f; // 简单起见，认为自己的质量为1
                Vector3 v2 = otherSphere.currentV;
                float m2 = otherSphere.mass;

                preV = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
                otherSphere.currentV = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);

                //如果有碰撞，位置回退，防止穿透
                transform.position = prePos;
            }
        }

    }


    private void OnCollisionEnter(Collision collision)

    {
        /*curV = Vector3.zero;*/
        fDir = Vector3.zero;
        /*frictionDeltaV = Vector3.zero;
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        currentV = direction * Mathf.Max(speed, 1f); */

        if(collision.gameObject.name == "Wall1")
        {
            preV = Vector3.zero;
            frictionDeltaV = Vector3.zero;
            rb1.AddForce(Vector3.right * pushForce, ForceMode.Impulse);
        }
        if (collision.gameObject.name == "Wall2")
        {
            preV = Vector3.zero;
            frictionDeltaV = Vector3.zero;
            rb1.AddForce(Vector3.left * pushForce, ForceMode.Impulse);
        }
        if (collision.gameObject.name == "Wall3")
        {
            preV = Vector3.zero;
            frictionDeltaV = Vector3.zero;
            rb1.AddForce(Vector3.back * pushForce, ForceMode.Impulse);
        }
        if (collision.gameObject.name == "Wall4")
        {
            preV = Vector3.zero;
            frictionDeltaV = Vector3.zero;
            rb1.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
        }
    }

}


