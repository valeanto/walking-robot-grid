using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DIRECTION { UP, DOWN, LEFT, RIGHT }
public class PlayerMovement : MonoBehaviour
{
    private string[] Animations = { "PlayerUp", "PlayerDown", "PlayerLeft", "PlayerRight" };
    private bool canMove = true, moving = false;
    private int speed = 5, buttonCooldown = 1;
    private bool gameStarted;
    private DIRECTION dir = DIRECTION.DOWN;
    private Vector3 pos;
    private Animator ani;

    public WorldGrid grid;
    public Tile tile;
    public InputField inputCommand;

    public DIRECTION Direction
    {
        get { return dir; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetPosition(new Vector2(2, 2));

    ani = gameObject.GetComponent<Animator>();
        ani.Play("PlayerDown");
        tile = grid.GetTile(transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            buttonCooldown--;
        }

        if (canMove)
        {
            pos = transform.position;
            move();
        }

        if (moving)
        {
            //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
            transform.position = pos;
            if (transform.position == pos)
            {
                moving = false;
                canMove = true;

                //move();
            }
        }
    }

    public void MoveBy(Vector3 deltaMove)
    {
        var newPos = transform.position + deltaMove;
        var newTile = grid.GetTile(newPos);
        if (newTile == null)
        {
            return;
        }

        tile = newTile;
        canMove = false;
        moving = true;
        pos += deltaMove;

        print("Moved to " + tile.name);
    }

    public void SetPosition(Vector2 pos, DIRECTION d)
    {
        var newTile = grid.GetTile(pos);
        if (newTile == null)
        {
            return;
        }

        tile = newTile;

        var position = tile.transform.position;
        position.x += 0.5f;
        position.y += 0.738f;
        transform.position = position;

        UpdateRotation(d);
    }

    private void UpdateRotation(DIRECTION newDir)
    {
        if(dir != newDir)
        {
            dir = newDir;
            ani.Play(Animations[(int)newDir]);
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
                    MoveBy(Vector3.up);
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
                    MoveBy(Vector3.down);
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
                    MoveBy(Vector3.left);
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
                    MoveBy(Vector3.right);
                }
            }
        }
    }

    public void ExecuteCommand(string fullCommand)
    {
        print("-->" + fullCommand);
        var args = fullCommand.Split(' ');
        var cmd = args[0].ToLower();
        if(cmd == "place" && args.Length == 3)
        {
            var coords = args[1].Split(',');
            if (!int.TryParse(coords[0], out int x) || !int.TryParse(coords[1], out int y))
                return;

            // convert direction (string) to our direction enum
            DIRECTION d = DIRECTION.DOWN;
            var ds = args[2].ToLower();
            if (ds == "north") d = DIRECTION.UP;
            else if (ds == "left") d = DIRECTION.LEFT;
            else if (ds == "right") d = DIRECTION.RIGHT;

            inputCommand.text = "";

            SetPosition(new Vector2(x, y), d);
            gameStarted = true;

        }
    }

    public void ReportPosition(Text text)
    {
        text.text = tile.name;
    }
}