using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCreatePlayerFunctions {
	
	private StatAllocationModule StatAllocationModule = new StatAllocationModule();

	public void DisplayStatAllocation()
	{
		//List of stats with plus and minus buttons
		StatAllocationModule.DisplayStatAllocationModule();
		//make sure the player cannot add more than stats given
	}

	public void DisplayFinalSetup()
	{
		//name
	}

	private void ChooseClass(int classSelection)
	{
		if (classSelection == 0)
		{
			GameInformation.PlayerClass = new BaseWYG();
		}else if(classSelection != 0)
		{
			GameInformation.PlayerClass = new BaseWYG();
		}
	}

}
