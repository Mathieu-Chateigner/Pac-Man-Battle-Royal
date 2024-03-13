using System;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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
        public float speed = 0.5f;
        public Transform movePoint;
        public LayerMask whatStopsMovement;
        
        private Directions _currentDir = Directions.NoDir;

        private void Start()
        {
            movePoint.parent = null;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

            if (!(Vector3.Distance(transform.position, movePoint.position) <= 0.005f)) return;
            if (Math.Abs(Mathf.Abs(Input.GetAxisRaw("Horizontal")) - 1f) < 0.001)
            {
                if (!Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(0.01f, 0f, 0f), .2f,
                        whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal")/100, 0f, 0f);
                }
            } else if (Math.Abs(Mathf.Abs(Input.GetAxisRaw("Vertical")) - 1f) < 0.001)
            {
                if (!Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(0f, 0.01f, 0f), .002f,
                        whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical")/100, 0f);
                }
            }

            /*
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
            */
        }
        
        /*
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
        */
    }
}
