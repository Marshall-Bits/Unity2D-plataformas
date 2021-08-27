using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = -1f;
    Rigidbody2D enemyRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if(isFacingLeft())
      {
          enemyRigidBody.velocity = new Vector2(enemySpeed, enemyRigidBody.velocity.y);
      }
      else
      {
          enemyRigidBody.velocity = new Vector2(-enemySpeed, enemyRigidBody.velocity.y);
      }
    }

    bool isFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(enemyRigidBody.velocity.x, transform.localScale.y);
    }
}
