using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used for playing and manage sounds & musics
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioSource sfxAudioSource;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", .5f);
		sfxAudioSource.volume = PlayerPrefs.GetFloat("SFXVolume", .5f);
	}

	public void PlayMusic(AudioClip audioClip)
	{
		if (audioSource.isPlaying) audioSource.Stop();
		audioSource.PlayOneShot(audioClip);
	}

	public void PlaySFX(AudioClip audioClip)
	{
		if (sfxAudioSource.isPlaying) sfxAudioSource.Stop();
		sfxAudioSource.PlayOneShot(audioClip);
	}

	public bool IsAudioPlaying()
	{
		return audioSource.isPlaying;
	}

	#region Settings
	/// <param name="volume">Between 0 and 1</param>
	public void SetMusicVolume(float volume)
	{
		audioSource.volume = volume;
		PlayerPrefs.SetFloat("MusicVolume", volume);
	}
	/// <param name="volume">Between 0 and 1</param>
	public void SetSFXVolume(float volume)
	{
		sfxAudioSource.volume = volume;
		PlayerPrefs.SetFloat("SFXVolume", volume);
	}
	#endregion


	#region Ui buttons
	[Header("UI buttons sounds")]
	[SerializeField] private AudioClip uiHover;
	[SerializeField] private AudioClip uiSelect;
	[SerializeField] private AudioClip noMoney;
	public void PlayUIHoverEvent()
	{
		if (uiHover != null) PlaySFX(uiHover);
		else Debug.LogWarning("No sound for hovering button");
	}
	public void PlayUISelectEvent()
	{
		if (uiSelect != null) PlaySFX(uiSelect);
		else Debug.LogWarning("No sound for clicking button");
	}
	public void PlaynoMoney()
	{
		if (noMoney != null) PlaySFX(noMoney);
		else Debug.LogWarning("No sound for no money");
	}

	#endregion

	#region materials and molicules
	[Header("UI buttons sounds")]
	[SerializeField] private AudioClip win;
	[SerializeField] private AudioClip loose;
	public void PlayWin()
	{
		if (win != null) PlaySFX(win);
		else Debug.LogWarning("No sound for win");
	}
	public void PlayLose()
	{
		if (win != null) PlaySFX(loose);
		else Debug.LogWarning("No sound for loose");
	}
	#endregion

	#region Materials
	[Header("Materials sounds")]
	[SerializeField] private List<AudioClip> bell = new();
	[SerializeField] private List<AudioClip> bongo = new();
	[SerializeField] private List<AudioClip> glass = new();
	[SerializeField] private List<AudioClip> tap = new();
	public void PlayPlatine()
	{
		if (glass.Count > 0)
		{
			PlaySFX(glass[Random.Range(0, glass.Count)]);
		}
		else Debug.LogWarning("No sound for clicking button");
	}
	public void PlayFer()
	{
		if (bongo.Count > 0)
		{
			PlaySFX(bongo[Random.Range(0, bongo.Count)]);
		}
		else Debug.LogWarning("No sound for clicking button");
	}
	public void PlayAl()
	{
		if (tap.Count > 0)
		{
			PlaySFX(tap[Random.Range(0, tap.Count)]);
		}
		else Debug.LogWarning("No sound for clicking button");
	}
	public void PlayAlFer()
	{
		if (bell.Count > 0)
		{
			PlaySFX(bell[Random.Range(0, bell.Count)]);
		}
		else Debug.LogWarning("No sound for clicking button");
	}
	#endregion
}
