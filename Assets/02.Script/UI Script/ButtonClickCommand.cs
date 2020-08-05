using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonClickCommand : MonoBehaviour
{
	public string Path;
	public Button Target;
	public string Args;

	private void Start()
	{
		//뷰모델의 패스를 통해 버튼 onclick 이벤트에 메서드를 추가해준다.
		string[] splits = Path.Split('.');
		string contextName = splits[0];
		string methodName = splits[1];
		
		var context = UIManager.Instance.GetContext(contextName);
		if(context)
		{
			Target.onClick.AddListener(delegate { context.SendMessage(methodName, Args); });
		}
	}

	private void Reset()
	{
		Target = GetComponent<Button>();
	}
}
