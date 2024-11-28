using UnityEngine;

public class GamePlayGameState : AStateBase
{ 
    public GamePlayGameState() : base(StateNames.Game)
    {        
    }

    public override void Activate()
    {
        Debug.Log("<color=green>GameplayGame State</color> OnActive");      
    }

    public override void Deactivate()
    {
        Debug.Log("<color=red>GameplayGame State</color> DeOnActive");       
    }

    public override void UpdateState()
    {      
    }
   
}
