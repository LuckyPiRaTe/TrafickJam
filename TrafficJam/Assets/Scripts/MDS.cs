using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MDS : MonoBehaviour {

	[Header("Graphicks")]
	public static Texture2D white,grad,block,backT,TargetT, NullT;
	public Texture2D _white,_grad,_block,_backT,_TargetT, _NullT;


	Vector3 mousePos;
	public Vector3 mouseDeltaPosition;


	//Swipe
	public bool swipeLeft, swipeRight;
	bool swipeLeftSave, swipeRightSave;


	void Start () {
		mousePos = Input.mousePosition;
		white =_white;
	}

	void Update () {
		white=_white;
		grad=_grad;
		block=_block;
		backT=_backT;
		TargetT=_TargetT;
		NullT = _NullT;



		if (Input.GetMouseButtonDown(0)) { mousePos = Input.mousePosition; }
		if (Input.GetMouseButton(0)) {
			mouseDeltaPosition = Input.mousePosition - mousePos;
			mousePos = Input.mousePosition;
		}
		if (!Input.GetMouseButton(0)) { mouseDeltaPosition = new Vector3(); }

		//-----------------+ Swipe +-----------------
		// LEFT
		if (swipeLeftSave) { swipeLeft = false; }
		if ((mouseDeltaPosition.x > 10) && (!swipeLeftSave)) {
			swipeLeft = true;
			swipeLeftSave = true;
		}

		// RIGHT
		if (swipeRightSave) { swipeRight = false; }
		if ((mouseDeltaPosition.x < -10) && (!swipeRightSave))
		{
			swipeRight = true;
			swipeRightSave = true;
		}


		if (!Input.GetMouseButton(0)) { swipeLeftSave = false; swipeRightSave = false; }
		//-------------------------------------------

	}

	public static bool testOnNumbers(string c){
		if(c=="0" || c=="1" || c=="2" || c=="3" || c=="4" || c=="5" || c=="6" || c=="7" || c=="8" || c=="9"){return true;}return false;
	}

	//=================== FLOAT ===================
	public static float FloatAnim(float a,float b,float anim){return (a*(1-anim)+b*anim);}
	public static float RandomFloat(float min,float max){
		int minI=(int)(min*1000000);
		int maxI=(int)(max*1000000);
		float rand=(Random.Range(minI,maxI))/1000000f;
		return rand;
	}
	public static float Absolute(float a){if(a<0){a*=-1;}return a;}
	public static float CircleAnim(float a){return Mathf.Sin(Mathf.PI/2*a);}
	public static Rect SizeScreen(){return new Rect(0,0,Screen.width,Screen.height);}
	public static Rect CentrScreen(){return new Rect(Screen.width/2,Screen.height/2,0,0);}
	//=============================================

	//================== COLOR ==================
	public static Color ColorAnim(Color color1,Color color2,float anim){
		return new Color(color1.r*(1-anim)+color2.r*(anim),
			color1.g*(1-anim)+color2.g*(anim),
			color1.b*(1-anim)+color2.b*(anim),
			color1.a*(1-anim)+color2.a*(anim));
	}
	public static Color ColorSetAlpha(Color color,float alpha){return new Color(color.r,color.g,color.b,alpha);}
	//===========================================

	//=========================== TEST ON DISTANCE|POINT ===========================
	public static bool TestPointInRect(Rect rect,Vector3 point){return TestPointInRect(rect,new Vector2(point.x,point.y));}
	public static bool TestPointInRect(Rect rect,Vector2 point){
		if(point.x>=rect.x && point.x<rect.x+rect.width
			&& point.y>=rect.y && point.y<rect.y+rect.height){return true;}
		return false;
	}
	//==============================================================================

	//============================ BUTTONS ============================
	public static void ButtonTextureFree(){
		GUI.skin.button.normal.background= NullT;
		GUI.skin.button.hover.background= NullT;
		GUI.skin.button.active.background= NullT;
	}
	//=================================================================

	//========================= RECT =========================
	public static Rect RectScale(Rect rect,float a){return new Rect(rect.x-rect.width*(a-1)/2,rect.y-rect.height*(a-1)/2,rect.width*a,rect.height*a);}
	public static Rect RectAnim(Rect rect1,Rect rect2,float anim){
		return new Rect(
			rect1.x*(1-anim)+rect2.x*anim
			,rect1.y*(1-anim)+rect2.y*anim
			,rect1.width*(1-anim)+rect2.width*anim
			,rect1.height*(1-anim)+rect2.height*anim);
	}
	//========================================================

	//========================= VECTORS =========================
	public static Vector2 Vector2Anim(Vector2 v1,Vector2 v2,float anim){
		return new Vector2(
			v1.x*(1-anim)+v2.x*anim
			,v1.y*(1-anim)+v2.y*anim);}
	public static Vector3 Vector3Anim(Vector3 v1,Vector3 v2,float anim){
		return new Vector3(
			v1.x*(1-anim)+v2.x*anim
			,v1.y*(1-anim)+v2.y*anim
			,v1.z*(1-anim)+v2.z*anim);}
	//===========================================================

	//============================ TEXTURE ============================
	public static class RectMode{
		public static Rect cropMax(Rect rect,Texture2D texture){
			Rect rect2=new Rect(0,0,0,0);
			if(texture!=null){
				//GUI.BeginGroup(rect);
				float w=texture.width;
				float h=texture.height;
				if((w/h)>(rect.width/rect.height)){float k=rect.height/h;rect2=new Rect((rect.width-w*k)/2,0,w*k,rect.height);}
				else{float k=rect.width/w;rect2=new Rect(0,(rect.height-h*k)/2,rect.width,h*k);}
				//GUI.DrawTexture(rect2,texture);
				//GUI.EndGroup();
			}
			return rect2;
		}
		public static Rect cropMin(Rect rect,Texture2D texture){
			Rect rect2=rect;
			//GUI.BeginGroup(rect);
			if(texture!=null){
				if(rect.width/rect.height<texture.width/texture.height){
					rect2=new Rect(0,rect.height/2-rect2.height/2,rect.width,rect.width/(texture.width/texture.height));
				}else{
					rect2=new Rect(rect.width/2-rect2.width/2,0,rect.height*(texture.width/texture.height),rect.height);
				}
				//GUI.DrawTexture(rect2,texture);
			}
			//	GUI.EndGroup();
			return rect2;
		}
	}

	public static class TextureMode{
		public static Rect cropMax(Rect rect,Texture2D texture){
			Rect rect2=new Rect(0,0,0,0);
			if(texture!=null){
				//GUI.BeginGroup(rect);
				float w=texture.width;
				float h=texture.height;
				/*
				if((rect.height/rect.width)<(h/w)){
					float k=(rect.width/w);
					rect2.width=w*k;
					rect2.height=h*k;
					rect2.x=0;
					rect2.y=rect.height/2-rect2.height/2;
				}else{
					
				}*/
				if((w/h)>(rect.width/rect.height)){float k=rect.height/h;rect2=new Rect(-(rect.width - w*k)/2,0,w*k,rect.height);}
				else{float k=rect.width/w;rect2=new Rect(0,-(rect.height-h*k)/2,rect.width,h*k);}
				rect2.x += rect.x;
				rect2.y += rect.y;
				//GUI.DrawTexture(rect2, texture);
				//GUI.EndGroup();
			}
			return rect2;
		}
		public static Rect cropMin(Rect rect,Texture2D texture){
			Rect rect2=rect;
			GUI.BeginGroup(rect);
			if(texture!=null){
				if(rect.width/rect.height<texture.width/texture.height){
					rect2=new Rect(0,rect.height/2-rect2.height/2,rect.width,rect.width/(texture.width/texture.height));
				}else{
					rect2=new Rect(rect.width/2-rect2.width/2,0,rect.height*(texture.width/texture.height),rect.height);
				}
				GUI.DrawTexture(rect2,texture);
			}
			GUI.EndGroup();
			return rect2;
		}
	}
	//=================================================================


	//============================ Quaternion ============================
	public static Quaternion QuaternionAnim(Quaternion quaternion1, Quaternion quaternion2, float anim) {
		return new Quaternion(
							quaternion1.x * (1 - anim) + quaternion2.x * anim,
							quaternion1.y * (1 - anim) + quaternion2.y * anim,
							quaternion1.z * (1 - anim) + quaternion2.z * anim,
							quaternion1.w * (1 - anim) + quaternion2.w * anim);
	}
	//====================================================================
}