using Photon.Pun;

namespace Game
{
    public class PlayerInfos : MonoBehaviourPunCallbacks
    {
        private int _life = 3;
        private float _health = 5;
        private float _strength = 1;
        private float _speed = 8;

        #region Getters
        
        public int GetLife()
        {
            return _life;
        }
        
        public float GetHealth()
        {
            return _health;
        }
        
        public float GetStrength()
        {
            return _strength;
        }
        
        public float GetSpeed()
        {
            return _speed;
        }
        
        #endregion

        #region Setters

        public void SetLife(int life)
        {
            _life = life;
        }
        
        public void SetHealth(float health)
        {
            _health = health;
        }
        
        public void SetStrength(float strength)
        {
            _strength = strength;
        }
        
        public void SetLife(float speed)
        {
            _speed = speed;
        }

        #endregion
    }
}