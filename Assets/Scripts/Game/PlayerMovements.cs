using UnityEngine;
using Vector2 = UnityEngine.Vector2;

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
        public LayerMask whatStopsMovement;
        public float moveDelay = 0.05f; // Time in seconds between moves

        private Directions _currentDir = Directions.NoDir;
        private float _lastMoveTime; // When the last move happened

        private void Update()
        {
            if (Time.time - _lastMoveTime < moveDelay) return;

            if (Input.GetKey(KeyCode.UpArrow))
                if (!Physics2D.OverlapCircle(rb.position + Vector2.up, .2f, whatStopsMovement))
                    _currentDir = Directions.Up;

            if (Input.GetKey(KeyCode.DownArrow))
                if (!Physics2D.OverlapCircle(rb.position + Vector2.down, .2f, whatStopsMovement))
                    _currentDir = Directions.Down;

            if (Input.GetKey(KeyCode.LeftArrow))
                if (!Physics2D.OverlapCircle(rb.position + Vector2.left, .2f, whatStopsMovement))
                    _currentDir = Directions.Left;

            if (Input.GetKey(KeyCode.RightArrow))
                if (!Physics2D.OverlapCircle(rb.position + Vector2.right, .2f, whatStopsMovement))
                    _currentDir = Directions.Right;

            Move(_currentDir);
        }

        private void Move(Directions direction = default)
        {
            var position = rb.position;

            switch (direction)
            {
                case Directions.Down:
                    if (!Physics2D.OverlapCircle(rb.position + Vector2.down, .2f, whatStopsMovement))
                    {
                        rb.rotation = -90;
                        rb.position = new Vector2(position.x, position.y - 1);
                    }

                    break;
                case Directions.Up:
                    if (!Physics2D.OverlapCircle(rb.position + Vector2.up, .2f, whatStopsMovement))
                    {
                        rb.rotation = 90;
                        rb.position = new Vector2(position.x, position.y + 1);
                    }

                    break;
                case Directions.Left:
                    if (!Physics2D.OverlapCircle(rb.position + Vector2.left, .2f, whatStopsMovement))
                    {
                        rb.rotation = 180;
                        rb.position = new Vector2(position.x - 1, position.y);
                    }

                    break;
                case Directions.Right:
                    if (!Physics2D.OverlapCircle(rb.position + Vector2.right, .2f, whatStopsMovement))
                    {
                        rb.rotation = 0;
                        rb.position = new Vector2(position.x + 1, position.y);
                    }

                    break;
                case Directions.NoDir:
                    return;
                default:
                    return;
            }

            _lastMoveTime = Time.time; // Update the time of the last move
        }
    }
}