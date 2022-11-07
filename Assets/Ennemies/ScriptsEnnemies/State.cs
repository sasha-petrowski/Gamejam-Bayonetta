using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
   protected StateManager _stateManager;
   
   public abstract State RunCurrentState();
}
