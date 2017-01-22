using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class TrackableLifts : FortressCraftMod
{
	#region customVars
	private string XMLConfigFile = "TrackableLiftsConfig.xml";
	private string XMLConfigPath = "";
	private string XMLModID = "Joseph983.TrackableLifts";
	private int XMLModVersion = 1;
	private bool XMLConfigExists;
	public TrackableLiftsConfig mConfig;
  
    //BlockID's
    public ushort MK1TrackableLiftShaftBuilderType = ModManager.mModMappings.CubesByKey["Joseph983.MK1TrackableLiftShaftBuilder"].CubeType;
    public ushort MK2TrackableLiftShaftBuilderType = ModManager.mModMappings.CubesByKey["Joseph983.MK2TrackableLiftShaftBuilder"].CubeType;
    public ushort PlacementType = ModManager.mModMappings.CubesByKey["MachinePlacement"].CubeType;
    public ushort MK1TrackableLiftShaftBuilderPlacementValue = ModManager.mModMappings.CubesByKey["Joseph983.MK1TrackableLiftShaftBuilder"].ValuesByKey["Joseph983.MK1TrackableLiftShaftBuilderPlacement"].Value;
    public ushort MK2TrackableLiftShaftBuilderPlacementValue = ModManager.mModMappings.CubesByKey["Joseph983.MK2TrackableLiftShaftBuilder"].ValuesByKey["Joseph983.MK2TrackableLiftShaftBuilderPlacement"].Value;
    #endregion

    public override ModRegistrationData Register()
	{
		#region XML Config Loading/Parsing
		XMLConfigPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		string configfile = Path.Combine(XMLConfigPath, XMLConfigFile);
		Debug.Log("Checking for XML Config file at:" + configfile);

		if (File.Exists(configfile))
		{
			XMLConfigExists = true;
			Debug.Log("TrackableLifts: Config File exists, loading...");
			string xmltext = File.ReadAllText(configfile);
			try
			{
				mConfig = (TrackableLiftsConfig)XMLParser.DeserializeObject(xmltext, typeof(TrackableLiftsConfig));
			}
			catch (Exception e)
			{
				Debug.Log("TrackableLifts: Error parsing XML. Exception" + e.ToString());
				XMLConfigExists = false;
			}
			if (XMLConfigExists) Debug.Log("TrackableLifts Config Loaded");
		}
		else
		{
			XMLConfigExists = false;
			Debug.Log("TrackableLifts: Config File missing!");
		}
        #endregion

        #region ModRegistrationData
        ModRegistrationData modData = new ModRegistrationData();

        modData.RegisterEntityHandler("Joseph983.MK1TrackableLiftsShaftBuilderPlacement");
        modData.RegisterEntityHandler("Joseph983.MK1TrackableLiftsShaftBuilderBlock");
        modData.RegisterEntityHandler("Joseph983.MK1TrackableLiftsShaftBuilderCenter");
        modData.RegisterEntityHandler("Joseph983.MK1TrackableLiftsShaftBuilderPlacement");

        modData.RegisterEntityHandler("Joseph983.MK2TrackableLiftsShaftBuilderPlacement");
        modData.RegisterEntityHandler("Joseph983.MK2TrackableLiftsShaftBuilderBlock");
        modData.RegisterEntityHandler("Joseph983.MK2TrackableLiftsShaftBuilderCenter");
        modData.RegisterEntityHandler("Joseph983.MK2TrackableLiftsShaftBuilderPlacement");

        modData.RegisterEntityHandler("Joseph983.GuideRailFornmer");
        #endregion
        Debug.Log("TrackableLifts: Registered");
        return modData;
	}
    public override void CheckForCompletedMachine(ModCheckForCompletedMachineParameters parameters)
    {
        if (parameters.CubeValue == MK1TrackableLiftShaftBuilderPlacementValue)
        {
            MK1TrackableLiftShaftBuilder.CheckForCompletedMachine(parameters.Frustrum, parameters.X, parameters.Y, parameters.Z);
        }
        if (parameters.CubeValue == MK2TrackableLiftShaftBuilderPlacementValue)
        {
            MK2TrackableLiftShaftBuilder.CheckForCompletedMachine(parameters.Frustrum, parameters.X, parameters.Y, parameters.Z);
        }
        Debug.Log("TrackableLifts: CheckForCompletedMachine Cube values did not match, CubeValue" + parameters.CubeValue);
        base.CheckForCompletedMachine(parameters);
    }
    public override ModCreateSegmentEntityResults CreateSegmentEntity(ModCreateSegmentEntityParameters parameters)
    {
        ModCreateSegmentEntityResults result = new ModCreateSegmentEntityResults();

        if (parameters.Cube == MK1TrackableLiftShaftBuilderType || (parameters.Cube == PlacementType && parameters.Value == MK1TrackableLiftShaftBuilderPlacementValue))
        {
            parameters.ObjectType = SpawnableObjectEnum.AutoExcavator;
            result.Entity = new MK1TrackableLiftShaftBuilder(parameters, mConfig, XMLModVersion);
        }

        if (parameters.Cube == MK2TrackableLiftShaftBuilderType || (parameters.Cube == PlacementType && parameters.Value == MK2TrackableLiftShaftBuilderPlacementValue))
        {
            parameters.ObjectType = SpawnableObjectEnum.AutoExcavator;
            result.Entity = new MK2TrackableLiftShaftBuilder(parameters, mConfig, XMLModVersion);
        }
        return result;
        return base.CreateSegmentEntity(parameters);
    }
}

