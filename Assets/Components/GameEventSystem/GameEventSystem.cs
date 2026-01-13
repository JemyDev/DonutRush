using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventSystem
{
    public static Action OnPlayerCollision;
    public static Action<Collider> OnDoorPassed;
    public static Action<IngredientScriptableObject> OnIngredientCollected;
    public static Action<Dictionary<string, OrderLine>> OnOrderCreated;
    public static Action OnOrderCompleted;
    public static Action OnDoorInstantiated;
    public static Action<IngredientScriptableObject> OnIngredientDistributed;
}
