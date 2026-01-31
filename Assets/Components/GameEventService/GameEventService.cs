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
        #region Input Events

        /// <summary>
        /// Player move input received (direction: -1 left, 1 right)
        /// <param name="direction">-1 = left, 1 = right</param>
        /// </summary>
        public static Action<int> OnMoveInputPerformed;

        /// <summary>
        /// Player jump input received
        /// </summary>
        public static Action OnJumpInputPerformed;

        #endregion

        #region Player Movement Events

        /// <summary>
        /// Player moving
        /// <param name="isMoving">true = start moving, false = stops moving</param>
        /// </summary>
        public static Action<bool> OnPlayerMoving;

        /// <summary>
        /// Lane target updated after player input
        /// <param name="targetLane">Next lane target</param>
        /// </summary>
        public static Action<Transform> OnUpdateLaneTarget;

        #endregion

        #region Player Collision Events

        /// <summary>
        /// Player collided with an obstacle
        /// </summary>
        public static Action OnPlayerCollision;

        /// <summary>
        /// Player passed through a door trigger
        /// <param name="doorTrigger">Door trigger</param>
        /// </summary>
        public static Action<Collider> OnPlayerTriggerDoorPassed;

        #endregion

        #region Ingredient Events

        /// <summary>
        /// Ingredient collected by the player
        /// <param name="ingredient">Ingredient collected</param>
        /// </summary>
        public static Action<IngredientData> OnIngredientCollected;

        /// <summary>
        /// Ingredient spawned on a door
        /// <param name="ingredient">Ingredient attached to the door</param>
        /// </summary>
        public static Action<IngredientData> OnIngredientSpawned;

        /// <summary>
        /// Door instantiated on a chunk
        /// </summary>
        public static Action OnDoorInstantiated;

        #endregion

        #region Order Events

        /// <summary>
        /// New order created
        /// <param name="newOrder">New order created</param>
        /// </summary>
        public static Action<Order> OnOrderCreated;

        /// <summary>
        /// Order completed
        /// <param name="scoreToAdd">Score to add when an order is completed</param>
        /// </summary>
        public static Action<int> OnOrderCompleted;

        /// <summary>
        /// Order timer expired
        /// </summary>
        public static Action OnOrderFailed;

        #endregion

        #region Level Events

        /// <summary>
        /// Level changed after completing required orders
        /// <param name="levelParametersInfo">Get updated parameters (speed, numbers of ingredients required...)</param>
        /// </summary>
        public static Action<LevelParametersInfo> OnLevelChanged;

        /// <summary>
        /// Initial level loaded
        /// <param name="initialLevelParameters">Get initial level parameters</param>
        /// </summary>
        public static Action<LevelParametersInfo> OnLevelStarted;

        #endregion

        #region Game State Events

        /// <summary>
        /// Countdown state
        /// <param name="isOnCountdownState">Check to see if CountdownState has started or is over</param>
        /// </summary>
        public static Action<bool> OnCountdownState;

        /// <summary>
        /// Countdown tick with remaining time
        /// <param name="remainingTime">Remaining time before end</param>
        /// </summary>
        public static Action<float> OnCountdownTick;

        /// <summary>
        /// Player life updated
        /// <param name="newLife">New life state</param>
        /// </summary>
        public static Action<int> OnPlayerLifeUpdated;

        /// <summary>
        /// Game state
        /// <param name="isOnGameState">Check to see if GameState has started or is over</param>
        /// </summary>
        public static Action<bool> OnGameState;

        /// <summary>
        /// Game over state
        /// <param name="isOnGameOverState">Check to see if GameOverState has started or is over</param>
        /// </summary>
        public static Action<bool> OnGameOverState;

        /// <summary>
        /// Score updated
        /// <param name="newScore">New score to update</param>
        /// </summary>
        public static Action<int> OnScoreUpdated;

        /// <summary>
        /// Game timer tick with remaining time
        /// <param name="remainingTime">Remaining time before end</param>
        /// </summary>
        public static Action<float> OnTimerTick;

        #endregion
    }
}
