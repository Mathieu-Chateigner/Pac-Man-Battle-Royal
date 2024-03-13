using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    internal enum Directions
    {
        Up,
        Down,
        Left,
        Right,
        NoDir
    }
    
    public class PlayerMovements : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float speed = 0.5f;

        private float _horizontal;
        private float _vertical;
        private Directions _currentDir = Directions.NoDir;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _currentDir = Directions.Up;
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _currentDir = Directions.Down;
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _currentDir = Directions.Left;
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _currentDir = Directions.Right;
            }
            
            Move(_currentDir);
        }

        private void Move(Directions direction = default)
        {
            switch (direction)
            {
                case Directions.Down:
                    rb.velocity = new Vector2(0, -1 * speed);
                    rb.rotation = -90;
                    break;
                case Directions.Up:
                    rb.velocity = new Vector2(0, 1 * speed);
                    rb.rotation = 90;
                    break;
                case Directions.Left:
                    rb.velocity = new Vector2(-1 * speed, 0);
                    rb.rotation = 180;
                    break;
                case Directions.Right:
                    rb.velocity = new Vector2(1 * speed, 0);
                    rb.rotation = 0;
                    break;
                case Directions.NoDir:
                    break;
                default:
                    Debug.Log("Default ?");
                    break;
            }
        }
    }
}
