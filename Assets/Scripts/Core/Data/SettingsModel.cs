using System;
using UnityEngine;
using Zenject;
using UniRx;
namespace Core.Data
{
   [Serializable]
   public class SerializableSettingsModel
   {
      [Range(0f, 100f)]
      public float masterVolume;
   }

   public class SettingsModel : IInitializable
   {
      private DataContainer _dataContainer;
      public ReadOnlyReactiveProperty<bool> MuteAudio { get; private set;}
      public ReactiveProperty<float> AudioVolume { get; private set;}

      public void Save()
      {
         var serializable = new SerializableSettingsModel { masterVolume = AudioVolume.Value };
         _dataContainer.SaveSettings(serializable);
      }

      [Inject]
      public SettingsModel(DataContainer dataContainer)
      {
         _dataContainer = dataContainer;
      }
      
      public void Initialize()
      {
         var settings = _dataContainer.LoadSettings();
         AudioVolume = new ReactiveProperty<float>(settings.masterVolume);
         MuteAudio = AudioVolume.Select(val => val == 0).ToReadOnlyReactiveProperty();
      }
   }
}
