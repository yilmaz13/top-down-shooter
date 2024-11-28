using System;

public static class GameEvents
{
    public static event Action OnClickLevelRestart;
    public static void ClickLevelRestart() { OnClickLevelRestart?.Invoke(); }

    public static event Action OnClickLevelNext;
    public static void ClickLevelNext() { OnClickLevelNext?.Invoke(); }

    public static event Action OnClickGotoMenu;
    public static void ClickGotoMenu() { OnClickGotoMenu?.Invoke(); }

    public static event Action OnClickGotoGameScene;
    public static void ClickGotoGameScene() { OnClickGotoGameScene?.Invoke(); }

    public static event Action OnStartGame;
    public static event Action<bool> OnEndGame;
    public static void StartGame() { OnStartGame?.Invoke(); }
    public static void EndGame(bool success) { OnEndGame?.Invoke(success); }        

}

