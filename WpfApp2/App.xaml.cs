using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Win32;

namespace WpfApp2;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private MediaPlayer mediaPlayer;

    private OpenFileDialog _openFileDialog;

    public void RunMusic(object obj, EventArgs arg)
    {
        mediaPlayer.Play();
    }
    
    public void FailedMusic(object obj, EventArgs arg)
    {
        throw new Exception("Music has been dead");
    }

    public App()
    {
        mediaPlayer = new();
        // Гг
        // D:\CodeRepos\CS\NewGame\Game_7_sem\WpfApp2\snd\backgroundMusic.mp3
        // "pack://siteoforigin:,,,/snd/backgroundMusic.wav"
        //A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\Music1.mp3
        _openFileDialog = new()
        {
            FileName = "A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\Music1.mp3"
        };
        mediaPlayer.MediaOpened += RunMusic;
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri(_openFileDialog.FileName));
    }
}
