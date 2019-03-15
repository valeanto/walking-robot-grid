using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION { UP, DOWN, LEFT, RIGHT }
public class PlayerMovement : MonoBehaviour
{
    private bool canMove = true, moving = false;
    private int speed = 5, buttonCooldown = 0;
    private DIRECTION dir = DIRECTION.DOWN;
    private Vector3 pos;
    private Animator ani;



    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        ani.Play("PlayerDown");
    }


    // Update is called once per frame
    void Update()
    {
        buttonCooldown--;
        if (canMove)
        {
            pos = transform.position;
            move();
        }

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
            transform.position = pos;

            if (transform.position == pos)
            {
                moving = false;
                canMove = true;
            }
        }
    }
    private void move()
    {
        if (buttonCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (dir != DIRECTION.UP)
                {
                    ani.Play("PlayerUp");
                    buttonCooldown = 5;
                    dir = DIRECTION.UP;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.up;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (dir != DIRECTION.DOWN)
                {
                    ani.Play("PlayerDown");
                    buttonCooldown = 5;
                    dir = DIRECTION.DOWN;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.down;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (dir != DIRECTION.LEFT)
                {
                    ani.Play("PlayerLeft");
                    buttonCooldown = 5;
                    dir = DIRECTION.LEFT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.left;
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (dir != DIRECTION.RIGHT)
                {
                    ani.Play("PlayerRight");
                    buttonCooldown = 5;
                    dir = DIRECTION.RIGHT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.right;
                }
            }
        }
    }
}