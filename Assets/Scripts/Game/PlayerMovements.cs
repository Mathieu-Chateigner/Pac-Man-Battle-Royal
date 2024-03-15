using System.Collections;
using Photon.Pun;
using UnityEngine;

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

    public class PlayerMovements : MonoBehaviourPunCallbacks
    {
        public Rigidbody2D rb;
        public LayerMask whatStopsMovement;
        public float moveDelay = 0.05f;
        public float moveSpeed = 5f;

        private Directions _currentDir = Directions.NoDir;
        private float _lastMoveTime;
        private bool _isMoving;

        private void Update()
        {
            if (!photonView.IsMine) return;
            if (Time.time - _lastMoveTime < moveDelay || _isMoving) return;

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

            if (_currentDir == Directions.NoDir) return;

            var directionVector = DirectionToVector(_currentDir);
            if (!Physics2D.OverlapCircle(rb.position + directionVector, .2f, whatStopsMovement))
            {
                StartCoroutine(MoveToPosition(rb.position + directionVector));
            }
        }

        private IEnumerator MoveToPosition(Vector2 targetPosition)
        {
            _lastMoveTime = Time.time;
            _isMoving = true;

            while (rb.position != targetPosition)
            {
                rb.position = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            _isMoving = false;
        }

        private Vector2 DirectionToVector(Directions dir)
        {
            switch (dir)
            {
                case Directions.Up:
                    rb.rotation = 90;
                    return Vector2.up;
                case Directions.Down:
                    rb.rotation = -90;
                    return Vector2.down;
                case Directions.Left: 
                    rb.rotation = 180;
                    return Vector2.left;
                case Directions.Right: 
                    rb.rotation = 0;
                    return Vector2.right;
                case Directions.NoDir: 
                    return Vector2.zero;
                default:
                    return Vector2.zero;
            }
        }

        public void StopMovement()
        {
            _currentDir = Directions.NoDir;
        }
    }
}