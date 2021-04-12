using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        NO_GAMEMODE_CHOOSEN,
        P_V_P,
        P_V_AI,
        AI_V_AI
    }

    public enum Display
    {
        DISPLAY_2D,
        DISPLAY_3D,
    }

    public enum GameStatus
    {
        NOT_FINISHED,
        PLAYER_RED_WIN,
        PLAYER_YELLOW_WIN,
        DRAW,
    }

    public enum GameDifficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    public delegate void DelegateDisplayHasChanged();
    public delegate void DelegateResetAsked();
    public delegate void DelegateWin();
    public DelegateDisplayHasChanged OnDisplayHasChanged;
    public DelegateResetAsked OnResetAsked;
    public DelegateWin OnWin;


    [SerializeField] GameMode gameModeChoosen = GameMode.P_V_P;
    [SerializeField] Display currentDisplay = Display.DISPLAY_2D;
    [SerializeField] GameStatus currentGameStatus = GameStatus.NOT_FINISHED;
    [SerializeField] GameDifficulty currentDifficulty = GameDifficulty.EASY;

    static GameManager instance = null;

    bool isGameIsFinished = false;

    public static GameManager Instance { get => instance; }
    public GameMode GameModeChoosen { get => gameModeChoosen; }
    public Display CurrentDisplay { get => currentDisplay; }
    public bool IsGameIsFinished { get => isGameIsFinished; set => isGameIsFinished = value; }
    public GameStatus CurrentGameStatus { get => currentGameStatus; set => currentGameStatus = value; }
    public GameDifficulty CurrentDifficulty { get => currentDifficulty; set => currentDifficulty = value; }

    // Start is called before the first frame update
    void Awake()
    {
        // If we find an other GameManager in the current scene, we destroy it
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // If it's the first time where we init this class, we give the address of the class to the Instance of the class
        instance = this;
        DontDestroyOnLoad(gameObject);

        OnResetAsked += ResetGameManagerData;
    }

    public void ChooseGameMode(string _gameModeChoosen)
    {
        switch (_gameModeChoosen)
        {
            case "PvP":
                gameModeChoosen = GameMode.P_V_P;
                break;
            case "PvAI":
                gameModeChoosen = GameMode.P_V_AI;
                break;
            case "AIvAI":
                gameModeChoosen = GameMode.AI_V_AI;
                break;
            default:
                gameModeChoosen = GameMode.NO_GAMEMODE_CHOOSEN;
                break;
        }
    }
    
    public void ChangeDisplayMode()
    {
        currentDisplay = (currentDisplay == Display.DISPLAY_3D) ? Display.DISPLAY_2D : Display.DISPLAY_3D;
        OnDisplayHasChanged?.Invoke();

        Debug.Log("New display " + currentDisplay);
    }

    public void ResetGameManagerData()
    {
        CurrentGameStatus = GameStatus.NOT_FINISHED;
        isGameIsFinished = false;
        currentDisplay = Display.DISPLAY_2D;
        OnDisplayHasChanged?.Invoke();
    }

    public void GameHasFinished(GameManager.GameStatus _status)
    {
        isGameIsFinished = true;
        currentGameStatus = _status;

        OnWin?.Invoke();
    }
    public void GameNeedToBeRestart()
    {
        OnResetAsked?.Invoke();
    }
}
