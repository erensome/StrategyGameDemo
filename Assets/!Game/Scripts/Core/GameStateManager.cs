using System;
using System.Collections.Generic;
using EventBus;
using InputState;
using UI;
using UnityEngine;

public class GameStateManager : MonoSingleton<GameStateManager>
{
    public enum GameState
    {
        Idle,
        Building,
        Selecting
    }

    private GameState currentState;
    private IInputStateHandler currentInputStateHandler;
    private readonly Dictionary<GameState, IInputStateHandler> inputStateHandlers = new()
    {
        { GameState.Idle, new IdleStateHandler() },
        { GameState.Building, new BuildingStateHandler() },
        { GameState.Selecting, new SelectingStateHandler() }
    };

    public GameState CurrentState => currentState;
    public IInputStateHandler CurrentInputStateHandler => currentInputStateHandler;
    
    private void Awake()
    {
        UIEventBus.OnProductionMenuItemSelected += OnProductionMenuItemSelected;
        GameEventBus.OnEntitySelected += OnEntitySelected;
        GameEventBus.OnBuildingPlaced += OnBuildingPlaced;
    }

    private void Start()
    {
        SetState(GameState.Idle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log($"Current State: {currentState}");
        }
    }

    protected override void OnDestroy()
    {
        UIEventBus.OnProductionMenuItemSelected -= OnProductionMenuItemSelected;
        GameEventBus.OnEntitySelected -= OnEntitySelected;
        base.OnDestroy();
    }

    private void OnProductionMenuItemSelected(ProductionMenuItem obj)
    {
        SetState(GameState.Building);
    }
    
    private void OnEntitySelected(EntityData obj)
    {
        SetState(obj == null ? GameState.Idle : GameState.Selecting);
    }
    
    private void OnBuildingPlaced(IBuildable obj)
    {
        SetState(GameState.Idle);
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        currentInputStateHandler = inputStateHandlers[newState];
    }
}
