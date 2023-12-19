using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace WpfApp2;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private MediaPlayer _mediaPlayer;

    private OpenFileDialog _openFileDialog;

    public void RunMusic(object obj, EventArgs arg)
    {
        _mediaPlayer.Open(new Uri(_openFileDialog.FileName));
        _mediaPlayer.Play();
    }
    
    public App()
    {
        _mediaPlayer = new();

        // Сделать относительную ссылку.
        _openFileDialog = new()
        {
            FileName = "D:\\CodeRepos\\CS\\NewGame\\Game_7_sem\\WpfApp2\\snd\\backgroundMusic.mp3"
        };
        RunMusic(this, new EventArgs());
        _mediaPlayer.MediaEnded += RunMusic;
    }

}
