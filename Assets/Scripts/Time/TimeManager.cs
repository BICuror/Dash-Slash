using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
   private float _defaultTimeScale = 1f;

   public float DefaultTimeScale {get {return _defaultTimeScale;} set {_defaultTimeScale = value;}}

   private void Awake() => Time.timeScale = 1f;
}
