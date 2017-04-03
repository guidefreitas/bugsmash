using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityConfig : MonoBehaviour {

	public int qualityLevel = 5;

	void Start () {
		 #if UNITY_ANDROID
         
				Application.targetFrameRate = 60;
				
				QualitySettings.vSyncCount = 1; 
				
				QualitySettings.antiAliasing = 0;
				
				if (qualityLevel == 0)
				{
					QualitySettings.shadowCascades = 0;
					QualitySettings.shadowDistance = 15;
				}
				
				else if (qualityLevel == 5)
				{
					QualitySettings.shadowCascades = 2;
					QualitySettings.shadowDistance = 70;
				}
				
				Screen.sleepTimeout = SleepTimeout.NeverSleep;
				
		#endif
		
	}
}
