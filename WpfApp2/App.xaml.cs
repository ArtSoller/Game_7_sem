using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;

namespace WpfApp2;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public Player Me;

    public Player? Companion;

    private MediaPlayer _mediaPlayer;

    private OpenFileDialog _openFileDialog;

    public void RunMusic(object obj, EventArgs arg)
    {
        _mediaPlayer.Play();
    }
    
    public void FailedMusic(object obj, EventArgs arg)
    {
        throw new Exception("Music has been dead");
    }

    public App()
    {
        _mediaPlayer = new();
        Me = new();

        // Гг
        // D:\CodeRepos\CS\NewGame\Game_7_sem\WpfApp2\snd\backgroundMusic.mp3
        // "pack://siteoforigin:,,,/snd/backgroundMusic.wav"
        _openFileDialog = new()
        {
            FileName = "A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\backgroundMusic.mp3"
        };
        _mediaPlayer.MediaOpened += RunMusic;
        _mediaPlayer.MediaFailed += FailedMusic;
        _mediaPlayer.Open(new Uri(_openFileDialog.FileName));
    }
}
