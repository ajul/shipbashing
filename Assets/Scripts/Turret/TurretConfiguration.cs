using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret {
    public class Placement {
        private readonly Configuration configuration;
        public readonly int placementOrder;
        public readonly int level;
        public readonly bool facesForwards;

        public bool isSuperfiring {
            get { return false;  }
        }

        public Placement(Configuration configuration, int placementOrder, int level, bool facesForwards) {
            this.configuration = configuration;
            this.placementOrder = placementOrder;
            this.level = level;
            this.facesForwards = facesForwards;
        }
    }

    public class Configuration : IEnumerable<Placement> {
        // Ordered aft to fore.
        public readonly List<Placement> aftTurrets = new List<Placement>();
        public readonly List<Placement> qTurrets = new List<Placement>();
        public readonly List<Placement> pTurrets = new List<Placement>();
        public readonly List<Placement> foreTurrets = new List<Placement>();

        public enum Area {
            Aft,
            Q,
            P,
            Fore
        }

        public List<Placement> turretsInArea(Area area) {
            switch (area) {
                case Area.Aft: return aftTurrets;
                case Area.Q: return qTurrets;
                case Area.P: return pTurrets;
                case Area.Fore: return foreTurrets;
                default: throw new System.ArgumentException("Invalid turret area.");
            }
        }

        public int placementCount {
            get {
                return aftTurrets.Count + qTurrets.Count + pTurrets.Count + foreTurrets.Count;
            }
        }

        public static Configuration TurretConfigurationFromName(string name) {
            switch (name.ToLower()) {
                case "atlanta":
                    return new Configuration() {
                        { Area.Fore, 0 },
                        { Area.Aft, 0 },
                        { Area.Fore, 1 },
                        { Area.Aft, 1 },
                        { Area.Fore, 2 },
                        { Area.Aft, 2 },
                        { Area.Q, 0 },
                        { Area.P, 0 },
                    };
                case "fuso":
                    return new Configuration() {
                        { Area.Fore, 0 },
                        { Area.Aft, 0 },
                        { Area.Fore, 1 },
                        { Area.Aft, 1 },
                        { Area.Q, 0 },
                        { Area.P, 0 },
                    };
                case "gangut":
                    return new Configuration() {
                        { Area.Fore, 0 },
                        { Area.Aft, 0 },
                        { Area.Q, 0, false },
                        { Area.P, 0, false },
                    };
                case "mogami":
                    return new Configuration() {
                        { Area.Fore, 1, true, false},
                        { Area.Aft, 1, true, false},
                        { Area.Fore, 0, true, false},
                        { Area.Aft, 0, true, false},
                        { Area.Fore, 0, true, false},
                        { Area.Aft, 0, true, false},
                    };
                case "myoko":
                    return new Configuration() {
                        { Area.Fore, 0 },
                        { Area.Aft, 0 },
                        { Area.Fore, 1 },
                        { Area.Aft, 1 },
                        { Area.Fore, 0, false },
                        { Area.Aft, 0, false },
                    };
                case "tone":
                    return new Configuration() {
                        { Area.Fore, 0 },
                        { Area.Fore, 1 },
                        { Area.Fore, 0 },
                        { Area.Fore, 0 },
                    };
                case "wyoming":
                    return new Configuration() {
                        { Area.Fore, 0 },
                        { Area.Aft, 0 },
                        { Area.Fore, 1 },
                        { Area.Aft, 1 },
                        { Area.Aft, 0 },
                        { Area.Aft, 1 },
                    };
                default:
                    throw new System.ArgumentException(string.Format("Invalid turret configuration name {0}", name));
            }
        }

        private void Add(Area area, int level, bool facesOutwards = true, bool addToInside = true) {
            List<Placement> areaTurrets = turretsInArea(area);
            int placementOrder = placementCount;
            bool isForward = area == Area.Fore || area == Area.P;
            if (isForward == addToInside) {
                areaTurrets.Insert(0, new Placement(this, placementOrder, level, facesOutwards));
            } else {
                areaTurrets.Add(new Placement(this, placementOrder, level, !facesOutwards));
            }
        }

        public IEnumerator<Placement> GetEnumerator() {
            foreach (Placement placement in aftTurrets) {
                yield return placement;
            }
            foreach (Placement placement in qTurrets) {
                yield return placement;
            }
            foreach (Placement placement in pTurrets) {
                yield return placement;
            }
            foreach (Placement placement in foreTurrets) {
                yield return placement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}