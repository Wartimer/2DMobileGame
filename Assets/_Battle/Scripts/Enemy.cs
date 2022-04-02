using UnityEngine;

namespace _Battle.Scripts
{
    internal interface IEnemy
    {
        void Update(DataPlayer dataPlayer, DataType dataType);
    }
    
    internal class Enemy : IEnemy
    {
        private const float KMoney = 5;
        private const float KPower = 1.5f;
        private const int MaxHealthPlayer = 20;
        
        private string _name;
        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;
        private int _crimePlayer;
        
        public Enemy(string name) =>
            _name = name;
        
        public void Update(DataPlayer dataPlayer, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Health:
                    _healthPlayer = dataPlayer.Value;
                    break;
                case DataType.Money:
                    _moneyPlayer = dataPlayer.Value;
                    break;
                case DataType.Power:
                    _powerPlayer = dataPlayer.Value;
                    break;
                case DataType.Crime:
                    _crimePlayer = dataPlayer.Value;
                    break;
            }
            
            Debug.Log($"Notified {_name} change to {dataPlayer}");
        }
        
        public int CalcPower()
        {
            var kHealth = CalcKHealth();
            float moneyRatio = _moneyPlayer / KMoney;
            float powerRatio = _powerPlayer / KPower - _healthPlayer/2.0f;
            
            return (int) (moneyRatio + kHealth + powerRatio);
        }

        private int CalcKHealth() =>
            _healthPlayer >= MaxHealthPlayer ? 10 : 5;
    }
}
