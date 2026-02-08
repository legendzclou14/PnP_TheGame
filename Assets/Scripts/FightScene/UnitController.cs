using System;
using UnityEngine;

public enum MenuControlActions
{
    Up,
    Down,
    Confirm,
    Return
}

public class UnitController : MonoBehaviour
{
    public enum UnitMenuState
    {
        ActionMenu,
        AttackMenu
    }

    [SerializeField] private GameObject _actions;
    [SerializeField] private GameObject _attackList;
    [SerializeField] private GameObject _attackDescription;

    private int _actionIndex = 0;
    private int _attackIndex = 0;

    private UnitMenuState _state;

    private Action<int> OnAttackSelected;

    public void Initialize()
    {
        ResetMenuState();
    }

    public void Cleanup()
    {

    }

    public void HandleMenuInput(MenuControlActions action)
    {
        switch (_state)
        {
            case UnitMenuState.ActionMenu:
                HandleActionMenuInput(action);
                break;
            case UnitMenuState.AttackMenu:
                HandleAttackMenuInput(action);
                break;
        }
    }

    private void HandleActionMenuInput(MenuControlActions action)
    {
        switch (action)
        {
            case MenuControlActions.Up:
                _actionIndex = ++_actionIndex % 3;
                RedrawActionMenu();
                break;
            case MenuControlActions.Down:
                _actionIndex = --_actionIndex >= 0 ? _actionIndex : 2;
                RedrawActionMenu();
                break;
            case MenuControlActions.Confirm:
                switch (_actionIndex)
                {
                    case 0: //attack, open attack menu
                        _attackList.SetActive(true);
                        _attackDescription.SetActive(true);
                        RedrawAttackMenu();

                        _state = UnitMenuState.AttackMenu;
                        break;
                    case 1: //defend, dmg/2 for next turn
                        ResetMenuState();
                        break;
                    case 2: //regen, +1 mana
                        ResetMenuState();
                        break;
                    default:
                        Debug.LogError($"actionIndex {_actionIndex} was unexpected!");
                        break;
                }
                break;
        }
    }

    private void HandleAttackMenuInput(MenuControlActions action)
    {
        switch (action)
        {
            case MenuControlActions.Up:
                _attackIndex = ++_attackIndex % 3;
                RedrawAttackMenu();
                break;
            case MenuControlActions.Down:
                _attackIndex = --_attackIndex >= 0 ? _attackIndex : 2;
                RedrawAttackMenu();
                break;
            case MenuControlActions.Confirm:
                OnAttackSelected?.Invoke(_attackIndex);
                ResetMenuState();
                break;
            case MenuControlActions.Return:
                _state = UnitMenuState.ActionMenu;

                _attackList.SetActive(false);
                _attackDescription.SetActive(false);
                break;
        }
    }

    public void TurnStart()
    {
        ResetMenuState();

        _actions.SetActive(true);
        _attackList.SetActive(false);
        _attackDescription.SetActive(false);
    }

    public void TurnEnd()
    {

    }

    private void RedrawActionMenu()
    {

    }

    private void RedrawAttackMenu()
    {

    }

    public void ResetMenuState()
    {
        _actions.SetActive(false);
        _attackList.SetActive(false);
        _attackDescription.SetActive(false);
        _attackIndex = 0;
        _actionIndex = 0;

        _state = UnitMenuState.ActionMenu;
    }
}
