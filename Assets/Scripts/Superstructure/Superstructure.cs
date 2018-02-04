﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISuperstructureItem {
    float slotSize {
        get;
    }
}

public class Superstructure : MonoBehaviour {
    public Ship ship {
        get {
            return GetComponentInParent<Ship>();
        }
    }

    public enum Area {
        Aft,
        Mid,
        Fore
    }

    public readonly List<ISuperstructureItem> aftSection = new List<ISuperstructureItem>();
    public readonly List<ISuperstructureItem> midSection = new List<ISuperstructureItem>();
    public readonly List<ISuperstructureItem> foreSection = new List<ISuperstructureItem>();
}
