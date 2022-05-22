using Zenject;

namespace Core.Data
{
    public class Repository : IInitializable
    {
        private DataProvider<SerializableSettingsModel> _settingsDataProvider;
        private DataPaths _dataPaths;

        public SerializableSettingsModel LoadSettings() => _settingsDataProvider.Load();

        public void SaveSettings(SerializableSettingsModel settingsModel) => _settingsDataProvider.Save(settingsModel);

        [Inject]
        public void Init(DataPaths dataPaths)
        {
            _dataPaths = dataPaths;
        }

        public void Initialize()
        {
            _settingsDataProvider = new DataProvider<SerializableSettingsModel>(_dataPaths.SettingsFileName);
        }
    }
}