using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cygnusprojects.TalentTree
{
    [Serializable]
    public class Property : ScriptableObject, IProperty
    {
        #region Variables
        [SerializeField]
        private PropertyType propertyType = PropertyType.String;
        [SerializeField]
        private bool boolValue = false;
        [SerializeField]
        private float floatValue = 0.0f;
        [SerializeField]
        private int intValue = 0;
        [SerializeField]
        private string stringValue = string.Empty;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public PropertyType PropertyType
        {
            get { return propertyType; }
        }

        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        public float FloatValue
        {
            get { return floatValue; }
            set { floatValue = value; }
        }

        public int IntValue
        {
            get { return intValue; }
            set { intValue = value; }
        }

        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
        #endregion

        #region Implementation
        public Property (String name, PropertyType propertyType)
        {
            this.propertyType = propertyType;
            this.name = name;
        }
        #endregion
  
    }
}
