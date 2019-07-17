﻿using System;
using System.Collections.Generic;
using UnityEngine;


namespace RapidGUI.Example
{
    /// <summary>
    /// GUIUtil.Field() examples
    /// </summary>
    public class FieldExample : ExampleBase
    {
        public enum EnumSample
        {
            One,
            Two,
            Three,
        };

        [Flags]
        public enum EnumSmapleFlags
        {
            Flag001 = 1,
            Flag010 = 2,
            Flag100 = 4,
        }

        /// <summary>
        /// Custom Class
        ///  RapidGUI displays member fields with the same rules as unity serialize(public, SerializeField, NonSerialized).
        ///  if class is ICloneable or has Copy Constructor then Array/List element will be dupricate when add new element.
        /// </summary>
        [Serializable]
        public class CustomClass : ICloneable
        {
            public int intVal;

            [SerializeField]
            protected float floatVal;

            public string stringVal;

            [NonSerialized]
            public int nonSerialized;


            public object Clone()
            {
                return new CustomClass()
                {
                    intVal = intVal,
                    floatVal = floatVal,
                    stringVal = stringVal
                };
            }
        }

        /// <summary>
        /// ComplecClass
        ///  display Slider for member field that has RangAttribute
        /// </summary>
        [Serializable]
        public class ComplexClass
        {
            [Range(0f, 10f)]
            public float rangeVal;
            public string longNameFieldSoThatWillBeMultiLine;
            public CustomClass customClass = new CustomClass();
            public ComplexClass complexClass = null;
            public float[] floatList;
            public List<CustomClass> customClassList;
        }

        public string stringVal;
        public bool boolVal;
        public int intVal;
        public float floatVal;
        public Color colorVal;
        public EnumSample enumVal;
        public EnumSmapleFlags enumFlagsVal;

        public Vector2 vector2Val;
        public Vector3 vector3Val;
        public Vector4 vector4Val;
        public Vector2Int vector2IntVal;
        public Vector3Int vector3IntVal;
        public Rect rectVal;
        public RectInt rectIntVal;
        public RectOffset rectOffsetVal;
        public Bounds boundsVal;
        public BoundsInt boundsIntVal;

        public float[] arrayVal;
        public List<int> listVal;

        public CustomClass customClassVal = new CustomClass();
        public List<CustomClass> customClassListVal;

        public ComplexClass complexClassVal = new ComplexClass();
        public List<ComplexClass> complexClassListVal;

        private void Start()
        {
            var c = new ComplexClass();
            c.complexClass = complexClassVal;
            complexClassVal.complexClass = c; // circular reference
        }


        protected override string title => "RGUI.Field()";

        public override void DoGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                using (new GUILayout.VerticalScope())
                {
                    stringVal = RGUI.Field(stringVal, "string");
                    boolVal = RGUI.Field(boolVal, "bool");

                    intVal = RGUI.Field(intVal, "int");
                    floatVal = RGUI.Field(floatVal, "float");
                    colorVal = RGUI.Field(colorVal, "color");
                    enumVal = RGUI.Field(enumVal, "enum");
                    enumFlagsVal = RGUI.Field(enumFlagsVal, "enumFlags");

                    vector2Val = RGUI.Field(vector2Val, "vector2");


                    vector3Val = RGUI.Field(vector3Val, "vector3");
                    vector4Val = RGUI.Field(vector4Val, "vector4");

                    vector2IntVal = RGUI.Field(vector2IntVal, "vector2Int");
                    vector3IntVal = RGUI.Field(vector3IntVal, "vector3Int");

                    rectVal = RGUI.Field(rectVal, "rect");
                    rectIntVal = RGUI.Field(rectIntVal, "rectInt");
                    rectOffsetVal = RGUI.Field(rectOffsetVal, "rectOffset");

                    boundsVal = RGUI.Field(boundsVal, "bounds");
                    boundsIntVal = RGUI.Field(boundsIntVal, "boundsInt");

                    arrayVal = RGUI.Field(arrayVal, "array");
                    listVal = RGUI.Field(listVal, "list");
                }


                using (new GUILayout.VerticalScope())
                {
                    customClassVal = RGUI.Field(customClassVal, "customClass");
                    customClassListVal = RGUI.Field(customClassListVal, "customClassList");

                    complexClassVal = RGUI.Field(complexClassVal, "complexClass");
                    complexClassListVal = RGUI.Field(complexClassListVal, "complexClassList");
                }
            }
        }
    }
}