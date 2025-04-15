using System;
using System.Configuration;
using System.Windows.Forms;

namespace Library.SaveLoadConfig
{ 
    static class TextTo
    {
        public static double ToDouble(string str, double defaultValue)
        {
            if (!double.TryParse(str, out double result)) result = defaultValue;
            return result;
        }

        public static float ToSingle(string str, float defaultValue)
        {
            if (!float.TryParse(str, out float result)) result = defaultValue;
            return result;
        }

        public static int ToInt32(string str, int defaultValue)
        {
            if (!int.TryParse(str, out int result)) result = defaultValue;
            return result;
        }

        public static bool ToBool(string str, bool defaultValue)
        {
            if (!bool.TryParse(str, out bool result)) result = defaultValue;
            return result;
        }
    }

    public class LoadSaveConfiguration
    {
        public enum SaveLoadSelection
        {
            Save,
            Load
        }

        Configuration configFile;
        KeyValueConfigurationCollection cfgCollection;
        public LoadSaveConfiguration()
        {
            configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfgCollection = configFile.AppSettings.Settings;
        }

        ~LoadSaveConfiguration()
        {
        }

        public void SaveConfiguration(NumericUpDown nUpDn)
        {
            string key = nUpDn.Name;
            string PropertyValue = nUpDn.Value.ToString();
            SaveConfiguration(key, PropertyValue);
        }

        public void SaveConfiguration(ComboBox comboBox)
        {
            string key = comboBox.Name;
            string PropertyValue = comboBox.Text;
            SaveConfiguration(key, PropertyValue);
        } 

        public void SaveConfiguration(CheckBox checkBox)
        { 
            string key = checkBox.Name;
            string PropertyValue = checkBox.Checked.ToString(); 
            SaveConfiguration(key, PropertyValue);
        }

