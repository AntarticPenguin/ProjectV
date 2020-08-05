using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI의 뷰 모델 클래스
public class Context : MonoBehaviour
{
	public void SetValue(string path, Object obj)
	{

	}
	/*
	 * Master Context 뷰모델의 루트
	 * 1. Context라는 뷰모델을 생성한다.
	 * 2. 뷰모델에는 경로가 될 string을 변수로 만들어준다.
	 * _context.setvalue("uipage.apptitle", value) -> 해당 경로가 있으면 데이터 세팅, 없으면 무시
	 * 3. UI 컴포넌트에서는 text setter와 같이 경로를 알려줄 스크립트 하나를 작성해 알아서 바인딩되게 한다.
	 * 씬 내의 오브젝트와 상호작용할 경우 : 예)버튼같은 경우 프리팹을 버튼에 드래그 하지말고, 메서드를 통해 해당 오브젝트에 접근할 수 있게하자
	 * 
	 * context
	 * {
	 *		UIPAGE uipage
	 *		{
	 *			Text apptitle
	 *		}
	 *		
	 *		setvalue(string path, Object obj)
	 *		{
	 *			string ui = path.split('.')[0];
				string property = path.split('.')[1];

				Type t = UIMANAGER[ui].GetType();
				PropertyInfo pi = t.GetProperty(property);
				if(pi != null)
				{
					pi.SetValue(UIMANAGER[ui], obj);
				}
	 *		}
	 * }
	 */
}
