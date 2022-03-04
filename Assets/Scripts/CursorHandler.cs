using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    [SerializeField] private Transform _Player;

    void Update()
    {
        transform.LookAt(_Player.position);
    }
}
