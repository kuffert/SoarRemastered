using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the playing of audio files.
/// </summary>
public class AudioManager {

    /// <summary>
    /// Plays an audio file from an AudioSource if sound is enabled.
    /// </summary>
    /// <param name="sound">The audio to be played.</param>
    public static void playSound(AudioSource sound)
    {
        if (!UserData.userData.getSoundDisabled() && !sound.isPlaying)
        {
            sound.Play();
        }
    }

    /// <summary>
    /// Plays an audio file from an AudioSource if music is enabled.
    /// </summary>
    /// <param name="music">The audio to be played.</param>
    public static void playMusic(AudioSource music)
    {
        if (!UserData.userData.getMusicDisabled() && !music.isPlaying)
        {
            music.Play();
        }
    }

    /// <summary>
    /// Stops a currently playing audio file.
    /// </summary>
    /// <param name="audio">The currently playing audio file.</param>
    public static void stopAudio(AudioSource audio)
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }
}
