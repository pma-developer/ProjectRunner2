using Zenject;
using UniRx;
namespace Core.Data
{
   public class SettingsModel {
      public ReadOnlyReactiveProperty<bool> MuteAudio {get; private set;}
      public ReactiveProperty<float> AudioVolume {get; private set;}

      [Inject]
      public SettingsModel() {
         AudioVolume = new ReactiveProperty<float>(50);
         MuteAudio = AudioVolume.Select(val => val == 0).ToReadOnlyReactiveProperty();
      }
   }
}
