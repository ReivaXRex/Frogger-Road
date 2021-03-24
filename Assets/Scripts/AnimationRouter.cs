using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRouter : MonoBehaviour
{
    [SerializeField] Player player;

    public void FinishHopAnimEvent()
    {
        player.FinishHop();
    }
}
