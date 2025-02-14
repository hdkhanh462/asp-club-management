using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.WindowsClient.Utils
{
    public class Helpers
    {
        public static void LoadComboBoxs<T>(ComboBox control) where T : Enum
        {
            var values = Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .Select(g => new { Value = g, DisplayName = g.GetDisplayName() })
                .ToList();

            control.DataSource = values;
            control.DisplayMember = "DisplayName";
            control.ValueMember = "Value";
        }
    }
}
