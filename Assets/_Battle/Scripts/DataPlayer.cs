using System;
using System.Collections.Generic;


namespace _Battle.Scripts
{
    internal class DataPlayer
    {
        private int _value;
        public DataType Type { get; }
        
        private readonly List<IEnemy> _enemies;

        public int Value
        {
            get => _value;
            set => SetValue(value);
        }
        
        private void Notify()
        {
            foreach (var enemy in _enemies)
                enemy.Update(this, Type);
        }

        public DataPlayer(DataType type)
        {
            Type = type;
            _enemies = new List<IEnemy>();
        }

        public void Attach(IEnemy enemy) => _enemies.Add(enemy);

        public void Detach(IEnemy enemy) => _enemies.Remove(enemy);

        private void SetValue(int newValue)
        {
            if (_value == newValue) return;
            _value = newValue;
            Notify();
        }
    }
}