using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class PlayerMovementTest
        //checks if the player is occupying a different tile after moving
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayerMovementDirection()
        {
            // Use the Assert class to test conditions
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            var player = playerGO.GetComponent<PlayerMovement>();
            player.MoveBy(Vector3.up);
            Assert.Equals(player.Direction, DIRECTION.Up);
            player.MoveBy(Vector3.down);
            Assert.Equals(player.Direction, DIRECTION.Down);
            player.MoveBy(Vector3.left);
            Assert.Equals(player.Direction, DIRECTION.Left);
            player.MoveBy(Vector3.right);
            Assert.Equals(player.Direction, DIRECTION.Right);
        }

        [Test]
        public void PlayerMovementMove()
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            var player = playerGO.GetComponent<PlayerMovement>();
            player.MoveBy(Vector3.up);
            player.MoveBy(Vector3.up);
            Assert.Equals(player.tile.name, "Tile at (0, 1)");
            player.MoveBy(Vector3.right);
            player.MoveBy(Vector3.right);
            Assert.Equals(player.tile.name, "Tile at (1, 1)");
            player.MoveBy(Vector3.down);
            player.MoveBy(Vector3.down);
            Assert.Equals(player.tile.name, "Tile at (1, 0)");
            player.MoveBy(Vector3.left);
            player.MoveBy(Vector3.left);
            Assert.Equals(player.tile.name, "Tile at (0, 0)");
        }

        [Test]
        public void PlayerMovementMoveEdge()
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            var player = playerGO.GetComponent<PlayerMovement>();
            player.MoveBy(Vector3.down);
            Assert.Equals(player.tile.name, "Tile at (0, 0)");
            player.MoveBy(Vector3.left);
            player.MoveBy(Vector3.left);
            Assert.Equals(player.tile.name, "Tile at (0, 0)");
            player.MoveBy(Vector3.up);
            player.MoveBy(Vector3.up);
            player.MoveBy(Vector3.up);
            player.MoveBy(Vector3.up);
            player.MoveBy(Vector3.up);
            player.MoveBy(Vector3.up);
            Assert.Equals(player.tile.name, "Tile at (0, 4)");
            player.MoveBy(Vector3.right);
            player.MoveBy(Vector3.right);
            player.MoveBy(Vector3.right);
            player.MoveBy(Vector3.right);
            player.MoveBy(Vector3.right);
            player.MoveBy(Vector3.right);
            Assert.Equals(player.tile.name, "Tile at (4, 4)");
        }

        [Test]
        public void PlayerMovementCommand()
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            var player = playerGO.GetComponent<PlayerMovement>();
            var tileName = player.tile.name;
            var dir = player.Direction;

            player.ExecuteCommand("");
            Assert.Equals(player.tile.name, tileName);
            Assert.Equals(player.Direction, dir);
            player.ExecuteCommand("sflj wer");
            Assert.Equals(player.tile.name, tileName);
            Assert.Equals(player.Direction, dir);
            player.ExecuteCommand("place x,5 north");
            Assert.Equals(player.tile.name, tileName);
            Assert.Equals(player.Direction, dir);

            player.ExecuteCommand("place 6,3 north");
            Assert.Equals(player.tile.name, tileName);
            Assert.Equals(player.Direction, dir);

            player.ExecuteCommand("place 2,3 north");
            Assert.Equals(player.tile.name, "Tile at (2,3)");
            Assert.Equals(player.Direction, DIRECTION.Up);
        }

        [Test]
        public void PlayerMovementReportPosition()
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            var player = playerGO.GetComponent<PlayerMovement>();
            var text = GameObject.FindObjectsOfType<Text>().Where(o => o.name == "TextPlayerTile").FirstOrDefault();
            player.ReportPosition(text);
            Assert.Equals(text.text, player.tile.name);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayerMovementTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
