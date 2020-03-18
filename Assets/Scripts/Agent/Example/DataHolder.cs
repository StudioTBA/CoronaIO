using System;
using UnityEngine;


namespace Com.StudioTBD.CoronaIO.Agent.Example
{
    /// <summary>
    /// This is an example class to show how to share data between the states.
    /// It can contain any kind of data.
    /// In this case since we are implementing basic arrive/flee,
    /// we need to have the position of the target.
    /// </summary>
    public class DataHolder
    {
        public Vector3? Target;
    }
}