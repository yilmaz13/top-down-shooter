using UnityEngine;

public class AppContext : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SceneReferences _sceneReferences;
    [SerializeField] private ResourceReferences _resourceReferences;

    //      Private
    private StateManager _stateManager;
    private UserDataManager _userDataManager;

    void Start()
    {
       // _popupManager.Initialize();

        _stateManager = new StateManager();
        _userDataManager = new UserDataManager();
       
        _stateManager.AddStates(new LoadingState(_stateManager, _sceneReferences, _resourceReferences));
        _stateManager.AddStates(new GamePlayGameState(_stateManager, _userDataManager, _sceneReferences,  _resourceReferences));
        _stateManager.AddStates(new MenuGameState(_stateManager, _userDataManager,_sceneReferences, _resourceReferences));

        _stateManager.ChangeState(StateNames.Loading);

    }

    void Update()
    {
        _stateManager.GetCurrentState().UpdateState();
    }
}
