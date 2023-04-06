using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class SwitchCamera : MonoBehaviour
{
	public Move player;

	[SerializeField]
	private CinemachineVirtualCamera DeadCamera;
	[SerializeField]
	private CinemachineVirtualCamera IdleCamera;
	[SerializeField]
	private CinemachineVirtualCamera WalkCamera;
	[SerializeField]
	private CinemachineVirtualCamera RunCamera;
	
	
	// Start is called before the first frame update
	void Start()
	{

	}

	void SetDisenableAllCamera()
    {
		//DeadCamera.enable = false;
		//IdleCamera.enable = false;
		//RunCamera.enable = false;
		//WalkCamera.enable = false;
    }

	int Camera_index = 0;
	void FixedUpdate()
	{
		Debug.Log(Camera_index);
		

		//IdleCamera.Priority = 100;
		//WalkCamera.Priority = 0;
		//RunCamera.Priority = 0;
		//DeadCamera.Priority = 0;
		

		//if (Input.GetKeyDown(KeyCode.P))
        //{
		//	_index = (_index + 1) % 4;
		//	

		//	Debug.Log($"switch->{_index}");
		//	Debug.Log($"switch->{(IdleCamera.Priority, WalkCamera.Priority, RunCamera.Priority, DeadCamera.Priority)}");
		//}
		//return;

		if (!player.isDead)
        {
			
			if (player.isWalk)
			{
				if (player.isRun)
				{
					Camera_index = 2;
				}
				else
				{
					Camera_index = 1;
				}
			}
			else
			{
				// idle
				Camera_index = 0;
			}
			//Camera_index = 1;
		}
        else
        {
			// dead
			Camera_index = 3;
        }
		

		/*if (player.isIdle == true)
		{
			//IdleCamera.Priority = 100;
			Camera_index = 0;
		}
		else
		{
			//IdleCamera.Priority = defaultPriority;
		}
		if (player.isWalk == true)
		{
			//WalkCamera.Priority = 100;
			Camera_index = 1;
		}
		else
		{
			//WalkCamera.Priority = defaultPriority;
		}
		if (player.isRun == true)
		{
			//RunCamera.Priority = 100;
			Camera_index = 2;
		}
		else
		{
			//RunCamera.Priority = defaultPriority;
		}
		if (player.isDead == true)
		{
			//DeadCamera.Priority = 100;
			//DeadCamera.Priority = 100;
			Camera_index = 3;
		}
		else
		{
			//DeadCamera.Priority = defaultPriority;
		}*/

		IdleCamera.Priority = Camera_index == 0 ? 100 : 0;
		WalkCamera.Priority = Camera_index == 1 ? 100 : 0;
		RunCamera.Priority = Camera_index == 2 ? 100 : 0;
		DeadCamera.Priority = Camera_index == 3 ? 100 : 0;

		
	}
}
