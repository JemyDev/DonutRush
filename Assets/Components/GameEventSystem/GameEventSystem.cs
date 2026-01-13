using System;
using System.Collections.Generic;

public static class GameEventSystem
{
    public static Action OnPlayerCollision;
    public static Action<string> OnIngredientCollected;
    public static Action<Dictionary<string, OrderLine>> OnOrderCreated;
    public static Action OnOrderCompleted;
    public static Action OnDoorInstantiated;
    public static Action<IngredientScriptableObject> OnIngredientDistributed;
}
