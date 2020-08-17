using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MessageBox : MonoBehaviour
{
	public UnityAction OnOkCallback;

	public Button OkButton;
	public Button CancelButton;
	public Text contentText;

	// Start is called before the first frame update
	void Start()
    {
		if(OnOkCallback != null)
		{
			OkButton.onClick.AddListener(delegate {
				OnOkCallback.Invoke();
				Destroy(gameObject);
			});
		}
			

		CancelButton.onClick.AddListener(delegate { Destroy(gameObject); });
    }
}
