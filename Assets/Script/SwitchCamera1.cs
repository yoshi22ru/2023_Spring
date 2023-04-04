using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera1 : MonoBehaviour
{
	[SerializeField]
	[Tooltip("�؂�ւ���̃J����")]
	private CinemachineVirtualCamera virtualCamera;

	// �؂�ւ���̃J�����̌��X��Priority��ێ����Ă���
	private int defaultPriority;

	// Start is called before the first frame update
	void Start()
	{
		defaultPriority = virtualCamera.Priority;
	}

	/// <summary>
	/// Collider�͈̔͂ɓ��葱���Ă���Ԏ��s���ꑱ����
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerStay(Collider other)
	{
		// �������������"Player"�^�O���t���Ă����ꍇ
		if (other.gameObject.tag == "Player")
		{
			// ����virtualCamera���������D��x�ɂ��邱�ƂŐ؂�ւ��
			virtualCamera.Priority = 100;
		}
	}

	/// <summary>
	/// Collider����o���Ƃ��Ɏ��s�����
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerExit(Collider other)
	{
		// �������������"Player"�^�O���t���Ă����ꍇ
		if (other.gameObject.tag == "Player")
		{
			// ����priority�ɖ߂�
			virtualCamera.Priority = defaultPriority;
		}
	}
}
