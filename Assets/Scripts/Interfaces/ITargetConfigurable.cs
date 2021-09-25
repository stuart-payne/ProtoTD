using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetConfigurable
{
    Strategy[] GetPossibleStrategies { get; }
    TargetSelector GetTargetSelector { get; }
}