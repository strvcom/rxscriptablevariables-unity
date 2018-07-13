using UnityEngine;

namespace STRV.Variables.Persistance
{
    [CreateAssetMenu(menuName = "Variables/Peristance/Player Prefs Serializer", order = 15)]
    public class PlayerPrefsSerializer: ValueSerializer
    {        
        public override bool Load(ISerializable target)
        {
            if (PlayerPrefs.HasKey(target.GetKey()))
            {
                var value = PlayerPrefs.GetString(target.GetKey());
                target.SetStringValue(value);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Save(ISerializable value)
        {
            PlayerPrefs.SetString(value.GetKey(), value.GetStringValue());
            PlayerPrefs.Save();
            return true;
        }
    }
}