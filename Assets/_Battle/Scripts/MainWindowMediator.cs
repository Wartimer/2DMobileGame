using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace _Battle.Scripts
{
    //Mediator pattern example
    public class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")] 
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countCrimeText;
        
        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Coin Buttons")] 
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _reduceMoneyButton;
        
        [Header("Coin Buttons")] 
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _reduceHealthButton;
        
        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _reducePowerButton;
        
        [Header("Crime Buttons")]
        [SerializeField] private Button _addCrimeButton;
        [SerializeField] private Button _reduceCrimeButton;
        
        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _leaveButton;
        

        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        private int _allCountCrimePlayer;

        private DataPlayer _money;
        private DataPlayer _health;
        private DataPlayer _power;
        private DataPlayer _crime;
        
        
        private Enemy _enemy;

        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreateDataPlayer(DataType.Money, _enemy);
            _health = CreateDataPlayer(DataType.Health, _enemy);
            _power = CreateDataPlayer(DataType.Power, _enemy);
            _crime = CreateDataPlayer(DataType.Crime, _enemy);
            CheckCrimeLevel();
            Subscribe();
        }

        private DataPlayer CreateDataPlayer(DataType dataType, IEnemy enemy)
        {
            var dataPlayer = new DataPlayer(dataType);
            dataPlayer.Attach(enemy);
            return dataPlayer;
        }

        private void RemoveDataPlayer(ref DataPlayer dataPlayer)
        {
            dataPlayer.Detach(_enemy);
            dataPlayer = null;
        }

        private void IncreasePower() => AddPower(1);
        private void ReducePower() => AddPower(-1);
        private void AddPower(int amount) => AddValue(ref _allCountPowerPlayer, amount, DataType.Power);

        private void IncreaseHealth() => AddHealth(1);
        private void ReduceHealth() => AddHealth(-1);
        private void AddHealth(int amount) => AddValue(ref _allCountHealthPlayer, amount, DataType.Health);

        private void IncreaseMoney() => AddMoney(1);
        private void ReduceMoney() => AddMoney(-1);
        private void AddMoney(int amount) => AddValue(ref _allCountMoneyPlayer, amount, DataType.Money);

        private void IncreaseCrime() => AddCrime(1);
        private void ReduceCrime() => AddCrime(-1);
        private void AddCrime(int amount) => AddValue(ref _allCountCrimePlayer, amount, DataType.Crime);
        
        private void AddValue(ref int origValue, int amount, DataType dataType)
        {
            origValue += amount;
            ChangeDataWindow(origValue, dataType);
            CheckCrimeLevel();
        }

        private void CheckCrimeLevel()
        {
            bool isActive;
            isActive = _crime.Value < 3; 
            _leaveButton.gameObject.SetActive(isActive);
        }
        
        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            TMP_Text tmpText = GetDataText(dataType);
            tmpText.text = $"Player {dataType:F} {countChangeData}";
            DataPlayer dataPlayer = GetDataPlayer(dataType);
            dataPlayer.Value = countChangeData;

            _countPowerEnemyText.text = $"Enemy Power {_enemy.CalcPower()}";
        }

        private TMP_Text GetDataText(DataType dataType) =>
            dataType switch
            {
                DataType.Health => _countHealthText,
                DataType.Money => _countMoneyText,
                DataType.Power => _countPowerText,
                DataType.Crime => _countCrimeText,
                _ => throw new ArgumentOutOfRangeException()
            };


        private DataPlayer GetDataPlayer(DataType dataType) =>
            dataType switch
            {
                DataType.Health => _health,
                DataType.Money => _money,
                DataType.Power => _power,
                DataType.Crime => _crime,
                _ => throw new ArgumentOutOfRangeException()

            };
        
        
        private void Fight()
        {
            bool isWin = _allCountPowerPlayer >= _enemy.CalcPower();
            string color = isWin ? "07FF00" : "FF0000";
            string message = isWin ? "Win" : "Lose";

            Debug.Log($"<color=#{color}>{message}!!!</color>");
        }

        private void Leave()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _reduceMoneyButton.onClick.AddListener(ReduceMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _reduceHealthButton.onClick.AddListener(ReduceHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _reducePowerButton.onClick.AddListener(ReducePower);
            
            _addCrimeButton.onClick.AddListener(IncreaseCrime);
            _reduceCrimeButton.onClick.AddListener(ReduceCrime);

            _fightButton.onClick.AddListener(Fight);
            _leaveButton.onClick.AddListener(Leave);
        }
        
        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveAllListeners();
            _reduceMoneyButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _reduceHealthButton.onClick.RemoveAllListeners();

            _addPowerButton.onClick.RemoveAllListeners();
            _reducePowerButton.onClick.RemoveAllListeners();

            _addCrimeButton.onClick.RemoveAllListeners();
            _reduceCrimeButton.onClick.RemoveAllListeners();
            
            _fightButton.onClick.RemoveAllListeners();
            _leaveButton.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            RemoveDataPlayer(ref _money);
            RemoveDataPlayer(ref _health);
            RemoveDataPlayer(ref _power);
            RemoveDataPlayer(ref _crime);
            Unsubscribe();
        }

    }
}
