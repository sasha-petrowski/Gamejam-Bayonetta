using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
   protected StateManager _stateManager;
   protected Animator animator;

   public abstract State RunCurrentState();
}
