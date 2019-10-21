using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    // Move amount is in increments of 2 (1 square size in unity units)
    void Move(int moveAmount = 1);
}
