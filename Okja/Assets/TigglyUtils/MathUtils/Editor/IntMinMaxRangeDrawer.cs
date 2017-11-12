using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomPropertyDrawer( typeof( IntMinMaxRangeAttribute ) )]
public class IntMinMaxRangeDrawer : PropertyDrawer
{
	public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
	{
		return base.GetPropertyHeight( property, label ) + 16;
	}

	// Draw the property inside the given rect
	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	{
		// Now draw the property as a Slider or an IntSlider based on whether it’s a float or integer.
		if ( property.type != "IntMinMaxRange" )
			Debug.LogWarning( "Use only with IntMinMaxRange type" );
		else
		{
			var range = attribute as IntMinMaxRangeAttribute;
			var minValue = property.FindPropertyRelative( "rangeStart" );
			var maxValue = property.FindPropertyRelative( "rangeEnd" );
			float newMin = minValue.intValue;
			float newMax = maxValue.intValue;
			int newMinI = minValue.intValue;
			int newMaxI = maxValue.intValue;

			var xDivision = position.width * 0.33f;
			var yDivision = position.height * 0.5f;
			EditorGUI.LabelField( new Rect( position.x, position.y, xDivision, yDivision )
				, label );

			EditorGUI.LabelField( new Rect( position.x, position.y + yDivision, position.width, yDivision )
				, range.minLimit.ToString( "0.##" ) );
			EditorGUI.LabelField( new Rect( position.x + position.width - 28f, position.y + yDivision, position.width, yDivision )
				, range.maxLimit.ToString( "0.##" ) );

			EditorGUI.LabelField( new Rect( position.x + xDivision, position.y, xDivision, yDivision )
				, "From: " );
			newMin = Mathf.Clamp( EditorGUI.IntField( new Rect( position.x + xDivision + 30, position.y, xDivision - 30, yDivision )
				, newMinI )
				, range.minLimit, newMax );
			EditorGUI.LabelField( new Rect( position.x + xDivision * 2f, position.y, xDivision, yDivision )
				, "To: " );
			newMax = Mathf.Clamp( EditorGUI.IntField( new Rect( position.x + xDivision * 2f + 24, position.y, xDivision - 24, yDivision )
				, newMaxI )
				, newMin, range.maxLimit );
			
			EditorGUI.MinMaxSlider( new Rect( position.x + 24f, position.y + yDivision, position.width - 48f, yDivision )
				, ref newMin, ref newMax, (float)range.minLimit, (float)range.maxLimit );

			minValue.intValue = Mathf.RoundToInt(newMin);
			maxValue.intValue = Mathf.RoundToInt(newMax);
		}
	}
}