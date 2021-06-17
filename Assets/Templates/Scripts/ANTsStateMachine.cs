using UnityEditor;
using UnityEngine;


public interface IANTsStateMachine
{
    void StateEnter();
    void StateUpdate();
    void StateExit();
}
