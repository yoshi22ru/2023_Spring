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


	// êÿÇËë÷Ç¶å„ÇÃÉJÉÅÉâÇÃå≥ÅXÇÃPriorityÇï€éùÇµÇƒÇ®Ç≠
	private int defaultPriority;

	// Start is called before the first frame update
	void Start()
	{
		defaultPriority = IdleCamera.Priority;
	}

	void FixedUpdate()
	{
		if (player.isIdle == true)
		{
			IdleCamera.Priority = 100;
		}
		else
		{
			IdleCamera.Priority = defaultPriority;
		}
		if (player.isWalk == true)
		{
			WalkCamera.Priority = 100;
		}
		else
		{
			WalkCamera.Priority = defaultPriority;
		}
		if (player.isRun == true)
		{
			RunCamera.Priority = 100;
		}
		else
		{
			RunCamera.Priority = defaultPriority;
		}
		if (player.isDead == true)
		{
			DeadCamera.Priority = 100;
		}
		else
		{
			DeadCamera.Priority = defaultPriority;
		}
	}
}
