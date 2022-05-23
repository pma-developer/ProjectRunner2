using System;
using UnityEngine;
using Zenject;
using UniRx;
namespace Core.Data
{
   [Serializable]
   public class SerializableSettingsModel
   {
      public float masterVolume;
   }

   public class SettingsModel : IInitializable
   {
      private Repository _repository;
      public ReadOnlyReactiveProperty<bool> MuteAudio { get; private set;}
      public ReactiveProperty<float> AudioVolume { get; private set;}

      public void Save()
      {
         var serializable = new SerializableSettingsModel { masterVolume = AudioVolume.Value };
         _repository.SaveSettings(serializable);
      }

      [Inject]
      public SettingsModel(Repository repository)
      {
         _repository = repository;
      }
      
      public void Initialize()
      {
         var settings = _repository.LoadSettings();
         AudioVolume = new ReactiveProperty<float>(settings.masterVolume);
         MuteAudio = AudioVolume.Select(val => val == 0).ToReadOnlyReactiveProperty();
      }
   }
}
