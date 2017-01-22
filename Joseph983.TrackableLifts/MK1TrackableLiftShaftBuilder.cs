using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MK1TrackableLiftShaftBuilder : MachineEntity, PowerConsumerInterface
{
    //Block type for multi-block machine
    public static ushort PLACEMENT_VALUE = ModManager.mModMappings.CubesByKey["MachinePlacement"].ValuesByKey["Joseph983."];

}