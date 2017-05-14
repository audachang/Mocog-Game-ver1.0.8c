using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class ObjExporterScript {

	public static string MeshToString(MeshFilter mf) {
		Mesh m = mf.mesh;
		Material[] mats = mf.GetComponent<Renderer>().sharedMaterials;

		StringBuilder sb = new StringBuilder();

		sb.Append("g ").Append(mf.name).Append("\n");
		foreach(Vector3 v in m.vertices) {
			sb.Append(string.Format("v {0} {1} {2}\n",v.x,v.y,v.z));
		}
		sb.Append("\n");
		foreach(Vector3 v in m.normals) {
			sb.Append(string.Format("vn {0} {1} {2}\n",v.x,v.y,v.z));
		}
		sb.Append("\n");
		foreach(Vector2 v in m.uv) {
			sb.Append(string.Format("vt {0} {1}\n",v.x,v.y));
		}
		sb.Append("\n");
		foreach(Vector2 v in m.uv2) {
			sb.Append(string.Format("vt1 {0} {1}\n",v.x,v.y));
		}
		sb.Append("\n");
		foreach(Vector2 v in m.uv2) {
			sb.Append(string.Format("vt2 {0} {1}\n",v.x,v.y));
		}
		sb.Append("\n");
		foreach(Color c in m.colors) {
			sb.Append(string.Format("vc {0} {1} {2} {3}\n",c.r,c.g,c.b,c.a));
		}
		for (int material=0; material < m.subMeshCount; material ++) {
			sb.Append("\n");
			sb.Append("usemtl ").Append(mats[material].name).Append("\n");
			sb.Append("usemap ").Append(mats[material].name).Append("\n");

			int[] triangles = m.GetTriangles(material);
			for (int i=0;i<triangles.Length;i+=3) {
				sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", 
				                        triangles[i]+1, triangles[i+1]+1, triangles[i+2]+1));
			}
		}
		return sb.ToString();
	}

	public static void MeshToFile(MeshFilter mf, string filename, bool append)
	{
		try
		{
			using (StreamWriter sw = new StreamWriter(filename, append)) 
			{
				sw.WriteLine(MeshToString(mf));
			}
		}
		catch (System.Exception)
		{
		}
	}
}