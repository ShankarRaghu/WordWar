using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public RectTransform _MenuPanel;
    public RectTransform _GamePanel;

    public Button _PlayBtn;
    public Button _BackBtn;

    private eGameState m_CurrentState;


    private void Start()
    {
        _PlayBtn.onClick.AddListener(OnPlay);
        _BackBtn.onClick.AddListener(OnBack);

        ChangeGameState(eGameState.Menu);
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
            OnBack();
#endif
    }

    private void OnDestroy()
    {
        _PlayBtn.onClick.RemoveAllListeners();
        _BackBtn.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Switches game state
    /// </summary>
    /// <param name="a_GameState">State to switch game to</param>
    public void ChangeGameState(eGameState a_GameState)
    {
        switch (a_GameState)
        {
            case eGameState.Game:
                OnPlay();
                break;

            case eGameState.Menu:
                OnMenu();
                break;
        }

        m_CurrentState = a_GameState;
    }

    /// <summary>
    /// Switches the game to Play state
    /// </summary>
    private void OnPlay()
    {
        if (_MenuPanel != null)
            _MenuPanel.gameObject.SetActive(false);

        if (_GamePanel != null)
            _GamePanel.gameObject.SetActive(true);

        m_CurrentState = eGameState.Game;
    }

    /// <summary>
    /// Switches the game to the Menu state
    /// </summary>
    private void OnMenu()
    {
        if (_MenuPanel != null)
            _MenuPanel.gameObject.SetActive(true);

        if (_GamePanel != null)
            _GamePanel.gameObject.SetActive(false);

        m_CurrentState = eGameState.Menu;
    }

    /// <summary>
    /// Back button event handler
    /// </summary>
    public void OnBack()
    {
        switch (m_CurrentState)
        {
            case eGameState.Menu:
#if UNITY_ANDROID
                Application.Quit();
#endif
                break;

            case eGameState.Game:
                ChangeGameState(eGameState.Menu);
                break;
        }
    }
}
