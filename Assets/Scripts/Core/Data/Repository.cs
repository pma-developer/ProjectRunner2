using Zenject;

namespace Core.Data
{
    public class Repository : IInitializable
    {
        private DataProvider<SerializableSettingsModel> _settingsDataProvider;
        private DataPaths _dataPaths;
        private SettingsPreset _settingsPreset;

        public SerializableSettingsModel LoadSettings() => _settingsDataProvider.Load();

        public void SaveSettings(SerializableSettingsModel settingsModel) => _settingsDataProvider.Save(settingsModel);

        [Inject]
        public void Init(DataPaths dataPaths, SettingsPreset settingsPreset)
        {
            _dataPaths = dataPaths;
            _settingsPreset = settingsPreset;
        }

        public void Initialize()
        {
            _settingsDataProvider = new DataProvider<SerializableSettingsModel>(_dataPaths.SettingsFilePath);

            if (LoadSettings() is null)
            {
                WriteSampleSettings();
            }
        }

        private void WriteSampleSettings()
        {
            _settingsDataProvider.Save(_settingsPreset.Preset);
        }
    }
}