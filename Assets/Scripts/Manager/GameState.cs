namespace Manager
{
    public enum GameState
    {
        MainMenu,
        ResourceLoading,
    }
    
    public interface IGameState
    {
        void EnterState();
        void Update();
        void ExitState();
        GameState GetCurrentGameState();
    }
}