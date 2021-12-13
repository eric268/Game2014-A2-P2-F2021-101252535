//--------------------KirbyAnimationState.cs-------------------------------------
//----------------Author: Eric Galway 101252535----------------------------------
//----------------Date Last Modified: Dec 10 2021--------------------------------
//  The file containts the enum used for tracking and switching animation states.
//  Revision History : 1.0 Added enum class.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KirbyAnimationState
{
    IDLE,
    WALKING,
    JUMP,
    FLYING,
    LANDING,
    SWORDATTACK,
    DEATH,
    VICTORY,
    NUM_ANIMATIONS

}
