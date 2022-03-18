using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Saltyfish.Data
{
    public static class LayerConfig
    {
        #region LayerConfig

        public static int Obstacle = LayerMask.NameToLayer("ObStacle");

        public static int EveryThingLayer = ~0;

        #endregion
    }

    public static class TagConfig
    {
        public const string PLAYER_TAG = "Player";

        public const string ENEMY_TAG = "Enemy";
    }
}
