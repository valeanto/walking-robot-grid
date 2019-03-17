using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DIRECTION { Up, Down, Left, Right }
public class PlayerMovement : MonoBehaviour
{
    private string[] Animations = { "PlayerUp", "PlayerDown", "PlayerLeft", "PlayerRight" };
    public bool gameStarted = false;

    private DIRECTION dir = DIRECTION.Down;
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
        ani = gameObject.GetComponent<Animator>();
        ani.Play("PlayerDown");
        tile = grid.GetTile(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            move();
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

        transform.position += deltaMove;

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
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (dir != DIRECTION.Up)
                {
                    ani.Play("PlayerUp");
                    dir = DIRECTION.Up;
                }
                else
                {
                    MoveBy(Vector3.up);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (dir != DIRECTION.Down)
                {
                    ani.Play("PlayerDown");
                    dir = DIRECTION.Down;
                }
                else
                {
                    MoveBy(Vector3.down);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (dir != DIRECTION.Left)
                {
                    ani.Play("PlayerLeft");
                    dir = DIRECTION.Left;
                }
                else
                {
                    MoveBy(Vector3.left);
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (dir != DIRECTION.Right)
                {
                    ani.Play("PlayerRight");
                    dir = DIRECTION.Right;
                }
                else
                {
                    MoveBy(Vector3.right);
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

            DIRECTION d = DIRECTION.Down;
            var ds = args[2].ToLower();
            if (ds == "north") d = DIRECTION.Up;
            else if (ds == "left") d = DIRECTION.Left;
            else if (ds == "right") d = DIRECTION.Right;

            inputCommand.text = "";

            SetPosition(new Vector2(x, y), d);
            gameStarted = true;
        }
    }

    public void ReportPosition(Text text)
    {
        text.text = tile.name + ", facing " + dir;
    }
}