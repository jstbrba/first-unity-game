using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuView : MonoBehaviour, IView
{
    private IContext _context;

    public event Action OnSpeedUpgrade;
    public event Action OnAttackUpgrade;
    public event Action OnDoorUpgrade;
    public event Action OnGenUpgrade;

    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Button _upgradeSpeedButton;
    [SerializeField] private TextMeshProUGUI _upgradeSpeedPriceText;
    [SerializeField] private Button _upgradeAttackButton;
    [SerializeField] private TextMeshProUGUI _upgradeAttackPriceText;
    [SerializeField] private Button _upgradeDoorButton;
    [SerializeField] private TextMeshProUGUI _upgradeDoorPriceText;
    [SerializeField] private Button _upgradeGenButton;
    [SerializeField] private TextMeshProUGUI _upgradeGenPriceText;

    private int _speedUpgradePrice;
    private int _attackUpgradePrice;
    private int _doorUpgradePrice;
    private int _genUpgradePrice;

    private UpgradeMenuModel _model;
    public void Initialise(IContext context)
    {
        _context = context;

        _upgradeSpeedButton.onClick.AddListener(SpeedUpgradeButton_OnClicked);
        _upgradeAttackButton.onClick.AddListener(AttackUpgradeButton_OnClicked);
        _upgradeDoorButton.onClick.AddListener(DoorUpgradeButton_OnClicked);
        _upgradeGenButton.onClick.AddListener(GenUpgradeButton_OnClicked);

        _model = _context.ModelLocator.Get<UpgradeMenuModel>();
        _model.IsMenuOpen.onValueChanged += ToggleMenu;
        _model.SpeedUpgradePrice.onValueChanged += Model_SpeedPrice_OnValueChanged;
        _model.AttackUpgradePrice.onValueChanged += Model_AttackPrice_OnValueChanged;
        _model.DoorUpgradePrice.onValueChanged += Model_DoorPrice_OnValueChanged;
        _model.GenUpgradePrice.onValueChanged += Model_GenPrice_OnValueChanged;

        InitialiseUI();
        ToggleMenu(false, _model.IsMenuOpen.Value);
    }
    public void InitialiseUI()
    {
        _upgradeSpeedPriceText.text = "$" + _model.SpeedUpgradePrice.Value;
        _upgradeAttackPriceText.text = "$" + _model.AttackUpgradePrice.Value;
        _upgradeDoorPriceText.text = "$" + _model.DoorUpgradePrice.Value;
        _upgradeGenPriceText.text = "$" + _model.GenUpgradePrice.Value;
    }
    public void ToggleMenu(bool previous, bool current) => _menuPanel.SetActive(current);

    public void Model_SpeedPrice_OnValueChanged(int previous, int current)
    {
        _speedUpgradePrice = current;
        _upgradeSpeedPriceText.text = "$" + _speedUpgradePrice;
    }

    public void Model_AttackPrice_OnValueChanged(int previous, int current)
    {
        _attackUpgradePrice = current;
        _upgradeAttackPriceText.text = "$" + _attackUpgradePrice;
    }

    public void Model_DoorPrice_OnValueChanged(int previous, int current)
    {
        _doorUpgradePrice = current;
        _upgradeDoorPriceText.text = "$" + _doorUpgradePrice;
    }

    public void Model_GenPrice_OnValueChanged(int previous, int current)
    {
        _genUpgradePrice = current;
        _upgradeGenPriceText.text = "$" + _genUpgradePrice;
    }
    public void SpeedUpgradeButton_OnClicked() => OnSpeedUpgrade?.Invoke();
    public void AttackUpgradeButton_OnClicked() => OnAttackUpgrade?.Invoke();
    public void DoorUpgradeButton_OnClicked() => OnDoorUpgrade?.Invoke();
    public void GenUpgradeButton_OnClicked() => OnGenUpgrade?.Invoke();
}