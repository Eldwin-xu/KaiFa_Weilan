using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSphere : MonoBehaviour
{
    public float friction;
    public float mass;
    public float radius;

    public GameObject other;
    public Rigidbody rb;
    public float pushForce = 1;

    [NonSerialized]
    public Vector3 currentV;
    Vector3 lastVelocity;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        frictionTest();
        lastVelocity = currentV;

     }

    public void frictionTest()
    {
        //摩擦力
        Vector3 frictionDeltaV = -Time.deltaTime * friction * currentV.normalized;
        //防止摩擦力反向运动
        Vector3 finalV = currentV + frictionDeltaV;
        if (finalV.x * currentV.x <= 0)
            frictionDeltaV.x = -currentV.x;
        if (finalV.y * currentV.y <= 0)
            frictionDeltaV.y = -currentV.y;
        if (finalV.z * currentV.z <= 0)
            frictionDeltaV.z = -currentV.z;

        //应用加速度
        Vector3 curV = currentV + frictionDeltaV;
        transform.Translate((curV + currentV) * Time.deltaTime / 2);
        currentV = curV;
    }

    public void ballCollision()
    {
       /* //摩擦力
        Vector3 frictionDeltaV = -Time.deltaTime * friction * preV.normalized;
        //防止摩擦力反向运动
        Vector3 finalV = preV + frictionDeltaV;
        if (finalV.x * preV.x <= 0)
            frictionDeltaV.x = -preV.x;
        if (finalV.y * preV.y <= 0)
            frictionDeltaV.y = -preV.y;
        if (finalV.z * preV.z <= 0)
            frictionDeltaV.z = -preV.z;
        //检测是否与其他球相撞
        Vector3 prePos = transform.position;
        Vector3 pos = transform.position;
        if (other != null)
        {
            CollosionTest sphere = other.GetComponent<CollosionTest>();
            Vector3 otherPos = other.transform.position;

            //球体间碰撞检测，判断球心距离与两球半径之和即可
            if (Vector3.Distance(pos, otherPos) < 0.5 + sphere.radius) //简单起见，认为自己的半径为0.5
            {
                Debug.Log("碰撞发生2");
                Vector3 v1 = preV;
                float m1 = 1.0f; // 简单起见，认为自己的质量为1
                Vector3 v2 = sphere.currentV;
                float m2 = sphere.mass;

                preV = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
                sphere.currentV = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);

                //如果有碰撞，位置回退，防止穿透
                transform.position = prePos;
            }
        } */
    }




    void OnCollisionEnter(Collision collision)
    {
        /* if(collision.gameObject.name == "Wall1")
         {
             transform.forward = Vector3.Reflect(collision.contacts[0].point, collision.contacts[0].normal);
             currentV *= -1;
             /*float bounce = 6f; //amount of force to apply
             rb.AddForce(collision.contacts[0].normal * pushForce / 5, ForceMode.Impulse);
             /*rb.AddForce(Vector3.right * pushForce / 3, ForceMode.Impulse);
             Debug.Log("1");
             frictionTest();
             /*currentV *= -1;
         }
         if (collision.gameObject.name == "Wall2")
         {
             Debug.Log("2");
             transform.forward = Vector3.Reflect(collision.contacts[0].point, collision.contacts[0].normal);
             currentV *= -1;
             /*float bounce = 6f; //amount of force to apply
             rb.AddForce(collision.contacts[0].normal * pushForce / 5, ForceMode.Impulse);
             /*rb.AddForce(Vector3.left * pushForce /3, ForceMode.Impulse);
             frictionTest();
             /*currentV *= -1;
         }
         if (collision.gameObject.name == "Wall3")
         {
             /*rb.AddForce(Vector3.back * pushForce / 3, ForceMode.Impulse);
             Debug.Log("3");
             /*currentV *= -1;*/
        /*frictionTest();
        float bounce = 6f; //amount of force to apply
        rb.AddForce(collision.contacts[0].normal * pushForce / 5, ForceMode.Impulse);
        transform.forward = Vector3.Reflect(collision.contacts[0].point, collision.contacts[0].normal);
        currentV *= -1;
    }
    if (collision.gameObject.name == "Wall4")
    {
        /*rb.AddForce(Vector3.forward * pushForce / 3, ForceMode.Impulse);
        Debug.Log("4");
        /*currentV *= -1;*/
        /*frictionTest();

        rb.AddForce(collision.contacts[0].normal * pushForce / 5, ForceMode.Impulse);
        transform.forward = Vector3.Reflect(collision.contacts[0].point, collision.contacts[0].normal);
        currentV *= -1;
    }
    if (collision.gameObject.name == "Sphere")
    {
        /*currentV *= -1;*/



        /* private void Bounce(Vector3 collisionNormal)
         {
             var speed = lastFrameVelocity.magnitude;
             var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

             Debug.Log("Out Direction: " + direction);
             rb.velocity = direction * Mathf.Max(speed, minVelocity);
         }
        */
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        currentV = direction * Mathf.Max(speed, 1f);
    }
}
