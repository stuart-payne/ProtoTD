using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeter
{
    public void RegisterTarget(Enemy enemy);
    public void DeregisterTarget(Enemy enemy);
}
