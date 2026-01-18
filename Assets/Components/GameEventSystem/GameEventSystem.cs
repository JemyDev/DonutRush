using System;
using UnityEngine;

public static class GameEventSystem
{
    public static Action OnPlayerCollision;
    public static Action<Collider> OnDoorPassed;
    public static Action<IngredientScriptableObject> OnIngredientCollected;
    public static Action<Order> OnOrderCreated;
    public static Action<int> OnOrderCompleted;
    public static Action OnDoorInstantiated;
    public static Action<IngredientScriptableObject> OnIngredientDistributed;
    public static Action OnGameOver;
}
