using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public static int cellSize=256;
	public static int pixelToUnit=100;

	public static float Get(float v){
		return v * cellSize / pixelToUnit;
	}
}
