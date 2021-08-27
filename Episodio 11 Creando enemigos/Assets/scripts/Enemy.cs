using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = -1f;
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if(isFacingLeft())
      {
          rigidbody.velocity = new Vector2(enemySpeed, rigidbody.velocity.y);
      }
      else
      {
          rigidbody.velocity = new Vector2(-enemySpeed, rigidbody.velocity.y);
      }
    }

    bool isFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(rigidbody.velocity.x, transform.localScale.y);
    }
}
