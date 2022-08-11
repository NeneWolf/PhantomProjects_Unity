using UnityEngine;
/// <summary>
/// Tool script taken from the UI source as it's set to Internal for some reason. So to use in the extensions project it is needed here also.
/// </summary>
/// 
namespace com.cygnusprojects.TalentTree
{
    internal static class SetPropertyUtility
    {
        public static bool SetClass<T>(ref T currentValue, T newValue) where T: class
        {
            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
                return false;

            currentValue = newValue;
            return true;
        }
    }
}
