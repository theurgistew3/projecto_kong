using UnityEngine;

namespace helper
{
	public static class text
	{
		public static TextMesh _( string text, Transform parent, Vector3 position )
		{
			return _( text, parent, position, Quaternion.identity );
		}

		public static TextMesh _( string text, Transform parent, Vector3 position, Quaternion rotation )
		{
			GameObject obj = new GameObject( string.Format( "text {0}", text ), typeof( TextMesh ) );
			obj.transform.parent = parent;
			obj.transform.localPosition = position;
			obj.transform.rotation = rotation;
			TextMesh text_mesh = obj.GetComponent<TextMesh>();
			text_mesh.text = text;
			return text_mesh;
		}
	}
}
