using UnityEngine;
using System;
using Components.Data;

namespace Services.GameEventService
{
    /// <summary>
    /// Game event service to handle all game events
    /// </summary>
    public static class GameEventService
    {
        /// <summary>
        /// Performed when player performs move input
        /// </summary>
        public static Action<int> OnMoveInputPerformed;
        /// <summary>
        /// Performed when player performs jump input
        /// </summary>
        public static Action OnJumpInputPerformed;
    
        /// <summary>
        /// Triggered when player starts or stops moving
        /// </summary>
        public static Action<bool> OnPlayerMoving;
        /// <summary>
        /// Triggered when lane changes regarding player move input
        /// </summary>
        public static Action<Transform> OnUpdateLaneTarget;
    
        /// <summary>
        /// Triggered when player collides with a collider
        /// </summary>
        public static Action OnPlayerCollision;
        /// <summary>
        /// Triggered when player passes through a door trigger
        /// </summary>
        public static Action<Collider> OnPlayerTriggerDoorPassed;
    
        // Game Events
        public static Action<IngredientData> OnIngredientCollected;
        public static Action<IngredientData> OnIngredientSpawned;
        public static Action OnDoorInstantiated;
        public static Action<Order> OnOrderCreated;
        public static Action<int> OnOrderCompleted;
        
        // State Events
        public static Action<bool> OnCountdownState;
        public static Action<float> OnCountdownTick;
        public static Action<int> OnPlayerLifeUpdated;
        public static Action<bool> OnGameState;
        public static Action<bool> OnGameOverState;

        // Level Events
        public static Action<LevelParametersData> OnLevelChanged;
    }
}
