using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelArm.Modules
{
    internal static class ObjectExt
    {
        /// <summary>
        /// 알 수 없는 객체에서 특정 이름의 속성 값을 가져옵니다.
        /// </summary>
        internal static object GetPropertyValue(this object _object, string property_name)
        {
            var propertyInfo = _object.GetType().GetProperty(property_name);

            if (propertyInfo == null)
                return null;

            return propertyInfo.GetValue(_object, null);
        }

        /// <summary>
        /// 알 수 없는 객체에서 특정 이름의 속성 값을 설정합니다.
        /// </summary>
        internal static void SetPropertyValue(this object _object, string property_name, object value)
        {
            var propertyInfo = _object.GetType().GetProperty(property_name);

            if (propertyInfo == null)
                return;

            propertyInfo.SetValue(_object, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }


        /// <summary>
        /// 알 수 없는 객체에서 같은 이름의 메서드가 존재하는지 확인하는 함수입니다.
        /// </summary>
        internal static bool PropertyExists(this object _object, string property_name)
        {
            if (_object == null)
                return false;

            if (_object is IDictionary<string, object> dict)
                return dict.ContainsKey(property_name);

            return _object.GetType().GetProperty(property_name) != null;
        }
    }
}
