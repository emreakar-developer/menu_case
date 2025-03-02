using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

[UxmlElement]
public partial class LocalizedButton : Button
{
    public static BindingId keyProperty = nameof(key);

    [UxmlAttribute]
    public string key;
    public LocalizedButton()
    {
        this.schedule.Execute(() =>
        {
            if (!string.IsNullOrEmpty(key))
            {
                string loc = LocalizationSettings.StringDatabase.GetLocalizedString(key);
                if (!string.IsNullOrEmpty(loc))
                {
                    text = loc;
                    return;
                }
                text = key;
            }
        });
    }
}