        public void SaveConfiguration(string key, string PropertyValue)
        {
            try
            {
                if (cfgCollection[key] == null)
                {
                    cfgCollection.Add(key, PropertyValue);
                }
                else
                {
                    cfgCollection.Remove(key);
                    cfgCollection.Add(key, PropertyValue);
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                // Console.WriteLine("configuration error exception occur!");
            }
        }

        /// <summary>
        /// TextBox 이름을 key로 하고 TextBox의 Text로 값을 저장 한다.
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="PropertyValue"></param>
        public void SaveConfiguration(TextBox textBox)
        {  
            string key = textBox.Name;
            string PropertyValue = textBox.Text; 
            SaveConfiguration(key, PropertyValue);
        }

        /// <summary>
        /// TextBox 이름을 key로 하고 TextBox의 Text로 값을 저장 한다.
        /// </summary>
        /// <param name="maskedTextBox"></param>
        /// <param name="PropertyValue"></param>
        public void SaveConfiguration(MaskedTextBox maskedTextBox)
        {
            string key = maskedTextBox.Name;
            string PropertyValue = maskedTextBox.Text;
             SaveConfiguration(key, PropertyValue); 
        }

        /// <summary>
        /// Object의 이름을 key로 하고 PropertyValue의 값을 저장 한다.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="PropertyValue"></param>
        public void SaveConfiguration(Control control, string PropertyValue)
        { 
            string objectName = GetControlName(control);
            if ((objectName != "ComboBox") && (objectName != "Label") && (objectName != "TextBox")) return; 
            string key = control.Name; 
            SaveConfiguration(key, PropertyValue);
        }

        /// <summary>
        /// Control의 이름을 key로 Control의 Text를 저장 한다.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="PropertyValue"></param>
        public void ControlSaveConfiguration(Control control)
        {

            string objectName = GetControlName(control);
            if ((objectName != "ComboBox") && (objectName != "Label") && (objectName != "TextBox")) return;

            string key = control.Name;
            string PropertyValue = control.Text;
            SaveConfiguration(key, PropertyValue);
        }

        /// <summary>
        /// RadioButton의 이름을 key로 RadioButton의 true or false 상태를 저장 한다.
        /// </summary>
        /// <param name="radioButton"></param>
        public void RadioButtonSaveConfiguration(RadioButton radioButton)
        {
            string key = radioButton.Name;
            string PropertyValue = radioButton.Checked.ToString();
            SaveConfiguration(key, PropertyValue);
        }

        /// <summary>
        /// TextBox의 이름을 key로 TextBox의 text 상태를 저장 한다.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="PropertyValue"></param>
        public void TextBoxSaveConfiguration(TextBox textBox)
        {
            string objectName = GetControlName(textBox);
            string key = textBox.Name;
            string PropertyValue = textBox.Text;
            SaveConfiguration(key, PropertyValue);
        }

        /// <summary>
        /// CheckBox의 이름을 key로 CheckBox의 true or false 상태를 저장 한다.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="PropertyValue"></param>
        public void CheckBoxSaveConfiguration(CheckBox checkbox)
        { 
            string objectName = GetControlName(checkbox);  
            string key = checkbox.Name;
            string PropertyValue = checkbox.Checked.ToString();
            SaveConfiguration(key, PropertyValue);
        }

        public string StringLoadConfiguration(string key, string defaultValue)
        {
            string PropertyValue = String.Empty;
            try
            {
                // null이 아닌 경우 왼쪽, null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key] ?? defaultValue;
                PropertyValue = PropertyValue.Trim();
                if (PropertyValue == "") PropertyValue = defaultValue;
            }
            catch (ConfigurationErrorsException)
            {
                // Console.WriteLine("configuration error exception occur!");
            }
            return PropertyValue;
        }

        /// <summary>
        /// obj로 지정된 Object의 이름을 key로 읽어 그 값을 string으로 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public void LoadConfiguration(NumericUpDown nUpDn, decimal defaultValue)
        {
            string key = nUpDn.Name;
            string PropertyValue = nUpDn.Text;
            try
            {
                // null이 아닌 경우 왼쪽 null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key];
                if (PropertyValue == null) PropertyValue = defaultValue.ToString();
                PropertyValue = PropertyValue.Trim();
                if (PropertyValue == "")
                    nUpDn.Value = defaultValue;
                else
                {
                    nUpDn.Value = decimal.Parse(PropertyValue);
                }
            }
            catch (ConfigurationErrorsException)
            {
                // Console.WriteLine("configuration error exception occur!");
            }
        }

        /// <summary>
        /// 지정된 CheckBoxd의 이름을 key로 읽어 그 값을 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public void LoadConfiguration(CheckBox checkBox, bool defaultValue)
        {
            string key = checkBox.Name;
            string PropertyValue = checkBox.Checked.ToString();
            try
            {
                // null이 아닌 경우 왼쪽 null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key];
                if (PropertyValue == null) PropertyValue = defaultValue.ToString();
                PropertyValue = PropertyValue.Trim();
                if (PropertyValue == "")
                    checkBox.Checked = defaultValue;
                else
                {
                    checkBox.Checked = bool.Parse(PropertyValue);
                }
            }
            catch (ConfigurationErrorsException)
            {
                checkBox.Checked = defaultValue;
                // Console.WriteLine("configuration error exception occur!");
            }
        }

        /// <summary>
        /// obj로 지정된 Object의 이름을 key로 읽어 그 값을 string으로 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public void LoadConfiguration(TextBox textBox, string defaultValue)
        {
            string key = textBox.Name;
            string PropertyValue = textBox.Text; 
            try
            {
                // null이 아닌 경우 왼쪽 null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key];
                if (PropertyValue==null) PropertyValue = defaultValue;
                PropertyValue = PropertyValue.Trim();
                if (PropertyValue == "")
                    textBox.Text = defaultValue;
                else
                { 
                    textBox.Text = PropertyValue;
                }
            }
            catch (ConfigurationErrorsException)
            {
                // Console.WriteLine("configuration error exception occur!");
            }
        }

        /// <summary>
        /// obj로 지정된 Object의 이름을 key로 읽어 그 값을 string으로 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public void LoadConfiguration(MaskedTextBox maskedTextBox, string defaultValue)
        {
            string key = maskedTextBox.Name;
            string PropertyValue = maskedTextBox.Text;
            try
            {
                // null이 아닌 경우 왼쪽 null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key];
                if (PropertyValue == null) PropertyValue = defaultValue;
                PropertyValue = PropertyValue.Trim();
                if (PropertyValue == "")
                    maskedTextBox.Text = defaultValue;
                else
                {
                    maskedTextBox.Text = PropertyValue;
                }
            }
            catch (ConfigurationErrorsException)
            {
                // Console.WriteLine("configuration error exception occur!");
            }
        }

        /// <summary>
        /// obj로 지정된 Object의 이름을 key로 읽어 그 값을 string으로 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public void LoadConfiguration(ComboBox comboBox, string defaultValue)
        {
            string key = comboBox.Name;
            string PropertyValue = comboBox.Text;
            try
            {
                // null이 아닌 경우 왼쪽 null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key] ?? defaultValue;
                PropertyValue = PropertyValue.Trim(); 

                // 선택한 Index를 찾는다.
                if (PropertyValue != "")
                {
                    comboBox.Text = PropertyValue;  
                    comboBox.SelectedIndex = comboBox.FindStringExact(PropertyValue);
                }
                else
                {
                    comboBox.Text = defaultValue;
                    comboBox.SelectedIndex = -1;
                }
            }
            catch (ConfigurationErrorsException)
            {
                // Console.WriteLine("configuration error exception occur!");
            }
        } 

        /// <summary>
        /// obj로 지정된 Object의 이름을 key로 읽어 그 값을 string으로 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string StringLoadConfiguration(Control control, string defaultValue)
        {
            string objectName = GetControlName(control);
            if ((objectName != "ComboBox") && (objectName != "Label") && (objectName != "TextBox")) return defaultValue;

            string key = control.Name;
            string PropertyValue = defaultValue;
            try
            {
                // null이 아닌 경우 왼쪽 null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key] ?? defaultValue;
                PropertyValue = PropertyValue.Trim();
                if (PropertyValue == "") PropertyValue = defaultValue;
            }
            catch (ConfigurationErrorsException)
            {
                // Console.WriteLine("configuration error exception occur!");
            }
            return PropertyValue;
        }

        public void RadioButtonLoadConfiguration(RadioButton radioButton, bool defaultValue)
        {
            string key = radioButton.Name;
            string PropertyValue = radioButton.Checked.ToString();
            try
            {
                // null이 아닌 경우 왼쪽 null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key];
                if (PropertyValue == null) PropertyValue = defaultValue.ToString();
                PropertyValue = PropertyValue.Trim();
                if (PropertyValue == "")
                    radioButton.Checked = defaultValue;
                else
                {
                    radioButton.Checked = bool.Parse(PropertyValue);
                }
            }
            catch (ConfigurationErrorsException)
            {
                radioButton.Checked = defaultValue;
                // Console.WriteLine("configuration error exception occur!");
            }
        }

        /// <summary>
        /// obj로 지정된 Object의 이름을 key로 읽어 그 값을 string으로 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public void TextBoxLoadConfiguration(Control control, string defaultValue)
        {
            string objectName = GetControlName(control);
            if (objectName != "TextBox") return;

            string key = control.Name;
            string PropertyValue = "";
            try
            {
                // null이 아닌 경우 왼쪽 null인 경우 오른쪽 값을 반환한다.
                PropertyValue = ConfigurationManager.AppSettings[key] ?? defaultValue;
                PropertyValue = PropertyValue.Trim();
                if (PropertyValue == "") PropertyValue = defaultValue;
            }
            catch (ConfigurationErrorsException)
            {
                // Console.WriteLine("configuration error exception occur!");
            }

            // Type에 관계없이 TextBox로 처리한다.
            ((TextBox)control).Text = PropertyValue;     // PropertyValue를 Text에 저장 한다.
        }

        internal string GetControlName(Control control)
        {
            Type objtype = control.GetType();
            string objectName = objtype.Name;
            // TextBox의 변종은 모두 TextBox로 처리한다.
            if (objectName.Length >= 7)
            {
                if (objectName.Substring(0, 7) == "TextBox") objectName = "TextBox";
            }
            return objectName;
        }

        /// <summary>
        /// obj로 지정된 Object의 이름을 key로 읽어 그 값을 string으로 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public void ControlLoadConfiguration(Control control, string defaultValue)
        {
            string objectName = GetControlName(control);
            if ((objectName != "ComboBox") && (objectName != "Label") && (objectName != "TextBox")) return;

            string key = control.Name;

            // Type에 관계없이 TextBox로 처리한다.
            control.Text = StringLoadConfiguration(key, defaultValue);   // PropertyValue를 Text에 저장 한다.
        }

        /// <summary>
        /// CheckBox의 이름을 key로 읽어 그 값을 string으로 리턴 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public void CheckBoxLoadConfiguration(CheckBox checkBox, bool defaultValue)
        { 
            string key = checkBox.Name;

            // Type에 관계없이 TextBox로 처리한다.
            checkBox.Checked = BoolLoadConfiguration(key, defaultValue);   // PropertyValue를 Text에 저장 한다.
        }

        public int IntLoadConfiguration(string key, int defaultValue)
        {
            string PropertyValue = LoadConfiguration(key, defaultValue.ToString());
            return TextTo.ToInt32(PropertyValue, defaultValue);
        }

        public float FloatLoadConfiguration(string key, float defaultValue)
        {
            string PropertyValue = LoadConfiguration(key, defaultValue.ToString());
            return TextTo.ToSingle(PropertyValue, defaultValue);
        } 

        public double DoubleLoadConfiguration(string key, double defaultValue)
        {
            string PropertyValue = LoadConfiguration(key, defaultValue.ToString());
            return TextTo.ToDouble(PropertyValue, defaultValue);
        }

        /// <summary>
        /// key의 값을 bool로 return 한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool BoolLoadConfiguration(string key, bool defaultValue)
        {
            string PropertyValue = LoadConfiguration(key, defaultValue.ToString());
            return TextTo.ToBool(PropertyValue, defaultValue);
        }
        
        public string LoadConfiguration(string key, string defaultValue)
        {
            string PropertyValue = String.Empty;
            try
            {
                PropertyValue = ConfigurationManager.AppSettings[key] ?? defaultValue;
            }
            catch (ConfigurationErrorsException)
            {
                return defaultValue;
            }
            return PropertyValue;
        }
    }
}
