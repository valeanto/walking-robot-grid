using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WorldGridTest
        //checks if the player is occupying a different tile after moving
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GetTile()
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
